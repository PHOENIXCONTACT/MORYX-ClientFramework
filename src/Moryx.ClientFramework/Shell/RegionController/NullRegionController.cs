// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using Moryx.Container;

namespace Moryx.ClientFramework.Shell
{
    /// <summary>
    /// Null region controller if you do not use regions in the shell
    /// </summary>
    public class NullRegionController : IShellRegionController
    {
        /// <inheritdoc />
        public void Initialize(IModuleContainerFactory containerFactory, IModuleManager manager, IConfigProvider provider)
        {

        }

        /// <inheritdoc />
        public void Register<T>(T component) where T : class
        {

        }

        /// <inheritdoc />
        public Region FetchRegion(string name)
        {
            return new Region(Visibility.Collapsed, null);
        }
    }
}
