// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using Marvin.Modules;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Interface for all client modules running within the HeartOfLead client framework.
    /// Connected to their partner server module a client module provides the visualization and interaction for the user. 
    /// Client modules might connect to multiple server modules and vice versa. 
    /// </summary>
    public interface IClientModule : IModule, IDisposable
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        IClientModuleConfig Config { get; }

        /// <summary>
        /// Activates the client module
        /// </summary>
        void Activate();

        /// <summary>
        /// Deactivates the client module
        /// </summary>
        void Deactivate(bool close);
    }
}
