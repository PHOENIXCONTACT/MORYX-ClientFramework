// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Config for a single assembly loaded by the <see cref="IRunMode"/>
    /// </summary>
    public class AssemblyConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyConfig"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public AssemblyConfig(string assembly)
        {
            Assembly = assembly;
        }

        /// <summary>
        /// Gets or sets the assembly.
        /// </summary>
        public string Assembly { get; set; }
    }
}
