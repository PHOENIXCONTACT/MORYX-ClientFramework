using System.IO;
using Marvin.Configuration;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// ConfigManager specialized to save configuration in the 
    /// AppData folder of a user
    /// </summary>
    public class AppDataConfigManager : KernelConfigManager, IAppDataConfigManager
    {
        /// <summary>
        /// Name of default's sub folder
        /// </summary>
        public const string AppDataDefaultsDirectoryName = "AppDataDefaults";

        private const string DefaultApplicationName = "HeartOfLead";

        /// <summary>
        /// Path to default configurations
        /// </summary>
        public string AppDataConfigDefaultsDir { get; set; }

        /// <summary>
        /// Initializes the configmanager and will create a folder in the users AppData_Dir
        /// </summary>
        public void Initialize(string application)
        {
            var appName = application;

            if (string.IsNullOrEmpty(appName))
            {
                appName = DefaultApplicationName;
            }

            ConfigDirectory = KernelConstants.AppData_Dir(appName);

            if (!Directory.Exists(ConfigDirectory))
            {
                Directory.CreateDirectory(ConfigDirectory);
            }

            CopyDefaultConfigurations();
        }

        private void CopyDefaultConfigurations()
        {
            if (Directory.Exists(AppDataConfigDefaultsDir))
            {
                foreach (var configFile in Directory.EnumerateFiles(AppDataConfigDefaultsDir, $"*{ConfigConstants.FileExtension}"))
                {
                    var fileInfo = new FileInfo(configFile);
                    var targetFilePath = Path.Combine(ConfigDirectory, fileInfo.Name);

                    if (!File.Exists(targetFilePath))
                    {
                        File.Copy(configFile, targetFilePath);
                    }
                }
            }
        }
    }
}