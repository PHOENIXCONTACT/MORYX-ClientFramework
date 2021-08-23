// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;

namespace Moryx.ClientFramework.Shell
{
    /// <summary>
    /// Wraps <see cref="IWorkspaceModule"/> in a <see cref="Screen"/>
    /// </summary>
    public class ModuleWrapper : Screen
    {
        /// <inheritdoc />
        public ModuleWrapper(IWorkspaceModule module)
        {
            Module = module;
        }

        #region Binding props

        /// <summary>
        /// Wrapped <see cref="IWorkspaceModule"/>
        /// </summary>
        public IWorkspaceModule Module { get; }

        /// <inheritdoc />
        /// <summary>
        /// Display name of the wrapped module
        /// </summary>
        public override string DisplayName => Module.DisplayName;


        /// <summary>
        /// Icon of the wrapped module
        /// </summary>
        public Geometry Icon => Module.Icon;

        private IModuleWorkspace _workspace;
        /// <summary>
        /// Gets or sets the <see cref="IModuleWorkspace"/>
        /// </summary>
        public IModuleWorkspace CurrentWorkspace
        {
            get { return _workspace; }
            private set
            {
                _workspace = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        #region Transitions

        /// <summary>
        /// Changes the workspace
        /// </summary>
        /// <param name="workspace"></param>
        public void ChangeWorkspace(IModuleWorkspace workspace)
        {
            var oldScreen = CurrentWorkspace;
            if (oldScreen != null)
                ScreenExtensions.TryDeactivateAsync(oldScreen, false);

            CurrentWorkspace = workspace;
            ScreenExtensions.TryActivateAsync(CurrentWorkspace);
        }

        /// <inheritdoc />
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            await Module.ActivateAsync();
            await base.OnActivateAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override async Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            await Module.DeactivateAsync(close);
            await ScreenExtensions.TryDeactivateAsync(CurrentWorkspace, close, cancellationToken);

            await base.OnDeactivateAsync(close, cancellationToken);
        }

        #endregion
    }
}
