// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Threading.Tasks;
using Moryx.Modules;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Interface for all client modules running within the HeartOfLead client framework.
    /// Connected to their partner server module a client module provides the visualization and interaction for the user.
    /// Client modules might connect to multiple server modules and vice versa.
    /// </summary>
    public interface IClientModule : IDisposable
    {
        /// <summary>
        /// Unique name for this module within the platform it is designed for
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Notifications published by this module
        /// </summary>
        INotificationCollection Notifications { get; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        IClientModuleConfig Config { get; }

        /// <summary>
        /// Initializes the client module
        /// </summary>
        Task InitializeAsync();

        /// <summary>
        /// Activates the client module
        /// </summary>
        Task ActivateAsync();

        /// <summary>
        /// Deactivates the client module
        /// </summary>
        Task DeactivateAsync(bool close);
    }
}
