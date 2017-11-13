namespace Marvin.ClientFramework
{
    /// <summary>
    /// ConfigManager specialized to save configuration in the 
    /// AppData folder of a user
    /// </summary>
    public interface IAppDataConfigManager : IKernelConfigManager
    {
        /// <summary>
        /// Initializes the specified core configuration manager.
        /// </summary>
        void Initialize(string application);
    }
}