using System.Windows.Media;
using Caliburn.Micro;

namespace Marvin.ClientFramework.Shell
{
    public class ModuleWrapper : Screen
    {
        public ModuleWrapper(IWorkspaceModule module)
        {
            Module = module;
        }

        #region Binding props

        public IWorkspaceModule Module { get; private set; }

        public override string DisplayName
        {
            get { return Module.Config.DisplayName; }
        }

        /// <summary>
        /// Defines AutomationId for module button
        /// </summary>
        public string AutomationId
        {
            get { return string.Format("AID_{0}_ModuleButton", Module.Config.DisplayName.Replace(" ", "_")); }
        }

        public Geometry Icon
        {
            get { return (Geometry)Module.Icon; }
        }

        private IModuleWorkspace _workspace;
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

        public void ChangeWorkspace(IModuleWorkspace workspace)
        {
            var oldScreen = CurrentWorkspace;
            if (oldScreen != null)
                ScreenExtensions.TryDeactivate(oldScreen, false);

            CurrentWorkspace = workspace;
            ScreenExtensions.TryActivate(CurrentWorkspace);
        }

        protected override void OnActivate()
        {
            Module.Activate();

            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            Module.Deactivate(close);

            ScreenExtensions.TryDeactivate(CurrentWorkspace, close);

            base.OnDeactivate(close);
        }

        #endregion

        public override string ToString()
        {
            return string.Format("{0} - {1}", Module.Name, _workspace.GetType().Name);
        }
    }
}