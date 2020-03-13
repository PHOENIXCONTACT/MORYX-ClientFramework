// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Interface for assembly downloader
    /// Possible implementations: Folder, Web, Network
    /// </summary>
    public interface IAssemblyMultiLoader : IDisposable
    {
        /// <summary>
        /// Load will start the download of the assemblies
        /// <see cref="AssembliesDownloaded"/> will be raised if all assemblies were downloaded
        /// </summary>
        void Load(IEnumerable<Uri> uris);

        /// <summary>
        /// Occurs when assemblies downloaded.
        /// </summary>
        event EventHandler<ReadOnlyCollection<Assembly>> AssembliesDownloaded;

        /// <summary>
        /// Occurs when assembly download failed.
        /// </summary>
        event EventHandler<IEnumerable<FailedDownload>> AssembliesDownloadFailed;

        /// <summary>
        /// Occurs when a assembly was downloaded
        /// </summary>
        event EventHandler<Assembly> AssemblyDownloaded;

        /// <summary>
        /// Occurs when a assembly download started
        /// </summary>
        event EventHandler<IEnumerable<Uri>> AssemblyDownloadStarted;
    }
}
