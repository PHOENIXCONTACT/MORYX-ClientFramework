// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using Moryx.ClientFramework.Tests.Mocks;
using NUnit.Framework;

namespace Moryx.ClientFramework.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void ProxyConfigTest()
        {
            var proxyConfig = new ProxyConfig();

            Assert.IsFalse(proxyConfig.EnableProxy);
            Assert.IsFalse(proxyConfig.UseDefaultWebProxy);
            Assert.AreEqual(proxyConfig.Port, 8080);
            Assert.AreEqual(proxyConfig.Address, "localhost");
        }

        [Test]
        public void RuntimeConfigTest()
        {
            var runtimeConfig = new RuntimeConfig();

            Assert.AreEqual(runtimeConfig.ClientId, "WpfClient");
            Assert.AreEqual(runtimeConfig.Port, 80);
            Assert.AreEqual(runtimeConfig.Host, "localhost");
        }

        [Test]
        public void WindowConfigTest()
        {
            WindowConfig windowConfig = null;

            Assert.DoesNotThrow(delegate
            {
                windowConfig = new WindowConfig();
            });

            Assert.AreEqual(windowConfig.State, WindowState.Normal);
            Assert.AreEqual(windowConfig.StartupLocation, WindowStartupLocation.CenterScreen);
        }

        [Test]
        public void ModulesConfigurationTest()
        {
            var modConf = new ModulesConfiguration();

            Assert.NotNull(modConf.Modules);
            Assert.NotNull(modConf.Shell);
        }

        [Test]
        public void ModuleConfigCopyToTest()
        {
            var modConf = new ModulConfig
            {
                SortIndex = 2,
                IsEnabled = true
            };

            var clientModuleConfig = new ClientModuleConfigMock();
            modConf.CopyTo(clientModuleConfig);
            
            Assert.AreEqual(modConf.SortIndex, ((IClientModuleConfig)clientModuleConfig).SortIndex);
            Assert.AreEqual(modConf.IsEnabled, clientModuleConfig.IsEnabled);
        }
    }
}
