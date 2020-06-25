// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Reflection;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <inheritdoc />
    /// <summary>
    /// Local container factory
    /// </summary>
    [KernelComponent(typeof(IModuleContainerFactory))]
    public class LocalContainerFactory : IModuleContainerFactory
    {
        /// <inheritdoc />
        public IContainer Create(IDictionary<Type, string> strategies, Assembly moduleAssembly)
        {
            return new LocalContainer()
                .ExecuteInstaller(new AutoInstaller(moduleAssembly));
        }
    }
}
