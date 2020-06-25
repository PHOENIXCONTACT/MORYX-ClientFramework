// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Linq;
using Moryx.ClientFramework.Shell;
using Moryx.Threading;

namespace Moryx.ClientFramework.Configurator
{
    /// <summary>
    /// Spezialized shell for the configurator
    /// </summary>
    [ModuleShell("ConfigShell"), ComponentForRunMode(KernelConstants.CONFIG_RUNMODE)]
    public class ConfigShellViewModel : ShellViewModelBase
    {
        private int _watchWorker;

        #region Dependencies

        /// <summary>
        /// Parallel opertations to shedule an update of the shell watch
        /// </summary>
        public IParallelOperations ParallelOperations { get; set; }

        #endregion

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            SelectModule(Items.First());
        }

        /// <inheritdoc />
        protected override void OnActivate()
        {
            base.OnActivate();

            _watchWorker = ParallelOperations.ScheduleExecution(Worker, 2000, 2000);
        }

        /// <inheritdoc />
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
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Creates the controller.
        /// </summary>
        protected override IShellRegionController CreateController()
        {
            return new NullRegionController();
        }
    }
}
