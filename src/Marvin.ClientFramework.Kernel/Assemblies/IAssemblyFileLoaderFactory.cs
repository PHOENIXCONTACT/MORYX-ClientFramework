// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Interface for creating AssemblyFileLoader
    /// </summary>
    public interface IAssemblyFileLoaderFactory
    {
        /// <summary>
        /// Creates a instance of <see cref="IAssemblyFileLoader"/>.
        /// </summary>
        IAssemblyFileLoader Create();
    }
}
