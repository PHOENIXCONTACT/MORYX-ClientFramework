using Marvin.Configuration;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Derivative of the CachedConfigManager from platform to handle 
    /// all kernel configuration stored in the application folder
    /// </summary>
    public class KernelConfigManager : CachedConfigManager, IKernelConfigManager
    {

    }
}