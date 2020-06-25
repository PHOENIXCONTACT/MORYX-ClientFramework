// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
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
