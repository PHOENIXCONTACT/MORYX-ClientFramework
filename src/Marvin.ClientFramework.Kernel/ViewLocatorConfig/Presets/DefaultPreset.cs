using System.Collections.Generic;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Preset for default view type
    /// </summary>
    [KernelComponent(typeof(IViewLocatorConfiguratorPreset))]
    public class DefaultPreset : IViewLocatorConfiguratorPreset
    {
        /// <inheritdoc />
        public string Name => "Default";

        /// <inheritdoc />
        public IEnumerable<string> ViewSuffixes => new string[] { };
    }
}
