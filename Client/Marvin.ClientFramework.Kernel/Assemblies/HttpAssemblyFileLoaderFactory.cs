namespace Marvin.ClientFramework.Kernel
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