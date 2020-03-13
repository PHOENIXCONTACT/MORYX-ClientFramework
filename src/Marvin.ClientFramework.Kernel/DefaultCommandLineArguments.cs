// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using CommandLine;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Class for the command line parameters.
    /// </summary>
    public class DefaultCommandLineArguments
    {
        /// <summary>
        /// Gets or sets the configuration folder.
        /// </summary>
        [Option('c', "configFolder", HelpText = "Sets the config target folder.", Default = KernelConstants.CONFIGS_DIR)]
        public string ConfigFolder { get; set; }

        /// <summary>
        /// Indicates whether to start the configurator
        /// </summary>
        [Option('v', "configurator", HelpText = "Starts the configuration run mode.")]
        public bool StartConfigurator { get; set; }
    }
}
