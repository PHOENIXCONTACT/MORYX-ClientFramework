// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Runtime.Serialization;
using Moryx.Configuration;

namespace Moryx.ClientFramework.Shell
{
    /// <summary>
    /// Configuration for a shell region
    /// </summary>
    [DataContract]
    public class ShellRegionConfig : ConfigBase, IShellRegionConfig
    {
        /// <summary>
        /// Configured regions
        /// </summary>
        [DataMember]
        public List<RegionConfig> Regions { get; set; }

        /// <summary>
        /// Indicates whether the region is initialized
        /// </summary>
        [DataMember]
        public bool Initialized { get; set; }
    }
}
