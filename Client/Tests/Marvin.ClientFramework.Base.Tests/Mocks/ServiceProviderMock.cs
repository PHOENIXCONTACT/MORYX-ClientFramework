using System;

namespace Marvin.ClientFramework.Base.Tests
{
    internal class ServiceProviderMock : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }
    }
}