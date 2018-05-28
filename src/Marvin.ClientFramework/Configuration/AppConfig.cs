using System;
using System.Linq;
using System.Runtime.Serialization;
using Marvin.Configuration;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Base application configuration so set the Name, Title, Application
    /// and framework properties
    /// </summary>
    [DataContract]
    public class AppConfig : ConfigBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfig"/> class.
        /// </summary>
        public AppConfig()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());

            Name = Properties.strings.AppConfig_DefaultName;
            Application = "HeartOfLead_" + result;
            ViewPreset = "Default";

            OpenConfigWithControl = true;
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        [DataMember]
        public string Application { get; set; }

        /// <summary>
        /// Gets or sets the run mode.
        /// </summary>
        [DataMember]
        public string RunMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [configuration with control].
        /// </summary>
        [DataMember]
        public bool OpenConfigWithControl { get; set; }

        /// <summary>
        /// Indicator if instances should be limited
        /// </summary>
        [DataMember]
        public bool LimitInstances { get; set; }

        /// <summary>
        /// Selected view type
        /// </summary>
        [DataMember]
        public string ViewPreset { get; set; }
    }
}