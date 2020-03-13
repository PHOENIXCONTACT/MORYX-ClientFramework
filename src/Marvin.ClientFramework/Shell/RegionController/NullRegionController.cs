// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using Marvin.Container;

namespace Marvin.ClientFramework.Shell
{
    /// <summary>
    /// Null region controller if you do not use regions in the shell
    /// </summary>
    public class NullRegionController : IShellRegionController
    {
        ///
        public void Initialize(IModuleContainerFactory containerFactory, IModuleManager manager, IConfigProvider provider)
        {

        }

        ///
        public void Register<T>(T component) where T : class
        {
            
        }

        /// 
        public Region FetchRegion(string name)
        {
            return new Region(Visibility.Collapsed, null);
        }
    }
}
