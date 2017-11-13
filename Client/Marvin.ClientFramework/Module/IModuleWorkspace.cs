using System;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Screen instance for the viewport
    /// </summary>
    public interface IModuleWorkspace
    {
        /// <summary>
        /// Current interaction state of the screen
        /// </summary>
        WorkspaceInteraction CurrentInteraction { get; }

        /// <summary>
        /// Event raised when the current interaction type changes
        /// </summary>
        event EventHandler<WorkspaceInteraction> InteractionChanged;
    }
}
