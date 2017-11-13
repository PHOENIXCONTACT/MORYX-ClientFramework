using System;

namespace Marvin.ClientFramework.History
{
    /// <summary>
    /// Interface for the central history component
    /// </summary>
    public interface IHistory
    {
        /// <summary>
        /// Push a module onto the history stack creating a new default screen
        /// </summary>
        void Push(IWorkspaceModule module);

        /// <summary>
        /// Push a module onto the history stack with a specific workspace
        /// </summary>
        void Push(IWorkspaceModule module, IModuleWorkspace workspace);

        /// <summary>
        /// Module tries to push itself onto the stack as result of a server side notification
        /// </summary>
        /// <returns>True if screen was pushed - false if enqueued!</returns>
        bool TryPush(IWorkspaceModule module, IModuleWorkspace workspace);

        /// <summary>
        /// Remove a pushed screen from the queue because it is no longer necessary
        /// </summary>
        void RemovePush(IWorkspaceModule module, IModuleWorkspace workspace);

        /// <summary>
        /// Move forward in the history
        /// </summary>
        bool MoveNext();

        /// <summary>
        /// Move to the previous element
        /// </summary>
        bool MovePrevious();

        /// <summary>
        /// Current screen with its module
        /// </summary>
        WorkspacePair Current { get; }

        /// <summary>
        /// Event raised when the current screen changes
        /// </summary>
        event EventHandler<WorkspacePair> WorkspaceChanged;
    }
}