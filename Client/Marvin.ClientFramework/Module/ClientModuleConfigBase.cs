using System.Collections.Generic;
using Marvin.Configuration;
using Marvin.Users;

namespace Marvin.ClientFramework
{
    public class ClientModuleConfigBase : ConfigBase, IClientModuleConfig
    {
        public string DisplayName
        {
            get; set;
        }

        public bool IsEnabled
        {
            get;set;
        }

        int IClientModuleConfig.SortIndex
        {
            get; set; 
        }

        public Dictionary<string, OperationAccess> OperationAccesses
        {
            get; set;
        }
    }
}