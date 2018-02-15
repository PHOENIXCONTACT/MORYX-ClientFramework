using System.Collections.Generic;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Preset for touch view type
    /// </summary>
    [KernelComponent(typeof(IViewLocatorConfiguratorPreset))]
    public class WideScreenPreset : IViewLocatorConfiguratorPreset
    {
        /// <inheritdoc />
        public string Name => "Widescreen";

        /// <inheritdoc />
        public IEnumerable<string> ViewSuffixes => new[] { "Widescreen" };
    }
}
