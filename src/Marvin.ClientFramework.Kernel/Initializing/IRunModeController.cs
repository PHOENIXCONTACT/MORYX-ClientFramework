// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Controller to initialize the configured runmode and
    /// start the frameworks initialization phase
    /// </summary>
    public interface IRunModeController
    {
        /// <summary>
        /// Initializes the runmode handler with an config manager to initialize the config provider
        /// </summary>
        void Initialize();

        /// <summary>
        /// The current running runmode.
        /// </summary>
        IRunMode Current { get; }

        /// <summary>
        /// Starts the configured runmode to do the initializing phase
        /// </summary>
        void Start();

        /// <summary>
        /// Occurs when all assemblies are downloaded and the configuration was created
        /// </summary>
        event EventHandler<ModulesConfiguration> RunModeReady;
    }
}
