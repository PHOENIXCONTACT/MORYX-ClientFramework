// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Linq;
using Moryx.Container;
using Moryx.Logging;
using LogLevel = Moryx.Logging.LogLevel;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Logger management
    /// </summary>
    [KernelComponent(typeof(ILoggerManagement))]
    public class ClientLoggerManagement : LoggerManagement
    {
        private LoggingConfig _config;

        /// <summary>
        /// Configuration manager instance. Injected by castel.
        /// </summary>
        public IKernelConfigManager ConfigManager { get; set; }

        /// <inheritdoc />
        protected override ILogTarget CreateLogTarget(string name)
        {
            return new CommonLoggingLogTarget(name);
        }

        /// <inheritdoc />
        protected override ModuleLoggerConfig GetLoggerConfig(string name)
        {
            var config = (_config = _config ?? ConfigManager.GetConfiguration<LoggingConfig>());
            var loggerConf = config.LoggerConfigs.FirstOrDefault(conf => conf.LoggerName == name);
            if (loggerConf == null)
            {
                loggerConf = new ModuleLoggerConfig { LoggerName = name, ActiveLevel = _config.DefaultLevel, ChildConfigs = new List<ModuleLoggerConfig>() };
                config.LoggerConfigs.Add(loggerConf);
            }
            return loggerConf;
        }

        /// <summary>
        /// Set the level of the given logger to the given log level.
        /// </summary>
        /// <param name="logger">The module logger for which the new log level should be set.</param>
        /// <param name="level">The new log level to which the given logger should be set.</param>
        public override void SetLevel(IModuleLogger logger, LogLevel level)
        {
            base.SetLevel(logger, level);
            ConfigManager.SaveConfiguration(_config);
        }

        /// <inheritdoc />
        protected override void ForwardToListeners(ILogMessage logMessage)
        {
        }
    }
}
