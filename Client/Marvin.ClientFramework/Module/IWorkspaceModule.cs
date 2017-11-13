using System.Windows.Media;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Client modules that display content on the workspace
    /// </summary>
    public interface IWorkspaceModule : IClientModule
    {
        /// <summary>
        /// Icon for the module
        /// </summary>
        Geometry Icon { get; }

        /// <summary>
        /// Gets a value indicating whether the module is visible in the shell navigation.
        /// </summary>
        bool HasButton { get; }

        /// <summary>
        /// Creates a Workspace.
        /// </summary>
        IModuleWorkspace CreateWorkspace();

        /// <summary>
        /// Destroys a Workspace.
        /// </summary>
        void DestroyWorkspace(IModuleWorkspace workspace);
    }
}