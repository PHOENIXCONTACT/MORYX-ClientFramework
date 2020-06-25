// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Linq;
using Moryx.ClientFramework.Shell;
using Moryx.Configuration;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Configuration provider for modules to save their configs
    /// </summary>
    public class LocalConfigProvider : IConfigProvider
    {
        private readonly ModulesConfiguration _modulesConfiguration;
        private readonly IConfigManager _configManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocalConfigProvider"/> class.
        /// </summary>
        public LocalConfigProvider(IConfigManager configManager, ModulesConfiguration modulesConfiguration)
        {
            _modulesConfiguration = modulesConfiguration;
            _configManager = configManager;
        }

        /// 
        public T GetModuleConfiguration<T>(string name) where T : class, IClientModuleConfig, new()
        {
            var config = GetConfiguration<T>();

            var module = _modulesConfiguration.Modules.First(m => m.ModuleName.Equals(name));
            module.CopyTo(config);

            return config;
        }

        /// <inheritdoc />
        public T GetShellConfig<T>() where T : class, IShellRegionConfig, new()
        {
            return GetConfiguration<T>();
        }

        /// <inheritdoc />
        public T GetConfiguration<T>() where T : class, IConfig, new()
        {
            return _configManager.GetConfiguration<T>();
        }

        /// <inheritdoc />
        public T GetConfiguration<T>(string name) where T : class, IConfig, new()
        {
            return _configManager.GetConfiguration<T>(name);
        }

        /// <inheritdoc />
        public T GetConfiguration<T>(bool getCopy) where T : class, IConfig, new()
        {
            return _configManager.GetConfiguration<T>(getCopy);
        }

        /// <inheritdoc />
        public T GetConfiguration<T>(bool getCopy, string name) where T : class, IConfig, new()
        {
            return _configManager.GetConfiguration<T>(getCopy, name);
        }

        /// <inheritdoc />
        public void SaveConfiguration<T>(T configuration) where T : class, IConfig
        {
            _configManager.SaveConfiguration(configuration);
        }

        /// <inheritdoc />
        public void SaveConfiguration<T>(T configuration, string name) where T : class, IConfig
        {
            _configManager.SaveConfiguration(configuration, name);
        }
    }
}
