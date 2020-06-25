// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using Moryx.Configuration;
using Moryx.Users;

namespace Moryx.ClientFramework
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
