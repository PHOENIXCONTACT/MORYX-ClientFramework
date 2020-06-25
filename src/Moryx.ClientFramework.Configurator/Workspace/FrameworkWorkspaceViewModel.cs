// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Caliburn.Micro;
using Moryx.Container;

namespace Moryx.ClientFramework.Configurator
{
    /// <summary>
    /// Main workspace for this module. The view model will host an conductor 
    /// for the specified and loaded <see cref="IConfigViewModel"/>.
    /// </summary>
    [Plugin(LifeCycle.Singleton, typeof(IModuleWorkspace), Name = ScreenName)]
    internal class FrameworkWorkspaceViewModel : ModuleWorkspace
    {
        internal const string ScreenName = "FrameworkWorkspaceViewModel";

        #region Dependency Injection

        /// <summary>
        /// Gets or sets the conductor which hosts the several configuration view models.
        /// </summary>
        public IConfigConductorViewModel Conductor { get; set; }

        #endregion

        /// <summary>
        /// Called when activating.
        /// </summary>
        protected override void OnActivate()
        {
            base.OnActivate();

            ScreenExtensions.TryActivate(Conductor);
        }

        /// <summary>
        /// Called when deactivating.
        /// </summary>
        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);

            ScreenExtensions.TryDeactivate(Conductor, true);
        }
    }
}
