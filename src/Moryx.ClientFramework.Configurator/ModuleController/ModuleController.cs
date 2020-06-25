// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Linq;
using Moryx.ClientFramework.Kernel;

namespace Moryx.ClientFramework.Configurator
{
    /// <summary>
    /// Module controller for the framework configuration.
    /// The main workspace hosts the several configuration view models
    /// </summary>
    [ClientModule("Configurator"), ComponentForRunMode(KernelConstants.CONFIG_RUNMODE)]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        #region Dependency Injection

        /// <summary>
        /// Gets or sets the run modes.
        /// </summary>
        public IEnumerable<IRunMode> RunModes { get; set; }

        /// <summary>
        /// Gets or sets the presets.
        /// </summary>
        public IEnumerable<IViewLocatorConfiguratorPreset> Presets { get; set; }

        /// <summary>
        /// Gets or sets the core configuration manager.
        /// </summary>
        public IKernelConfigManager KernelConfigManager { get; set; }

        /// <summary>
        /// Config manager for apply configurations in the app data dir of the user
        /// </summary>
        public IAppDataConfigManager AppDataConfigManager { get; set; }

        #endregion

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        protected override void OnInitialize()
        {
            Container.LoadComponents<IConfigViewModel>();

            Container.SetInstance(KernelConfigManager)
                .SetInstance(AppDataConfigManager);

            foreach (var runMode in RunModes.ToList())
                Container.SetInstance(runMode);

            foreach (var preset in Presets.ToList())
                Container.SetInstance(preset);
        }

        /// <summary>
        /// Called when [activate].
        /// </summary>
        protected override void OnActivate()
        {
        }

        /// <summary>
        /// Called when [deactivate].
        /// </summary>
        protected override void OnDeactivate(bool close)
        {
        }

        /// <summary>
        /// Called when [create workspace].
        /// </summary>
        /// <returns></returns>
        protected override IModuleWorkspace OnCreateWorkspace()
        {
            return Container.Resolve<IModuleWorkspace>(FrameworkWorkspaceViewModel.ScreenName);
        }

        /// <summary>
        /// Called when [destroy workspace].
        /// </summary>
        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {
        }
    }
}
