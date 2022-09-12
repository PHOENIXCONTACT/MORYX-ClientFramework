// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Threading;
using Caliburn.Micro;
using CommandLine;
using Moryx.ClientFramework.Localization;
using Moryx.Configuration;
using Moryx.Container;
using Moryx.Logging;
using Moryx.Threading;
using Moryx.Tools;
using MessageBoxImage = System.Windows.MessageBoxImage;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Main class to create ClientFramework UIs
    /// </summary>
    public class HeartOfLead : HeartOfLead<DefaultCommandLineArguments>
    {
        /// <inheritdoc />
        public HeartOfLead(string[] args) : base(args)
        {
        }
    }

    /// <summary>
    /// Main class to create ClientFramework UIs
    /// </summary>
    public class HeartOfLead<TCommandLineArguments> : IApplicationRuntime, ILoggingHost
        where TCommandLineArguments : DefaultCommandLineArguments
    {
        #region Fields and Properties

        string ILoggingHost.Name => "ClientKernel";

        /// <inheritdoc />
        IModuleLogger ILoggingHost.Logger { get; set; }

        /// <inheritdoc />
        IContainer IApplicationRuntime.GlobalContainer => _container;

        /// <summary>
        /// Returns the current <see cref="AppConfig"/>
        /// </summary>
        protected AppConfig AppConfig { get; private set; }

        /// <summary>
        /// Returns the current <see cref="DefaultCommandLineArguments"/>
        /// </summary>
        protected TCommandLineArguments CommandLineOptions { get; private set; }

        /// <summary>
        /// Flag if the HeartOfLead is initialized
        /// </summary>
        public bool IsInitialied { get; private set; } // TODO: Rename to IsInitialized in the next major

        private GlobalContainer _container;
        private IKernelConfigManager _configManager;
        private IAppDataConfigManager _appDataConfigManager;
        private BaseApplication _application;
        private IRunModeController _runModeController;
        private ILoaderHandler _loaderHandler;
        private IModuleManager _moduleManager;

        private readonly string[] _args;

        #endregion

        #region Main and ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="HeartOfLead{TCommandLineArguments}"/> class.
        /// </summary>
        /// <param name="args"></param>
        public HeartOfLead(string[] args)
        {
            _args = args;

            // This is necessary to enable assembly lookup from AppDomain
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;

            // Load additional assemblies
            LoadAssembliesFromFramework();
        }

        #endregion

        /// <summary>
        /// Initializes HeartOfLead instance
        /// </summary>
        public void Initialize()
        {
            // Check Initialization
            if (IsInitialied)
                throw new InvalidOperationException("HeartOfLead is already initialized!");

            // Initialize platform
            WpfPlatform.SetProduct();

            // Attach this Application to the console.
            Kernel32.AttachConsole(-1);

            // Will parse the exe arguments
            ParseCommandLineArguments();

            // Create global container and configure config manager
            CreateContainer();

            // Register app config
            LoadConfiguration();

            // Activates the logging
            ActivateKernelLogging();

            //Configures the thread context
            ConfigureThreadContext();

            // ConfigureLocalization
            ConfigureLocalization();

            // Limit instances
            LimitInstances();

            // Manages the loader
            ConfigureLoaderHandler();

            // Configure view locator
            ConfigureViewLocator();

            // Initialize application
            InitializeApplication();

            // Lock initialization
            IsInitialied = true;
        }

        /// <summary>
        /// Configures the localization for the client
        /// </summary>
        private void ConfigureLocalization()
        {
            var appDataConfigManager = _container.Resolve<IAppDataConfigManager>();
            var languageConfig = appDataConfigManager.GetConfiguration<UserLanguageConfig>();

            // If not set, select os language
            var culture = string.IsNullOrEmpty(languageConfig.Culture)
                ? CultureInfo.InvariantCulture
                : new CultureInfo(languageConfig.Culture);

            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            // Ensure the current culture passed into bindings is the OS culture.
            // By default, WPF uses en-US as the culture, regardless of the system settings.
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement),
                new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(culture.IetfLanguageTag)));
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            if (!IsInitialied)
                throw new InvalidOperationException("HeartOfLead is not initialized!");

            // Start the base application here - thread will hold up to that the application will closed
            StartApplication();
        }

        /// <summary>
        /// Parses the arguments.
        /// </summary>
        private void ParseCommandLineArguments()
        {
            var parser = new CommandLine.Parser(with => with.HelpWriter = Console.Out);
            var result = parser.ParseArguments<TCommandLineArguments>(_args);
            if (result.Tag == ParserResultType.Parsed)
                CommandLineOptions = ((Parsed<TCommandLineArguments>)result).Value;
            else
                Environment.Exit(1);
        }

        /// <summary>
        /// Configures the thread context.
        /// </summary>
        private void ConfigureThreadContext()
        {
            var catcher = _container.Resolve<IErrorCatcher>();

            //set global thread context
            ThreadContext.Dispatcher = Dispatcher.CurrentDispatcher;
            ThreadContext.Dispatcher.UnhandledException += catcher.HandleDispatcherException;
        }

        /// <summary>
        /// configures the loader handler (loader view)
        /// </summary>
        private void ConfigureLoaderHandler()
        {
            _loaderHandler = _container.Resolve<ILoaderHandler>();
            _container.ComponentCreated += _loaderHandler.CheckForAdapter;
            _loaderHandler.Initialize();
        }

        /// <summary>
        /// Activates the logging.
        /// </summary>
        private void ActivateKernelLogging()
        {
            var management = _container.Resolve<ILoggerManagement>();
            management.ActivateLogging(this);

            _container.SetInstance(((ILoggingHost)this).Logger);
        }

        /// <summary>
        /// Initializes the BaseApplication
        /// </summary>
        private void InitializeApplication()
        {
            _application = new BaseApplication(_container);
            _application.InitializeComponent();
            _application.Startup += OnApplicationStartUp;
            _application.Exit += OnApplicationExit;
        }

        /// <summary>
        /// Starts the application.
        /// </summary>
        private void StartApplication()
        {
            _application.Run();
        }

        /// <summary>
        /// Called when [application start up].
        /// </summary>
        private void OnApplicationStartUp(object sender, StartupEventArgs startupEventArgs)
        {
            var cmpInitializer = _container.Resolve<IComponentInitializer>();
            cmpInitializer.Completed += CmpInitializerCompleted;

            cmpInitializer.Initialize();
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        private void OnApplicationExit(object sender, ExitEventArgs exitEventArgs)
        {
            _application.DisposeShell();

            _moduleManager?.Dispose();
            _configManager?.SaveAll();
            _appDataConfigManager?.SaveAll();
        }

        /// <summary>
        /// Called when all components are initialized
        /// </summary>
        private void CmpInitializerCompleted(object sender, EventArgs eventArgs)
        {
            _runModeController = _container.Resolve<IRunModeController>();

            //configure runMode and initialize
            _runModeController.RunModeReady += OnRunModeReady;
            _runModeController.Initialize();

            _runModeController.Start();
        }

        /// <summary>
        /// Called when [run mode ready].
        /// </summary>
        private void OnRunModeReady(object sender, ModulesConfiguration modulesConfiguration)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    _moduleManager = _container.Resolve<IModuleManager>();
                    _moduleManager.Initialize();

                    ThreadContext.BeginInvoke(() => _application.InitializeShell());
                }
                catch (Exception exception)
                {
                    CrashHandler.WriteErrorToFile(ExceptionPrinter.Print(exception));
                    throw;
                }
            });
        }

        /// <summary>
        /// Creates the container
        /// </summary>
        private void CreateContainer()
        {
            var container = new GlobalContainer();

            // Parallel operations
            container.Register<IParallelOperations, ParallelOperations>();

            // Register window manager
            container.Register<IWindowManager, WindowManager>();

            // Load kernel and core modules
            container.ExecuteInstaller(new AutoInstaller(GetType().Assembly));
            container.LoadComponents<object>(type => type.GetCustomAttribute(typeof(KernelComponentAttribute)) != null);

            _container = container;
        }

        /// <summary>
        /// Loads all framework configurations configurations.
        /// </summary>
        private void LoadConfiguration()
        {
            if (!Directory.Exists(CommandLineOptions.ConfigFolder))
                Directory.CreateDirectory(CommandLineOptions.ConfigFolder);

            // Configure config manager
            _configManager = new KernelConfigManager { ConfigDirectory = CommandLineOptions.ConfigFolder };
            _container.SetInstance<IKernelConfigManager>(_configManager, "KernelConfigManager");
            _container.SetInstance<IConfigManager>(_configManager, "ConfigManager");

            // Load global app config
            AppConfig = _configManager.GetConfiguration<AppConfig>();

            if (CommandLineOptions.StartConfigurator || AppConfig.OpenConfigWithControl && (Keyboard.Modifiers & ModifierKeys.Control) > 0)
                AppConfig.RunMode = KernelConstants.CONFIG_RUNMODE;

            // Configure AppDataConfigManager with the application config directory
            _appDataConfigManager = new AppDataConfigManager(AppConfig.Application, CommandLineOptions.ConfigFolder);
            _container.SetInstance(_appDataConfigManager);
        }

        /// <summary>
        /// If enabled, instances will be limited
        /// </summary>
        private void LimitInstances()
        {
            if (!AppConfig.LimitInstances)
                return;

            var instances = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().Location));
            if (instances.Length <= 1) // Usually the instance count should be at least 1 but if the application is started in the debugger the instance count is zero.
                return;

            User32.BringToFront(instances.OrderBy(p => p.StartTime).First().MainWindowHandle);

            Environment.Exit(1);
        }

        private void ConfigureViewLocator()
        {
            try
            {
                if (AppConfig.RunMode == KernelConstants.CONFIG_RUNMODE)
                    return;

                var viewLocatorConfigurator = _container.Resolve<IViewLocatorConfigurator>();
                viewLocatorConfigurator.ActivateSet(AppConfig.ViewPreset);
            }
            catch (NotSupportedException)
            {
                MessageBox.Show($"Cannot find any view locator preset with name '{AppConfig.ViewPreset}'. " +
                                "Please change view preset in configurator.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Find a matching assembly in the AppDomain
        /// </summary>
        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            return AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(item => item.GetName().Name == args.Name.Split(',')[0]);
        }

        /// <summary>
        /// Loads the assemblies from framework folder.
        /// </summary>
        private static void LoadAssembliesFromFramework()
        {
            // Load all additional directories
            AppDomainBuilder.LoadAssemblies();

            // Set working directory to location of this exe
            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
    }
}
