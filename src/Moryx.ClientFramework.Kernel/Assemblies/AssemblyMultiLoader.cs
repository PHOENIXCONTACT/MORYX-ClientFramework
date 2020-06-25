// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// The <see cref="AssemblyMultiLoader"/> will download multiple 
    /// assemblies with a <see cref="IAssemblyFileLoader"/>
    /// </summary>
    [GlobalComponent(LifeCycle.Transient, typeof (IAssemblyMultiLoader))]
    public class AssemblyMultiLoader : IAssemblyMultiLoader
    {
        #region Dependency Injection

        /// <summary>
        /// Gets or sets the core configuration manager.
        /// </summary>
        public IKernelConfigManager KernelConfigManager { get; set; }

        #endregion

        #region Fields and Properties

        private readonly List<IAssemblyFileLoader> _assemblyLoaders = new List<IAssemblyFileLoader>();
        private readonly List<FailedDownload> _failedAssemblies = new List<FailedDownload>();
        private readonly List<Assembly> _internalLoadedAssemblies = new List<Assembly>();
        private IEnumerable<Uri> _urisToLoad;

        /// <summary>
        /// Gets or sets the loader factory.
        /// The factory will be used to instantiate assembly loader for earch uri
        /// </summary>
        public IAssemblyFileLoaderFactory LoaderFactory { get; set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyMultiLoader"/> class.
        /// </summary>
        public AssemblyMultiLoader()
        {
            //Using HttpAssemblyLoader as Default
            LoaderFactory = new HttpAssemblyFileLoaderFactory();
        }
        
        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _urisToLoad = null;
        }

        ///
        public void Load(IEnumerable<Uri> uris)
        {
            _urisToLoad = uris.ToList();

            var proxyConfig = KernelConfigManager.GetConfiguration<ProxyConfig>();

            if (AssemblyDownloadStarted != null)
                AssemblyDownloadStarted(this, _urisToLoad);

            if (_urisToLoad.Any())
            {
                foreach (var uri in _urisToLoad)
                {
                    var assemblyLoader = LoaderFactory.Create();
                    assemblyLoader.Initialize(uri, proxyConfig);
                    assemblyLoader.AssemblyDownloaded += OnAssemblyDownloaded;
                    assemblyLoader.AssemblyDownloadFailed += OnAssemblyDownloadFailed;

                    _assemblyLoaders.Add(assemblyLoader);
                }
            }
            else
            {
                if (AssembliesDownloaded != null)
                    AssembliesDownloaded(this, new ReadOnlyCollection<Assembly>(_internalLoadedAssemblies));
            }

            foreach (var assemblyLoader in _assemblyLoaders.ToList())
            {
                assemblyLoader.DownloadFile();
            }
        }

        ///
        public event EventHandler<ReadOnlyCollection<Assembly>> AssembliesDownloaded;

        ///
        public event EventHandler<IEnumerable<FailedDownload>> AssembliesDownloadFailed;

        ///
        public event EventHandler<Assembly> AssemblyDownloaded;

        ///
        public event EventHandler<IEnumerable<Uri>> AssemblyDownloadStarted;

        /// <summary>
        /// Called when assembly downloaded.
        /// </summary>
        private void OnAssemblyDownloaded(object sender, Assembly assembly)
        {
            var loader = (IAssemblyFileLoader)sender;

            if (AssemblyDownloaded != null)
                AssemblyDownloaded(this, assembly);

            _internalLoadedAssemblies.Add(assembly);

            lock (_assemblyLoaders)
            {
                loader.AssemblyDownloaded -= OnAssemblyDownloaded;
                loader.AssemblyDownloadFailed -= OnAssemblyDownloadFailed;

                _assemblyLoaders.Remove(loader);

                loader.Dispose();

                if (_assemblyLoaders.Count == 0 & AssembliesDownloaded != null)
                    AssembliesDownloaded(this, new ReadOnlyCollection<Assembly>(_internalLoadedAssemblies));
            }
        }

        /// <summary>
        /// Called when assembly download failed.
        /// </summary>
        private void OnAssemblyDownloadFailed(object sender, FailedDownload failedDownload)
        {
            _failedAssemblies.Add(failedDownload);

            var loader = (IAssemblyFileLoader)sender;

            lock (_assemblyLoaders)
            {
                _assemblyLoaders.Remove(loader);
                loader.Dispose();

                if (_assemblyLoaders.Count == 0 & AssembliesDownloadFailed != null)
                    AssembliesDownloadFailed(this, _failedAssemblies);
            }
        }
    }
}
