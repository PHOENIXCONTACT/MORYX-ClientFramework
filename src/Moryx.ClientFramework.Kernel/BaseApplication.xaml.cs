// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Moryx.ClientFramework.Shell;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class BaseApplication
    {
        private readonly IContainer _container;
        private IModuleShell _moduleShell;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseApplication"/> class.
        /// </summary>
        public BaseApplication(IContainer container)
        {
            Startup += OnApplicationStartup;

            _container = container;
        }

        /// <summary>
        /// Called when application is starting.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="startupEventArgs">The <see cref="StartupEventArgs"/> instance containing the event data.</param>
        private void OnApplicationStartup(object sender, StartupEventArgs startupEventArgs)
        {
            var loaderHandler = _container.Resolve<ILoaderHandler>();
            var windowConfig = _container.Resolve<IAppDataConfigManager>().GetConfiguration<WindowConfig>();
            var appConfig = _container.Resolve<IKernelConfigManager>().GetConfiguration<AppConfig>();

            //initialize base root visual
            var defaultRootVisual = new RootVisual(windowConfig)
            {
                Title = appConfig.Name,
                Content = loaderHandler.View
            };

            //show the main window with the loader view
            Current.MainWindow = defaultRootVisual;

            //show the window
            Current.MainWindow.Show();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        public async Task InitializeShell()
        {
            var configuration = _container.Resolve<ModulesConfiguration>();

            _moduleShell = _container.Resolve<IModuleShell>(configuration.Shell.ShellName);
            await _moduleShell.InitializeAsync();
            await _moduleShell.ActivateAsync();

            View.SetModel(Current.MainWindow, _moduleShell);
        }

        /// <summary>
        /// will dispose the shell
        /// </summary>
        public async Task DisposeShell()
        {
            if (_moduleShell != null)
                await _moduleShell.DeactivateAsync(true);
        }
    }
}
