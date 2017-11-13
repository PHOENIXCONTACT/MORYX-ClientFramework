using System;
using System.IO;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Constants for the framework kernal
    /// </summary>
    public static class KernelConstants
    {
        // ReSharper disable InconsistentNaming

        /// <summary>
        /// The path of the default configuration folder
        /// </summary>
        public const string CONFIGS_DIR = "Config";

        /// <summary>
        /// Path of the folder for crash logs
        /// </summary>
        public const string CRASHLOGS_DIR = "Crashlogs";

        /// <summary>
        /// Name of the configurator run mode
        /// </summary>
        public const string CONFIG_RUNMODE = "Config_RunMode";

        /// <summary>
        /// The folder to the specialiced application data 
        /// </summary>
        public static string AppData_Dir(string appName)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, "Phoenix Contact", appName);
        }
    }
}