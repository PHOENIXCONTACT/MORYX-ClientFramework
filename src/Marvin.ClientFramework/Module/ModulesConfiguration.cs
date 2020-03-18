using System.Collections.Generic;
using System.Runtime.Serialization;
using Marvin.Configuration;
using Marvin.Users;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Base configuration for the client modules
    /// </summary>
    [DataContract]
    public class ModulesConfiguration : IConfig
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModulesConfiguration"/> class.
        /// </summary>
        public ModulesConfiguration()
        {
            Application = "NoName";
            Shell = new ShellConfig();
            Modules = new List<ModulConfig>();
        }

        /// <summary>
        /// The name of the application. Shold be shared over multiple clients
        /// Can be grouped
        /// </summary>
        [DataMember]
        public string Application { get; set; }

        /// <summary>
        /// Shell configuration of the current instance
        /// </summary>
        [DataMember]
        public ShellConfig Shell { get; set; }

        /// <summary>
        /// Base configuration of the current modules
        /// </summary>
        [DataMember]
        public List<ModulConfig> Modules { get; set; }

        /// <summary>
        /// Current state of the config object. This should be decorated with the data member in order to save
        /// the valid state after finalized configuration.
        /// </summary>
        [DataMember]
        public ConfigState ConfigState { get; set; }

        /// <summary>
        /// Exception message if load failed. This must not be decorated with a data member attribute.
        /// </summary>
        public string LoadError { get; set; }
    }

    /// <summary>
    /// Configuration of the current shell
    /// </summary>
    [DataContract]
    public class ShellConfig
    {
        /// <summary>
        /// Gets or sets the name of the shell.
        /// </summary>
        [DataMember]
        public string ShellName { get; set; }
    }

    /// <summary>
    /// Config for a single module to load by a framework
    /// </summary>
    [DataContract]
    public class ModulConfig
    {
        /// <summary>
        /// Name of the module
        /// </summary>
        [DataMember]
        public string ModuleName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this module is enabled or not.
        /// </summary>
        [DataMember]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Index on which position the module will be displayed
        /// </summary>
        [DataMember]
        public int SortIndex { get; set; }

        /// <summary>
        /// Gets or sets the accesses for the current user of the module.
        /// </summary>
        [DataMember]
        public Dictionary<string, OperationAccess> Accesses { get; set; }

        /// <summary>
        /// Copies local framwork configuration to the module
        /// </summary>
        public void CopyTo(IClientModuleConfig moduleConfig)
        {
            moduleConfig.OperationAccesses = Accesses;
            moduleConfig.SortIndex = SortIndex;
            moduleConfig.IsEnabled = IsEnabled;
        }
    }
}
