using System;
using System.Linq;
using Marvin.ClientFramework.Shell;
using Marvin.Logging;
using Marvin.Threading;

namespace Marvin.ClientFramework.Config
{
    /// <summary>
    /// Spezialized shell for the configurator
    /// </summary>
    [ModuleShell("ConfigShell"), ComponentForRunMode(KernelConstants.CONFIG_RUNMODE)]
    public class ConfigShellViewModel : ShellViewModelBase
    {
        private int _watchWorker;
        public IModuleLogger Logger { get; set; }

        public IParallelOperations ParallelOperations { get; set; }

        protected override void OnInitialize()
        {
            SelectModule(Items.First());
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            _watchWorker = ParallelOperations.ScheduleExecution(Worker, 2000, 2000);
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            ParallelOperations.StopExecution(_watchWorker);
        }

        private void Worker()
        {
            NotifyOfPropertyChange(() => Now);
        }

        /// <summary>
        /// Gets the current date.
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.Now; }
        }
        
        /// <summary>
        /// Creates the controller.
        /// </summary>
        protected override IShellRegionController CreateController()
        {
            return new NullRegionController();
        }
    }
}