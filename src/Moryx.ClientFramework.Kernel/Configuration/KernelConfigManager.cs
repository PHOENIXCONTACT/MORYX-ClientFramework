// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.Configuration;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Derivative of the CachedConfigManager from platform to handle
    /// all kernel configuration stored in the application folder
    /// </summary>
    internal class KernelConfigManager : CachedConfigManager, IKernelConfigManager
    {

    }
}
