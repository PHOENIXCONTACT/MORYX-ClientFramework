// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Threading;
using Moryx.Modules;

namespace Moryx.ClientFramework.Kernel.Tests
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
