// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Factory to instantiate HTTP Assembly File Loader
    /// </summary>
    internal class HttpAssemblyFileLoaderFactory : IAssemblyFileLoaderFactory
    {
        public IAssemblyFileLoader Create()
        {
            return new HttpAssemblyLoader();
        }
    }
}
