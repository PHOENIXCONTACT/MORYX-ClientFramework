using System;
using System.IO;
using System.Reflection;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Component to download assemblies via HTTP
    /// </summary>
    internal class HttpAssemblyLoader : HttpFileLoader, IAssemblyFileLoader
    {
        public override void Initialize(Uri uri, ProxyConfig proxyConfig)
        {
            base.Initialize(uri, proxyConfig);

            FileDownloaded += OnFileDownloaded;
            FileDownloadFailed += OnFileDownloadFailed;
        }

        /// <summary>
        /// Called when file download failed.
        /// </summary>
        private void OnFileDownloadFailed(object sender, FailedDownload failedDownload)
        {
            AssemblyDownloadFailed?.Invoke(this, failedDownload);
        }

        /// <summary>
        /// Called when file downloaded success.
        /// </summary>
        private void OnFileDownloaded(object sender, Stream stream)
        {
            if (AssemblyDownloaded == null) 
                return;

            Assembly assembly;

            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                var mStream = memoryStream.ToArray();

                assembly = Assembly.Load(mStream);
            }

            AssemblyDownloaded(this, assembly);
        }

        public event EventHandler<Assembly> AssemblyDownloaded;

        public event EventHandler<FailedDownload> AssemblyDownloadFailed;
    }
}
