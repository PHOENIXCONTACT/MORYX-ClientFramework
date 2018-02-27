using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Caliburn.Micro;
using CommandLine;
using Marvin.ClientFramework.Dialog;
using Marvin.ClientFramework.UI;
using Marvin.Container;
using Marvin.Logging;
using Marvin.Threading;
using Marvin.Tools;
using MessageBoxImage = System.Windows.MessageBoxImage;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Main class to create ClientFramwork UI's
    /// </summary>
    public class HeartOfLead : HeartOfLead<DefaultCommandLineArguments>
    {
        /// <inheritdoc />
        public HeartOfLead(string[] args) : base(args)
        {
        }
    }

    /// <summary>
    /// Main class to create ClientFramwork UI's
    /// </summary>
    public class HeartOfLead<TCommandLineArguments> : ILoggingHost 
        where TCommandLineArguments : DefaultCommandLineArguments
    {
        #region Fields

        string ILoggingHost.Name => "ClientKernel";
        IModuleLogger ILoggingHost.Logger { get; set; }

        /// <summary>
        /// Returns the current <see cref="AppConfig"/>
        /// </summary>
        protected AppConfig AppConfig { get; private set; }

        /// <summary>
        /// Returns the current <see cref="DefaultCommandLineArguments"/>
        /// </summary>
        protected TCommandLineArguments CommandLineOptions { get; private set; }

        private GlobalContainer _container;
        private IKernelConfigManager _configManager;
        private IAppDataConfigManager _appDataConfigManager;
        private BaseApplication _application;
        private IRunModeController _runModeController;
        private ILoaderHandler _loaderHandler;
        private IModuleManager _moduleManager;

        private string[] _args;

        #endregion

        #region Main and ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="HeartOfLead{TCommandLineArguments}"/> class.
        /// </summary>
        /// <param name="args"></param>
        public HeartOfLead(string[] args)
        {
            Initialize(args);
        }

        private void Initialize(string[] args)
        {
            _args = args;

            // This is necessary to enable assembly lookup from AppDomain
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;

            // Load additional assemblies
            LoadAssembliesFromFramework();
        }

        #endregion

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        {
            // Attach this Application to the console.
            Kernel32.AttachConsole(-1);

            // Will parse the exe arguments
            ParseCommandLineArguments();

            // Create global container and configure config manager
            CreateContainer();

            // Activates the logging
            ActivateKernelLogging();

            //Configures the thread context
            ConfigureThreadContext();

            // Register app config
            LoadConfiguration();

            // Limit instances
            LimitInstances();

            // Manages the loader
            ConfigureLoaderHandler();

            // Configure view locator
            ConfigureViewLocator();

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
        /// Starts the application.
        /// </summary>
        private void StartApplication()
        {
            _application = new BaseApplication(_container);
            _application.InitializeComponent();
            _application.Startup += OnApplicationStartUp;
            _application.Exit += OnApplicationExit;

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
            _appDataConfigManager?.SaveAll();
        }

        /// <summary>
        /// Called when all components are initialized
        /// </summary>
        private void CmpInitializerCompleted(object sender, EventArgs eventArgs)
        {
            _runModeController = _container.Resolve<IRunModeController>();

            //configure runmode and initialize
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

            // Register runtimes
            container.Register<IWindowManager, WindowManager>();

            // Load kernel and core modules
            container.ExecuteInstaller(new AutoInstaller(GetType().Assembly));
            container.LoadComponents<object>(type => type.GetCustomAttribute(typeof(KernelComponentAttribute)) != null);

            //TODO: find better way to register factory and global components
            container.Register<IMessageBox, MessageBoxViewModel>("MessageBoxViewModel", LifeCycle.Transient);
            container.Register<IMessageBoxFactory>();

            _container = container;
        }

        /// <summary>
        /// Loads all framework configurations configurations.
        /// </summary>
        private void LoadConfiguration()
        {
            if (!Directory.Exists(CommandLineOptions.ConfigFolder))
                Directory.CreateDirectory(CommandLineOptions.ConfigFolder);

            //configure config manager
            _configManager = new KernelConfigManager { ConfigDirectory = CommandLineOptions.ConfigFolder };
            _container.SetInstance(_configManager);

            //load global app config
            AppConfig = _configManager.GetConfiguration<AppConfig>();

            if (CommandLineOptions.StartConfigurator || AppConfig.OpenConfigWithControl && (Keyboard.Modifiers & ModifierKeys.Control) > 0)
                AppConfig.RunMode = KernelConstants.CONFIG_RUNMODE;

            _appDataConfigManager = new AppDataConfigManager
            {
                AppDataConfigDefaultsDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, CommandLineOptions.ConfigFolder, AppDataConfigManager.AppDataDefaultsDirectoryName)
            };

            _appDataConfigManager.Initialize(AppConfig.Application);
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
        public static void LoadAssembliesFromFramework()
        {
            // Load all additional directories
            AppDomainBuilder.LoadAssemblies();

            // Set working directory to location of this exe
            // ReSharper disable once AssignNullToNotNullAttribute
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
    }
}
