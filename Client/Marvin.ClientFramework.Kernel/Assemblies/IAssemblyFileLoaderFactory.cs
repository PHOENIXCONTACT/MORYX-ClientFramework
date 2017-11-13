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