// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Defines a named preset for the <see cref="IViewLocatorConfigurator"/>
    /// </summary>
    public interface IViewLocatorConfiguratorPreset
    {
        /// <summary>
        /// Default name of this preset
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A list of view suffixes used by the <see cref="Caliburn.Micro.ViewLocator"/>. Note that the resolving strategy is last wins.
        /// </summary>
        IEnumerable<string> ViewSuffixes { get; }
    }
}
