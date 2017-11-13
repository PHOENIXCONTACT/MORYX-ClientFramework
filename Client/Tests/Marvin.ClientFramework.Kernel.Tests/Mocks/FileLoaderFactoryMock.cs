namespace Marvin.ClientFramework.Kernel.Tests
{
    internal class FileLoaderFactoryMock : IAssemblyFileLoaderFactory
    {
        private readonly bool _good;

        public FileLoaderFactoryMock(bool good)
        {
            _good = good;
        }

        public IAssemblyFileLoader Create()
        {
            if (_good)
            {
                return new FileLoaderGoodMock();
            }
            return new FileLoaderBadMock();
        }
    }
}