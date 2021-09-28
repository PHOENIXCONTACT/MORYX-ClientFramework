using System;
using System.IO;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Constants for the framework kernel
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
        /// Name of the manufacturer company
        /// </summary>
        public const string COMPANY_NAME = "Phoenix Contact";

        /// <summary>
        /// The folder to the specialized application data
        /// </summary>
        public static string AppData_Dir(string appName)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, COMPANY_NAME, appName);
        }
    }
}