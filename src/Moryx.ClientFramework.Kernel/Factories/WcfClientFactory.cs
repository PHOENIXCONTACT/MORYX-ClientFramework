// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Moryx.Container;
using Moryx.Modules;
using Moryx.Tools.Wcf;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Wcf client factory to crate wcf clients
    /// </summary>
    [KernelComponent(typeof(IWcfClientFactory), typeof(IInitializable))]
    public class WcfClientFactory : BaseWcfClientFactory, IPriorizedInitialize
    {
        #region Dependency Injection

        /// <summary>
        /// Gets or sets the kernel configuration manager.
        /// </summary>
        public IKernelConfigManager ConfigManager { get; set; }

        #endregion

        #region Fields and Properties

        /// <summary>
        /// Sets an runlevel for the initialization phase
        /// </summary>
        public RunLevel RunLevel => RunLevel.R0;

        #endregion

        /// 
        public void Initialize()
        {
            var runtimeConfig = ConfigManager.GetConfiguration<RuntimeConfig>();
            var proxyConfig = ConfigManager.GetConfiguration<ProxyConfig>();

            var wcfConfig = new WcfClientFactoryConfig
            {
                ClientId = runtimeConfig.ClientId,
                Host = runtimeConfig.Host,
                Port = runtimeConfig.Port
            };

            base.Initialize(wcfConfig, proxyConfig, new WpfThreadContext());
        }
    }
}
