using Marvin.Container;

namespace Marvin.ClientFramework.Shell
{
    /// <summary>
    /// Manages the plugins assigned to various region
    /// </summary>
    public interface IShellRegionController
    {
        /// <summary>
        /// Initialize this component and prepare it for incoming taks. This must only involve preparation and must not start 
        /// any active functionality and/or periodic execution of logic.
        /// </summary>
        void Initialize(IModuleContainerFactory containerFactory, IModuleManager manager, IConfigProvider provider);

        /// <summary>
        /// Registers component to the shell container.
        /// </summary>
        void Register<T>(T component) where T : class;

        /// <summary>
        /// Fetch region with this name
        /// </summary>
        Region FetchRegion(string name);
    }
}