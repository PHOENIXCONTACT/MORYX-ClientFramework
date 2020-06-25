// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Reflection;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Interface for assembly downloading components
    /// </summary>
    public interface IAssemblyFileLoader : IDisposable
    {
        /// <summary>
        /// Initializes the assembly file loader with an <see cref="Uri"/> and a <see cref="ProxyConfig"/>.
        /// </summary>
        void Initialize(Uri uri, ProxyConfig proxyConfig);

        /// <summary>
        /// Will start the download of the file and will trigger the <see cref="AssemblyDownloaded"/> event if it is done
        /// </summary>
        void DownloadFile();

        /// <summary>
        /// Occurs when assembly downloaded.
        /// </summary>
        event EventHandler<Assembly> AssemblyDownloaded;

        /// <summary>
        /// Occurs when assembly download failed
        /// </summary>
        event EventHandler<FailedDownload> AssemblyDownloadFailed;
    }
}
