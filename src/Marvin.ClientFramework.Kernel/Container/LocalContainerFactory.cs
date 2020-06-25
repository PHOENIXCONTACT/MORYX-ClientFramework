using System;
using System.Collections.Generic;
using System.Reflection;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
{
    [KernelComponent(typeof(IModuleContainerFactory))]
    public class LocalContainerFactory : IModuleContainerFactory
    {
        public IContainer Create(IDictionary<Type, string> strategies, Assembly moduleAssembly)
        {
            return new LocalContainer()
                .ExecuteInstaller(new AutoInstaller(moduleAssembly));
        }
    }
}
