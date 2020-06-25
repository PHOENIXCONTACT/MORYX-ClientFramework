// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Configures the <see cref="Caliburn.Micro.ViewLocator"/>. User can define a collection of sets and switch
    /// between different sets to configure which views shall be shown for a specific view model.
    /// </summary>
    public interface IViewLocatorConfigurator
    {
        /// <summary>
        /// Currently activated set.
        /// </summary>
        string ActivatedSet { get; }

        /// <summary>
        /// Activates a set by the given name.
        /// </summary>
        void ActivateSet(string setName);
    }
}
