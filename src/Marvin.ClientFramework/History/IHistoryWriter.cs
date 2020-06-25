namespace Marvin.ClientFramework.History
{
    /// <summary>
    /// Interface for a history writing component. Usually the writer will be used by a view model to push
    /// </summary>
    public interface IHistoryWriter
    {
        /// <summary>
        /// Push a module onto the history stack with a specific screen
        /// </summary>
        void Push(IModuleWorkspace workspace);

        /// <summary>
        /// Module tries to push itself onto the stack as result of a server side notification
        /// </summary>
        /// <returns>True if screen was pushed - false if enqueued!</returns>
        bool TryPush(IModuleWorkspace workspace);

        /// <summary>
        /// Remove a pushed screen from the queue because it is no longer necessary
        /// </summary>
        void RemovePush(IModuleWorkspace workspace);

        /// <summary>
        /// Go back in time to
        /// </summary>
        void Reverse();
    }
}