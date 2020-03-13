// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using Marvin.ClientFramework.Shell;
using Marvin.Container;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Attribute for the module shell
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleShellAttribute : GlobalComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModuleShellAttribute"/> class.
        /// </summary>
        public ModuleShellAttribute(string name) : base(LifeCycle.Singleton, typeof(IModuleShell))
        {
            Name = name;
        }
    }
}
