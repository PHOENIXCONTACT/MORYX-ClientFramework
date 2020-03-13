// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using Marvin.Container;

namespace Marvin.ClientFramework.Configurator
{
    /// <summary>
    /// Base attribute for configuration view model registrations
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigViewModelPluginAttribute : PluginAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigViewModelPluginAttribute"/> class.
        /// </summary>
        public ConfigViewModelPluginAttribute() : base(LifeCycle.Singleton, typeof(IConfigViewModel))
        {
        }
    }
}
