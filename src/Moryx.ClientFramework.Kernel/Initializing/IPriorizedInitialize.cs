// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.Modules;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Runlevel for the <see cref="IPriorizedInitialize"/> to set the priority
    /// </summary>
    public enum RunLevel
    {
        /// <summary>
        /// Runlevel 0
        /// </summary>
        R0,

        /// <summary>
        /// Runlevel 1
        /// </summary>
        R1,

        /// <summary>
        /// Runlevel 2
        /// </summary>
        R2,

        /// <summary>
        /// Runlevel 3
        /// </summary>
        R3,

        /// <summary>
        /// Runlevel 4
        /// </summary>
        R4,
    }

    /// <summary>
    /// Inizializable wich can set a priority for the initialization process
    /// </summary>
    public interface IPriorizedInitialize : IInitializable
    {
        /// <summary>
        /// Sets an runlevel for the initialization phase
        /// </summary>
        RunLevel RunLevel { get; }
    }
}
