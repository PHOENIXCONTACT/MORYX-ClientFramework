using System;
using System.Collections.Generic;
using System.Reflection;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
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
