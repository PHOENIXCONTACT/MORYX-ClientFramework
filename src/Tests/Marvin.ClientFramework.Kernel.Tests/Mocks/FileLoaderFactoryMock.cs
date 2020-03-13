// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework.Kernel.Tests
{
    internal class FileLoaderFactoryMock : IAssemblyFileLoaderFactory
    {
        private readonly bool _good;

        public FileLoaderFactoryMock(bool good)
        {
            _good = good;
        }

        public IAssemblyFileLoader Create()
        {
            if (_good)
            {
                return new FileLoaderGoodMock();
            }
            return new FileLoaderBadMock();
        }
    }
}
