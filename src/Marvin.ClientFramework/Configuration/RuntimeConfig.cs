// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Runtime.Serialization;
using Marvin.Configuration;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Configuration for the runtime
    /// </summary>
    [DataContract]
    public class RuntimeConfig : ConfigBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RuntimeConfig"/> class.
        /// </summary>
        public RuntimeConfig()
        {
            Host = "localhost";
            Port = 80;
            ClientId = "WpfClient";
        }

        /// <summary>
        /// Hostname or IP-Adress of the server to connect to.
        /// </summary>
        [DataMember]
        public string Host { get; set; }

        /// <summary>
        /// The port number of the version service on the server to connect to.
        /// </summary>
        [DataMember]
        public int Port { get; set; }

        /// <summary>
        /// Unique ClientId
        /// </summary>
        [DataMember]
        public string ClientId { get; set; }
    }
}
