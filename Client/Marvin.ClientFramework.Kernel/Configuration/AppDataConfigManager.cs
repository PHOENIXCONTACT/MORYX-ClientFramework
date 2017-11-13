using System.IO;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// ConfigManager specialized to save configuration in the 
    /// AppData folder of a user
    /// </summary>
    public class AppDataConfigManager : KernelConfigManager, IAppDataConfigManager
    {
        /// <summary>
        /// Initializes the configmanager and will create a folder in the users AppData_Dir
        /// </summary>
        public void Initialize(string application)
        {
            var appName = application;

            if (string.IsNullOrEmpty(appName))
            {
                appName = "HeartOfLead";
            }

            ConfigDirectory = KernelConstants.AppData_Dir(appName);

            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }
        }
    }
}