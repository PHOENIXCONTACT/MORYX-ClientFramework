// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using Marvin.Container;
using Marvin.Logging;

namespace Marvin.ClientFramework.Kernel
{
    internal class DummyLogTarget : ILogTarget
    {
        public void Log(LogLevel loglevel, string message)
        {
            
        }

        public void Log(LogLevel loglevel, string message, Exception exception)
        {
            
        }
    }

    /// <summary>
    /// Logger management
    /// </summary>
    [KernelComponent(typeof(ILoggerManagement))]
    public class ClientLoggerManagement : LoggerManagement
    {
        /// <inheritdoc />
        protected override ILogTarget CreateLogTarget(string name)
        {
            return new DummyLogTarget();
        }

        /// <inheritdoc />
        protected override ModuleLoggerConfig GetLoggerConfig(string name)
        {
            return new ModuleLoggerConfig
            {
                LoggerName = name,
                ActiveLevel = LogLevel.Debug,
                ChildConfigs = new List<ModuleLoggerConfig>()
            };
        }

        /// <inheritdoc />
        protected override void ForwardToListeners(ILogMessage logMessage)
        {
            Debug.WriteLine("Level: {0}, Name: {1}, Class: {2} -> {3}", logMessage.Level, logMessage.Logger.Name,
                logMessage.ClassName, logMessage.Message);

            if (logMessage.Level > LogLevel.Warning)
            {
                Debug.WriteLine("Logger manager shoud forward to server log... Error level > Warning!!!!!");
            }
        }
    }
}
