using System.Linq;
using System.Windows;
using Caliburn.Micro;
using Marvin.ClientFramework.Shell;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
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
            _container = container;
            Startup += OnApplicationStartup;
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

            // Initialize platfrom
            WpfPlatform.SetProduct(appConfig.Application);

            //initialize base root visual
            var defaultRootVisual = new RootVisual(windowConfig)
            {
                Title = appConfig.WindowTitle, 
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
        public void InitializeShell()
        {
            var configuration = _container.Resolve<ModulesConfiguration>();

            _moduleShell = _container.Resolve<IModuleShell>(configuration.Shell.ShellName);
            _moduleShell.Initialize();
            _moduleShell.Activate();

            View.SetModel(Current.MainWindow, _moduleShell);
        }

        /// <summary>
        /// will dispose the shell
        /// </summary>
        public void DisposeShell()
        {
            _moduleShell?.Deactivate(true);
        }
    }
}