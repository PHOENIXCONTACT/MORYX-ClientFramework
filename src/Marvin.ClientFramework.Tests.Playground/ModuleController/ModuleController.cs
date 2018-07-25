using System.Windows.Media;
using C4I;

namespace Marvin.ClientFramework.Tests.Playground
{
    [ClientModule("Playground")]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        public override Geometry Icon => ShapeFactory.GetShapeGeometry(CommonShapeType.Cloud);

        protected override void OnInitialize()
        {
            Config.DisplayName = "Playground";
        }

        protected override void OnActivate()
        {
        }

        protected override void OnDeactivate(bool close)
        {
        }

        protected override IModuleWorkspace OnCreateWorkspace()
        {
            return Container.Resolve<IModuleWorkspace>(PlaygroundWorkspaceViewModel.WorkspaceName);
        }

        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {
        }
    }
}