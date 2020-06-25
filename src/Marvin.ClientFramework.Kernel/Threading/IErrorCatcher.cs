using System.Windows.Threading;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Interface for error catcher and handler
    /// </summary>
    public interface IErrorCatcher
    {
        /// <summary>
        /// Handles the unhandled dispatcher exceptions
        /// </summary>
        void HandleDispatcherException(object sender, DispatcherUnhandledExceptionEventArgs args);
    }
}