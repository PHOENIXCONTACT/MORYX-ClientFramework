// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using Marvin.Configuration;
using Marvin.Users;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Interface mandatory for all configs of client modules
    /// </summary>
    public interface IClientModuleConfig : IConfig
    {
        /// <summary>
        /// Flag if the module is currently enabled or shall be hidden
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Sort index. This will determine the order of the buttons
        /// </summary>
        int SortIndex { get; set; }

        /// <summary>
        /// UserManagement operations that are locally configured
        /// </summary>
        Dictionary<string, OperationAccess> OperationAccesses { get; set; } 
    }
}
