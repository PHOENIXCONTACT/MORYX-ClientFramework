// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Moryx.ClientFramework.Dialog;
using Moryx.ClientFramework.History;
using Moryx.Container;
using Moryx.Tools.Wcf;

namespace Moryx.ClientFramework.Shell
{
    /// <summary>
    /// Base view model for the application shells.
    /// </summary>
    public abstract class ShellViewModelBase : Conductor<ModuleWrapper>.Collection.OneActive, IModuleShell
    {
        #region Dependency Injection

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IModuleManager ModuleManager { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IHistory History { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IDialogManager DialogManager { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IWindowManager WindowManager { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IConfigProvider ConfigProvider { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IKernelConfigManager KernelConfigManager { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IWcfClientFactory WcfClientFactory { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IAppDataConfigManager AppDataConfigManager { get; set; }

        /// <summary>
        /// Dependency provided for the shells local container
        /// </summary>
        public IModuleContainerFactory ContainerFactory { get; set; }

        #endregion

        #region Fields and properties

        /// <summary>
        /// Contains the created region controller
        /// </summary>
        protected IShellRegionController RegionController { get; private set; }

        #endregion

        #region IModuleShell

        Task IModuleShell.InitializeAsync()
        {
            RegionController = CreateController();
            RegionController.Initialize(ContainerFactory, ModuleManager, ConfigProvider);

            //register components needed in the shell container
            RegionController.Register(KernelConfigManager);
            RegionController.Register(WindowManager);
            RegionController.Register(WcfClientFactory);
            RegionController.Register(AppDataConfigManager);

            var enabledModules = ModuleManager.EnabledModules.OfType<IWorkspaceModule>();

            Items.AddRange(WrapModules(enabledModules));
            History.WorkspaceChanged += HistoryOnWorkspaceChanged;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Creates the shell region controller for the current shell
        /// </summary>
        protected abstract IShellRegionController CreateController();

        #endregion

        #region History

        /// <summary>
        /// Pushes the given mdule to the main workspace.
        /// </summary>
        public void SelectModule(ModuleWrapper wrapper)
        {
            History.Push(wrapper.Module);
        }

        /// <summary>
        /// Pushes the next workspace
        /// </summary>
        public void HistoryNext()
        {
            History.MoveNext();
        }

        /// <summary>
        /// Pushes the previous workspace
        /// </summary>
        public void HistoryPrevious()
        {
            History.MovePrevious();
        }

        private void HistoryOnWorkspaceChanged(object sender, WorkspacePair workspacePair)
        {
            var wrapper = Items.First(item => item.Module == workspacePair.Module);
            wrapper.ChangeWorkspace(workspacePair.Workspace);

            ActiveItem = wrapper;
        }

        #endregion

        /// <summary>
        /// Returns a region by name
        /// </summary>
        protected Region FetchRegion(string regionName)
        {
            var region = RegionController.FetchRegion(regionName);
            ((IShellRegion)region.Content).ConnectToShell(this);
            return region;
        }

        private static IEnumerable<ModuleWrapper> WrapModules(IEnumerable<IWorkspaceModule> modules)
        {
            return modules.OrderBy(m => m.Config.SortIndex).Select(cm => new ModuleWrapper(cm));
        }
    }
}
