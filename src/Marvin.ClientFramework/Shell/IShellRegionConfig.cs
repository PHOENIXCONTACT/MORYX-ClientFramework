// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using Marvin.Configuration;

namespace Marvin.ClientFramework.Shell
{
    /// <summary>
    /// Config for the regions provided by the shell.
    /// With this configurations regions will be enabled, disabled and replaced
    /// </summary>
    public interface IShellRegionConfig : IConfig
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IShellRegionConfig"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        bool Initialized { get; set; }

        /// <summary>
        /// List of regions with their configuration
        /// </summary>
        List<RegionConfig> Regions { get; set; }
    }
}
