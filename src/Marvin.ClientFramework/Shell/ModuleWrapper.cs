using System.Windows.Media;
using Caliburn.Micro;

namespace Marvin.ClientFramework.Shell
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
                ScreenExtensions.TryDeactivate(oldScreen, false);

            CurrentWorkspace = workspace;
            ScreenExtensions.TryActivate(CurrentWorkspace);
        }

        /// <inheritdoc />
        protected override void OnActivate()
        {
            Module.Activate();

            base.OnActivate();
        }

        /// <inheritdoc />
        protected override void OnDeactivate(bool close)
        {
            Module.Deactivate(close);

            ScreenExtensions.TryDeactivate(CurrentWorkspace, close);

            base.OnDeactivate(close);
        }

        #endregion
    }
}