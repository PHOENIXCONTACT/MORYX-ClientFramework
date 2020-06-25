using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Marvin.ClientFramework.Tests.Mocks;
using Marvin.Users;
using NUnit.Framework;

namespace Marvin.ClientFramework.Tests
{
    [TestFixture]
    public class ConfigurationTests
    {
        [Test]
        public void AppConfigTest()
        {
            var appConfig = new AppConfig();

            Assert.NotNull(appConfig.Name);
            Assert.NotNull(appConfig.Application);
            Assert.IsTrue(appConfig.OpenConfigWithControl, "Default parameter is not true");
        }

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
                DisplayName = "DisplayName",
                Accesses = new Dictionary<string, OperationAccess>
                {
                    {"Test", OperationAccess.LimitedRead}
                },
                SortIndex = 2,
                IsEnabled = true
            };

            var clientModuleConfig = new ClientModuleConfigMock();
            modConf.CopyTo(clientModuleConfig);

            Assert.AreEqual(modConf.DisplayName, clientModuleConfig.DisplayName);
            Assert.AreEqual(modConf.Accesses, clientModuleConfig.OperationAccesses);
            Assert.AreEqual(modConf.Accesses.Count, clientModuleConfig.OperationAccesses.Count);

            Assert.AreEqual(modConf.SortIndex, ((IClientModuleConfig)clientModuleConfig).SortIndex);
            Assert.AreEqual(modConf.IsEnabled, clientModuleConfig.IsEnabled);
        }
    }
}