using System;

namespace Marvin.ClientFramework.History
{
    /// <summary>
    /// Event args containing all information for the workspace change
    /// </summary>
    public class WorkspacePair : EventArgs
    {
        /// <summary>
        /// Fill event args with workspace change arguments
        /// </summary>
        public WorkspacePair(IWorkspaceModule module, IModuleWorkspace workspace)
        {
            Module = module;
            Workspace = workspace;
        }

        /// <summary>
        /// Module hosting the new workspace
        /// </summary>
        public IWorkspaceModule Module { get; }

        /// <summary>
        /// Workspace instance for visu
        /// </summary>
        public IModuleWorkspace Workspace { get; }
    }
}