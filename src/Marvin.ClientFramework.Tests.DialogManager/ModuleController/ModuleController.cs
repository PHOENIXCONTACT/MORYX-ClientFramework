using Marvin.ClientFramework.Base;

namespace Marvin.ClientFramework.Tests.DialogManager
{
    [ClientModule("DialogManagerTest")]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        protected override void OnInitialize()
        {
            Config.DisplayName = "DialogManager";

        }

        protected override void OnActivate()
        {

        }

        protected override void OnDeactivate(bool close)
        {

        }

        protected override IModuleWorkspace OnCreateWorkspace()
        {
            return Container.Resolve<IModuleWorkspace>("DialogManagerWorkspaceViewModel");
        }

        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {

        }
    }
}