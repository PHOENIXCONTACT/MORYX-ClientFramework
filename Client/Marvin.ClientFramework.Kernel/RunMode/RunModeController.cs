using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.Container;
using Marvin.Tools;
using MessageBoxImage = System.Windows.MessageBoxImage;

namespace Marvin.ClientFramework.Kernel
{
    [KernelComponent(typeof(IRunModeController))]
    internal class RunModeController : IRunModeController
    {
        #region Dependency Injection

        public IRunMode Current { get; private set; }
        public IContainer Container { get; set; }
        
        public IEnumerable<IRunMode> PossibleRunModes { get; set; }
        public IKernelConfigManager ConfigManager { get; set; }

        #endregion

        #region Fields

        private CaliburnBootstrapper _caliburnBootstrapper;
        private AppConfig _appConfig;

        #endregion

        ///
        public void Initialize()
        {
            _appConfig = ConfigManager.GetConfiguration<AppConfig>();

            var selectedRunMode = SelectRunMode();
            Current = Container.Resolve<IRunMode>(selectedRunMode);

            Current.AssembliesLoaded += OnAssembliesLoaded;
            Current.LoadModulesConfigurationCompleted += OnModulesConfigurationLoaded;
            Current.ExeptionOccured += OnExceptionOccured;
        }

        /// 
        public void Start()
        {
            Current.Initialize();
        }

        /// <summary>
        /// Selects the run mode.
        /// </summary>
        private string SelectRunMode()
        {
            var allRunModes = Container.ResolveAll<IRunMode>().Select(r => r.GetType().GetCustomAttribute<KernelComponentAttribute>().Name);
            string selectedRunMode;

            if (string.IsNullOrEmpty(_appConfig.RunMode) || !allRunModes.Contains(_appConfig.RunMode))
            {
                selectedRunMode = KernelConstants.CONFIG_RUNMODE;
            }
            else
            {
                selectedRunMode = _appConfig.RunMode;
            }

            return selectedRunMode;
        }

        /// <summary>
        /// Called when [assemblies loaded].
        /// </summary>
        private void OnAssembliesLoaded(object sender, IEnumerable<Assembly> assemblies)
        {
            var assemblyList = assemblies.ToList();

            //TODO: find better way to register UI DLL
            assemblyList.Add(typeof(DialogConductorView).Assembly);

            //initialize the container
            _caliburnBootstrapper = new CaliburnBootstrapper(assemblyList, Container);
            _caliburnBootstrapper.Initialize();

            //create module configuration
            Current.LoadModulesConfiguration();
        }

        /// <summary>
        /// Called when [modules configuration loaded].
        /// </summary>
        private void OnModulesConfigurationLoaded(object sender, ModulesConfiguration modulesConfiguration)
        {
            //set config provider
            Container.SetInstance(Current.ConfigProvider);
            Container.SetInstance(modulesConfiguration);

            RunModeReady?.Invoke(this, modulesConfiguration);
        }

        /// <summary>
        /// Called when [exeption occured].
        /// </summary>
        private static void OnExceptionOccured(object sender, ClientException e)
        {
            MessageBox.Show(e.DisplayText + "\r\n\r\n" + ExceptionPrinter.Print(e), e.Message, MessageBoxButton.OK,
                MessageBoxImage.Error);

            Execute.BeginOnUIThread(() => Application.Current.Shutdown(2));
        }

        ///
        public event EventHandler<ModulesConfiguration> RunModeReady;
    }
}