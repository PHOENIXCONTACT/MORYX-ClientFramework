using Marvin.Configuration;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// ConfigManager for the ClientFramework
    /// Will store configuration in the runtime config folder.
    /// </summary>
    public interface IKernelConfigManager : IConfigManager
    {
        /// <summary>
        /// Gets directory where the configurations were stored.
        /// </summary>
        string ConfigDirectory { get; }

        /// <summary>
        /// Method to trigger that all configurations should be saved
        /// </summary>
        void SaveAll();
    }
}