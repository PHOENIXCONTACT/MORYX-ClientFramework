using Marvin.ClientFramework.Base;

namespace Marvin.ClientFramework.Tests.HistoryWriter
{
    [ClientModule("HistoryWriter")]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        protected override void OnInitialize()
        {
            Config.DisplayName = "History";
        }

        protected override void OnActivate()
        {

        }

        protected override void OnDeactivate(bool close)
        {

        }

        protected override IModuleWorkspace OnCreateWorkspace()
        {
            var fac = Container.Resolve<IHistoryWorkspaceFactory>();
            return fac.CreateWorkspace(1);
        }

        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {

        }
    }
}