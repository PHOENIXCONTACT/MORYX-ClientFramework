using System;
using System.IO;
using System.Net;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// WebClient for files from HTTP
    /// </summary>
    internal class HttpFileLoader : IDisposable
    {
        private Uri _uri;
        private ProxyConfig _proxyConfig;

        /// <summary>
        /// Initializes the downloader with the <see cref="Uri"/> 
        /// to the file and a <see cref="ProxyConfig"/> for a proxy to use
        /// </summary>
        public virtual void Initialize(Uri uri, ProxyConfig proxyConfig)
        {
            _uri = uri;
            _proxyConfig = proxyConfig;
        }

        /// <summary>
        /// Downloads the file which is given with the <see cref="Initialize"/>.
        /// </summary>
        public void DownloadFile()
        {
            var webClient = new WebClient
            {
                Proxy = !_proxyConfig.EnableProxy ? null : new WebProxy(_proxyConfig.Address, _proxyConfig.Port)
            };

            webClient.DownloadDataCompleted += OnDownloadDataCompleted;
            webClient.DownloadProgressChanged += OnDownloadProgressChanged;

            webClient.DownloadDataAsync(_uri);
        }

        /// <summary>
        /// Called when download progress changed.
        /// </summary>
        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownloadProgressChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Called when download data completed.
        /// </summary>
        private void OnDownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                FileDownloadFailed?.Invoke(this, new FailedDownload(_uri, e.Error));
            }
            else
            {
                FileDownloaded?.Invoke(this, new MemoryStream(e.Result));
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }

        /// <summary>
        /// Occurs when the file was downloaded.
        /// </summary>
        public event EventHandler<Stream> FileDownloaded;

        /// <summary>
        /// Occurs when the file download failed
        /// </summary>
        public event EventHandler<FailedDownload> FileDownloadFailed;

        /// <summary>
        /// Occurs when download progress changed to show up a progress
        /// </summary>
        public event EventHandler<DownloadProgressChangedEventArgs> DownloadProgressChanged;
    }
}