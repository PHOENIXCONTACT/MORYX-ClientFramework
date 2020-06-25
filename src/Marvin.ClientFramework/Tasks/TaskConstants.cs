using System.Threading.Tasks;

namespace Marvin.ClientFramework.Tasks
{
    /// <summary>
    /// Constants for Task operations
    /// </summary>
    internal static class TaskConstants
    {
        // save completed task staticaly to save time
        private static readonly Task Completed = Task.FromResult(false);

        /// <summary>
        /// Returns a <see cref="System.Threading.Tasks.Task"/> which is allready completed
        /// </summary>
        public static Task CompletedTask()
        {
            return Completed;
        }
    }
}