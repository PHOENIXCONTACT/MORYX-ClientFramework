// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Defines that the component is for a special runMode
    /// </summary>
    public class ComponentForRunModeAttribute : Attribute
    {
        /// <summary>
        /// Defines the run mode for this client module
        /// </summary>
        public string RunMode { get; }

        /// <summary>
        /// Creates a new instance of <see cref="ComponentForRunModeAttribute"/>
        /// </summary>
        /// <param name="runMode">The runmode for the component</param>
        public ComponentForRunModeAttribute(string runMode)
        {
            RunMode = runMode;
        }
    }
}
