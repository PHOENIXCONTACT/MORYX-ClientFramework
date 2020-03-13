// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Will hold the complete assembly configuration of the loaded
    /// assemblies by the <see cref="IRunMode"/>
    /// </summary>
    public class AssemblyConfiguration
    {
        /// <summary>
        /// Gets or sets the assemblies.
        /// </summary>
        public List<AssemblyConfig> Assemblies  { get; set; }
    }
}
