using System.Windows.Media;
using C4I;
using Marvin.ClientFramework.Tests.Playground.Properties;

namespace Marvin.ClientFramework.Tests.Playground
{
    [ClientModule("Playground")]
    [ClassDisplay(Name = nameof(Strings.ModuleController_Playground), Description = "", ResourceType = typeof(Strings))]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        public override Geometry Icon => ShapeFactory.GetShapeGeometry(CommonShapeType.Cloud);

        protected override void OnInitialize()
        {
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