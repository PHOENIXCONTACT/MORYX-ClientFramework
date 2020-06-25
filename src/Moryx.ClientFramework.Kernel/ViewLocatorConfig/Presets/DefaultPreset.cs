// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Preset for default view type
    /// </summary>
    [KernelComponent(typeof(IViewLocatorConfiguratorPreset))]
    public class DefaultPreset : IViewLocatorConfiguratorPreset
    {
        /// <inheritdoc />
        public string Name => "Default";

        /// <inheritdoc />
        public IEnumerable<string> ViewSuffixes => new string[] { };
    }
}
