using System.ComponentModel;

namespace Marvin.ClientFramework.Shell
{
    /// <summary>
    /// Interface for all plugins displayed in the shell
    /// </summary>
    public interface IShellRegion : INotifyPropertyChanged
    {
        /// <summary>
        /// Connect this plugin with the shell
        /// </summary>
        /// <param name="shell">Parent instance</param>
        void ConnectToShell(ShellViewModelBase shell);
    }
}