using System.Collections.Generic;
using System.Runtime.Serialization;
using Marvin.Configuration;

namespace Marvin.ClientFramework.Shell
{
    [DataContract]
    public class ShellRegionConfig : ConfigBase, IShellRegionConfig
    {
        [DataMember]
        public List<RegionConfig> Regions { get; set; }

        [DataMember]
        public bool Initialized { get; set; }
    }
}