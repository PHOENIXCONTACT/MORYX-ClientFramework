using System.Collections.Generic;
using Marvin.Configuration;
using Marvin.Users;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Configuration base class for client modules
    /// </summary>
    public class ClientModuleConfigBase : ConfigBase, IClientModuleConfig
    {
        /// <inheritdoc />
        /// <summary>
        /// Indicates whether the client module is enabled
        /// </summary>
        public bool IsEnabled { get; set; }

        int IClientModuleConfig.SortIndex { get; set; }

        /// <summary>
        /// Access rights for this client module
        /// </summary>
        public Dictionary<string, OperationAccess> OperationAccesses { get; set; }
    }
}