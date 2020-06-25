using Caliburn.Micro;

namespace Marvin.ClientFramework.Behavior
{
    /// <summary>
    /// Interface for modules and plugins which are 
    /// indicating that they have a running shutdown task
    /// </summary>
    public interface IHaveShutdownTask
    {
        /// <summary>
        /// Gets the shutdown task.
        /// </summary>
        IResult GetShutdownTask();
    }
}
