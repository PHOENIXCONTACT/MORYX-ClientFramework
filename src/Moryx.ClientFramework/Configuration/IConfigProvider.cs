// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.ClientFramework.Shell;
using Moryx.Configuration;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Interface for config providers used in module structure
    /// This provider should not be used in the kernel
    /// </summary>
    public interface IConfigProvider : IConfigManager
    {
        /// <summary>
        /// Gets the module configuration by the module name
        /// </summary>
        T GetModuleConfiguration<T>(string name) where T : class, IClientModuleConfig, new();

        /// <summary>
        /// Gets the shell configuration by the shell type.
        /// </summary>
        T GetShellConfig<T>() where T : class, IShellRegionConfig, new();
    }
}
