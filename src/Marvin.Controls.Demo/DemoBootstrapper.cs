using System.Windows;
using Caliburn.Micro;
using Marvin.Controls.Demo.Shell;

namespace Marvin.Controls.Demo
{
    public class DemoBootstrapper : BootstrapperBase
    {
        public DemoBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
