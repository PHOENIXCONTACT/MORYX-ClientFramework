using System.Windows.Media;
using Marvin.ClientFramework;
using Marvin.Tools.WcfClient.UI.Viewer.Properties;

namespace Marvin.Tools.WcfClient.UI.Viewer
{
    /// <inheritdoc />
    /// <summary>
    /// Module controller for wcf viewer
    /// </summary>
    [ClientModule("Wcf Viewer")]
    public sealed class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        /// <inheritdoc />
        public override Geometry Icon => Geometry.Parse(
            "M69.05,58.1c-4.8,0-9.1,2.3-11.8,5.8l-24.3-14.1c1.5-3.7,1.5-7.8,0-11.5l24.3-14.1c2.7,3.5,7,5.8,11.8,5.8" +
            "c8.3,0,15-6.7,15-15s-6.7-15-15-15s-15,6.7-15,15c0,2,0.4,4,1.1,5.7l-24.3,14.2c-2.8-3.5-7-5.8-11.8-5.8c-8.3,0-15,6.7-15,15" +
            "s6.7,15,15,15c4.8,0,9.1-2.3,11.8-5.8l24.3,14.1c-0.7,1.7-1.1,3.7-1.1,5.7c0,8.3,6.7,15,15,15s15-6.7,15-15S77.35,58.1,69.05,58.1z" +
            "M69.05,4.1c6.1,0,11,4.9,11,11s-4.9,11-11,11c-6.1,0-11-4.9-11-11S62.95,4.1,69.05,4.1z M19.05,55.1c-6.1,0-11-4.9-11-11" +
            "s4.9-11,11-11s11,4.9,11,11S25.15,55.1,19.05,55.1z M69.05,84.1c-6.1,0-11-4.9-11-11s4.9-11,11-11c6.1,0,11,4.9,11,11" +
            "S75.15,84.1,69.05,84.1z");

        /// <inheritdoc />
        protected override void OnInitialize()
        {
            DisplayName = strings.Title;
        }

        /// <inheritdoc />
        protected override void OnActivate()
        {

        }

        /// <inheritdoc />
        protected override void OnDeactivate(bool close)
        {
        }

        /// <inheritdoc />
        protected override IModuleWorkspace OnCreateWorkspace()
        {
            return Container.Resolve<IModuleWorkspace>(nameof(WcfClientViewerWorkspaceViewModel));
        }

        /// <inheritdoc />
        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {
        }
    }
}