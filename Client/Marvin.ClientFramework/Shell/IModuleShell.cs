using Caliburn.Micro;

namespace Marvin.ClientFramework.Shell
{
    /// <summary>
    /// Interface for the module shell.
    /// The module shell will show the whole module and will watch their lifecycle
    /// </summary>
    public interface IModuleShell : IConductor, IScreen
    {
        /// <summary>
        /// Initializes the shell.
        /// </summary>
        void Initialize();
    }
}