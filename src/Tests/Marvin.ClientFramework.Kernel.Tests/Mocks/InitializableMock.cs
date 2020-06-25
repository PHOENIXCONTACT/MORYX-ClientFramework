using System;
using System.Threading;
using Marvin.Modules;

namespace Marvin.ClientFramework.Kernel.Tests
{
    internal class InitializableMock : IInitializable
    {
        private readonly bool _throwException;

        public InitializableMock(bool throwException)
        {
            _throwException = throwException;
        }

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            if (_throwException)
            {
                throw new TypeInitializationException(this.GetType().Name, new Exception("Some exception"));
            }

            Thread.Sleep(100);
            IsInitialized = true;
        }
    }
}