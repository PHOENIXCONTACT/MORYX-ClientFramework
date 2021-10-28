// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.Configuration;

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
    }
}
