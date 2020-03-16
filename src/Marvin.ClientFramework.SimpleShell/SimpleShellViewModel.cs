using System.Linq;
using Marvin.ClientFramework.Shell;

namespace Marvin.ClientFramework.SimpleShell
{
    /// <summary>
    /// Simple shell for hosting client modules
    /// </summary>
    [ModuleShell("SimpleShell")]
    public class SimpleShellViewModel : ShellViewModelBase
    {
        /// <inheritdoc />
        protected override void OnInitialize()
        {
            SelectModule(Items.First());
        }

        /// <summary>
        /// Creates the controller.
        /// </summary>
        protected override IShellRegionController CreateController()
        {
            return new NullRegionController();
        }
    }
}