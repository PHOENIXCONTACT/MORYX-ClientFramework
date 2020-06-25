using System;
using System.Reflection;
using System.Threading;

namespace Marvin.ClientFramework.Kernel.Tests
{
    internal class FileLoaderBadMock : IAssemblyFileLoader
    {
        public void Initialize(Uri uri, ProxyConfig proxyConfig)
        {

        }

        public void Dispose()
        {

        }

        public void DownloadFile()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                AssemblyDownloadFailed(this, new FailedDownload(null, null));
            });
        }

        public event EventHandler<Assembly> AssemblyDownloaded;

        private void RaiseAssemblyDownloaded(Assembly e)
        {
            var handler = AssemblyDownloaded;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<FailedDownload> AssemblyDownloadFailed;
    }
}