// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading.Tasks;
using Caliburn.Micro;

namespace Moryx.ClientFramework.Shell
{
    /// <summary>
    /// Interface for the module shell.
    /// The module shell will show the whole module and will watch their lifecycle
    /// </summary>
    public interface IModuleShell : IConductor, IScreen
    {
        /// <summary>
        /// Initializes the shell.
        /// </summary>
        Task InitializeAsync();
    }
}
