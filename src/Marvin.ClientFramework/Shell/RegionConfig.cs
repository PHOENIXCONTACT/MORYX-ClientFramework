using System.Runtime.Serialization;
using System.Windows;

namespace Marvin.ClientFramework.Shell
{
    /// <summary>
    /// Region config handled in the <see cref="IShellRegionConfig"/>
    /// Will configure a single region
    /// </summary>
    [DataContract]
    public class RegionConfig
    {
        /// <summary>
        /// Gets or sets the name of the region.
        /// </summary>
        [DataMember]
        public string RegionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the plugin.
        /// </summary>
        [DataMember]
        public string PluginName { get; set; }

        /// <summary>
        /// Gets or sets the visibility of the region.
        /// </summary>
        [DataMember]
        public Visibility Visibility { get; set; }
    }
}