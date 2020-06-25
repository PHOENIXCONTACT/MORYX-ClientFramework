// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Linq;
using Moryx.Container;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Attribute for the modules of the framework
    /// This attribute will be used for register client modules at the framework
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ClientModuleAttribute : GlobalComponentAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientModuleAttribute" /> class.
        /// </summary>
        /// <param name="name">The name of the module</param>
        /// <param name="facades">Proviced facades from this module</param>
        public ClientModuleAttribute(string name, params Type[] facades) 
            : base(LifeCycle.Singleton, new []{ typeof(IClientModule) }.Union(facades).ToArray())
        {
            Name = name;
        }
    }
}
