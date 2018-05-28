using System.Reflection;
using System.Windows.Media;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.ClientFramework.History;
using Marvin.ClientFramework.Properties;
using Marvin.Tools;

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
        /// Window manager to register in local container
        /// </summary>
        public IWindowManager WindowManager { get; set; }

        /// <summary>
        /// DialogManager to register in local container
        /// </summary>
        public IDialogManager DialogManager { get; set; }

        /// <summary>
        /// History to register in local container
        /// </summary>
        public IHistory History { get; set; }

        #endregion

        public string DisplayName { get; protected set; }

        public virtual Geometry Icon => Geometry.Empty;

        public virtual bool HasButton => true;

        /// <summary>
        /// Gets called when the module shall be created
        /// </summary>
        /// <returns></returns>
        protected abstract IModuleWorkspace OnCreateWorkspace();

        /// <summary>
        /// Gets called when module shall be destroyed
        /// </summary>
        protected abstract void OnDestroyWorkspace(IModuleWorkspace workspace);

        internal sealed override void AdditionalInitialize()
        {
            Container.SetInstance(WindowManager).SetInstance(DialogManager)
                .SetInstance<IHistoryWriter>(new HistoryWriter(this, History));

            DisplayName = GetType().GetDisplayName() ?? ModuleName;
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