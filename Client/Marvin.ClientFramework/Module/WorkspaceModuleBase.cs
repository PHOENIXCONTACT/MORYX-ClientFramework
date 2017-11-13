using System.Windows.Media;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.ClientFramework.History;

namespace Marvin.ClientFramework
{
    public abstract class WorkspaceModuleBase<TConf> : ClientModuleBase<TConf>, IWorkspaceModule
        where TConf : class, IClientModuleConfig, new()
    {
        #region Dependency Injection

        public IWindowManager WindowManager { get; set; }

        public IDialogManager DialogManager { get; set; }

        public IHistory History { get; set; }

        #endregion

        public abstract Geometry Icon { get; }

        public virtual bool HasButton => true;

        protected abstract IModuleWorkspace OnCreateWorkspace();

        protected abstract void OnDestroyWorkspace(IModuleWorkspace workspace);

        internal sealed override void AdditionalInitialize()
        {
            Container.SetInstance(WindowManager).SetInstance(DialogManager)
                .SetInstance<IHistoryWriter>(new HistoryWriter(this, History));
        }

        public IModuleWorkspace CreateWorkspace()
        {
            return OnCreateWorkspace();
        }

        public void DestroyWorkspace(IModuleWorkspace workspace)
        {
            OnDestroyWorkspace(workspace);
        } 
    }
}