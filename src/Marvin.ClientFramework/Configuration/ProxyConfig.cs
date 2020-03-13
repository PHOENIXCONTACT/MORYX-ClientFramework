// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Runtime.Serialization;
using Marvin.Configuration;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Configuration for proxy settings
    /// Used for all services in this framework
    /// </summary>
    [DataContract]
    public class ProxyConfig : ConfigBase, IProxyConfig
    {
        /// <summary>
        /// Method called if no file was found and the config was generated
        /// </summary>
        public ProxyConfig()
        {
            EnableProxy = false;
            UseDefaultWebProxy = false;
            Address = "localhost";
            Port = 8080;
        }

        /// <summary>
        /// <c>True</c>, if the default proxy configuration of the machine shall be used.
        /// The <see cref="P:Marvin.Tools.WcfClient.IProxyConfig.Address" /> and 
        /// <see cref="P:Marvin.Tools.WcfClient.IProxyConfig.Port" /> properties are ignored then.
        /// </summary>
        [DataMember]
        public bool UseDefaultWebProxy { get; set; }

        /// <summary>
        /// The IP address or hostname of the proxy to be used.
        /// This property is ignored if <see cref="P:Marvin.Tools.WcfClient.IProxyConfig.UseDefaultWebProxy" /> is <c>true</c>.
        /// </summary>
        [DataMember]
        public string Address { get; set; }

        /// <summary>
        /// The TCP port  of the proxy to be used.
        /// This property is ignored if <see cref="P:Marvin.Tools.WcfClient.IProxyConfig.UseDefaultWebProxy" /> is <c>true</c>.
        /// </summary>
        [DataMember]
        public int Port { get; set; }

        /// <summary>
        /// <c>True</c>, if a proxy shall be used or <c>false otherwise.</c>
        /// </summary>
        [DataMember]
        public bool EnableProxy { get; set; }
    }
}
