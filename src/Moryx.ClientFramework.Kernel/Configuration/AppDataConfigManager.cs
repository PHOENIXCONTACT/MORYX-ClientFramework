// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.IO;
using Moryx.Configuration;

namespace Moryx.ClientFramework.Kernel
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
        private const string AppDataDefaultsDirectory = "AppDataDefaults";

        /// <summary>
        /// Default application name if the name was not set
        /// </summary>
        private const string DefaultApplicationName = "HeartOfLead";

        /// <summary>
        /// Path to default configurations
        /// </summary>
        private readonly string _defaultsDirectory;

        /// <summary>
        /// Creates a new instance of the config manager
        /// </summary>
        /// <param name="application">Name of the application</param>
        /// <param name="defaultsBaseDirectory">Base config directory of this application</param>
        public AppDataConfigManager(string application, string defaultsBaseDirectory)
        {
            _defaultsDirectory = Path.Combine(defaultsBaseDirectory, AppDataDefaultsDirectory);

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

        /// <summary>
        /// Copies the all default config file from the <see cref="_defaultsDirectory"/>
        /// </summary>
        private void CopyDefaultConfigurations()
        {
            if (!Directory.Exists(_defaultsDirectory))
                return;

            foreach (var defaultFile in Directory.EnumerateFiles(_defaultsDirectory, $"*{ConfigConstants.FileExtension}"))
            {
                var fileInfo = new FileInfo(defaultFile);
                var targetFilePath = Path.Combine(ConfigDirectory, fileInfo.Name);

                if (!File.Exists(targetFilePath))
                {
                    File.Copy(defaultFile, targetFilePath);
                }
            }
        }
    }
}
