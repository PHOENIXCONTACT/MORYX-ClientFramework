// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Preset for touch view type
    /// </summary>
    [KernelComponent(typeof(IViewLocatorConfiguratorPreset))]
    public class TouchPreset : IViewLocatorConfiguratorPreset
    {
        /// <inheritdoc />
        public string Name => "Touch";

        /// <inheritdoc />
        public IEnumerable<string> ViewSuffixes => new[] {"Touch"};
    }
}
