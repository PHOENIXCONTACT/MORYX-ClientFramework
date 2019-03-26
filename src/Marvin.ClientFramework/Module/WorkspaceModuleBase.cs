using System.Windows.Media;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.ClientFramework.History;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Abstract for workspace modules
    /// </summary>
    /// <typeparam name="TConf"></typeparam>
    public abstract class WorkspaceModuleBase<TConf> : ClientModuleBase<TConf>, IWorkspaceModule
        where TConf : class, IClientModuleConfig, new()
    {
        #region Dependency Injection

        /// <summary>
        /// Injected <see cref="IWindowManager"/>
        /// </summary>
        public IWindowManager WindowManager { get; set; }

        /// <summary>
        /// Injected <see cref="IDialogManager"/>
        /// </summary>
        public IDialogManager DialogManager { get; set; }

        /// <summary>
        /// Injected <see cref="IHistory"/>
        /// </summary>
        public IHistory History { get; set; }

        #endregion

        /// <inheritdoc />
        public abstract Geometry Icon { get; }

        /// <inheritdoc />
        public virtual bool HasButton => true;

        /// <summary>
        /// Gets called when the module shall be created
        /// </summary>
        /// <returns></returns>
        protected abstract IModuleWorkspace OnCreateWorkspace();

        /// <summary>
        /// Gets called when module shall be destroyed
        /// </summary>
        /// <param name="workspace"></param>
        protected abstract void OnDestroyWorkspace(IModuleWorkspace workspace);

        internal sealed override void AdditionalInitialize()
        {
            Container.SetInstance(WindowManager).SetInstance(DialogManager)
                .SetInstance<IHistoryWriter>(new HistoryWriter(this, History));
        }

        /// <inheritdoc />
        public IModuleWorkspace CreateWorkspace()
        {
            return OnCreateWorkspace();
        }

        /// <inheritdoc />
        public void DestroyWorkspace(IModuleWorkspace workspace)
        {
            OnDestroyWorkspace(workspace);
        } 
    }
}