// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Threading;
using Moq;
using NUnit.Framework;

namespace Moryx.ClientFramework.Kernel.Tests
{
    [TestFixture]
    public class AssemblyLoaderTests
    {
        private AssemblyMultiLoader _assemblyMultiLoader;
        private IKernelConfigManager _configManager;

        private bool _assembliesDownloadFailedRaised;
        private bool _assembliesDownloadedRaised;
        private bool _assemblyDownloadedRaised;
        private bool _assemblyDownloadStartedRaised;
        private int _downloadEventCount;
        private List<Uri> _uris;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var configManagerMock = new Mock<IKernelConfigManager>();
            configManagerMock.Setup(c => c.GetConfiguration<ProxyConfig>()).Returns(() => new ProxyConfig());

            _configManager = configManagerMock.Object;
        }

        [SetUp]
        public void SetUp()
        {
            _assemblyMultiLoader = new AssemblyMultiLoader
            {
                KernelConfigManager = _configManager,
            };

            _assemblyMultiLoader.AssembliesDownloadFailed += (sender, downloads) => _assembliesDownloadFailedRaised = true;
            _assemblyMultiLoader.AssembliesDownloaded += (sender, assemblies) => _assembliesDownloadedRaised = true;
            _assemblyMultiLoader.AssemblyDownloaded += delegate
            {
                _assemblyDownloadedRaised = true;
                _downloadEventCount++;
            };
            _assemblyMultiLoader.AssemblyDownloadStarted += (sender, uris) => _assemblyDownloadStartedRaised = true;

            _uris = new List<Uri>
            {
                new Uri("http://localhost/TestAssembly1.dll"),
                new Uri("http://localhost/TestAssembly2.dll"),
                new Uri("http://localhost/TestAssembly3.dll"),
                new Uri("http://localhost/TestAssembly4.dll")
            };
        }

        [TearDown]
        public void TearDown()
        {
            _assemblyMultiLoader.Dispose();
            _assemblyMultiLoader = null;

            _assembliesDownloadFailedRaised = false;
            _assembliesDownloadedRaised = false;
            _assemblyDownloadedRaised = false;
            _assemblyDownloadStartedRaised = false;
            _downloadEventCount = 0;
            _uris = null;
        }

        [Test]
        public void DownloadGoodAssembly()
        {
            _assemblyMultiLoader.LoaderFactory = new FileLoaderFactoryMock(true);
            _assemblyMultiLoader.Load(_uris);

            //This test is going to be legen... wait for it ...dary
            Thread.Sleep(150);

            Assert.AreEqual(_uris.Count, _downloadEventCount);

            Assert.IsTrue(_assemblyDownloadedRaised, "The event AssemblyDownloaded was not raised!");
            Assert.IsTrue(_assemblyDownloadStartedRaised, "The event AssemblyDownloadStarted was not raised!");
            Assert.IsTrue(_assembliesDownloadedRaised, "The event AssembliesDownloaded was not raised!");
            Assert.IsFalse(_assembliesDownloadFailedRaised, "The event AssembliesDownloadFailed was raised!");
        }

        [Test]
        public void DownloadBadAssembly()
        {
            _assemblyMultiLoader.LoaderFactory = new FileLoaderFactoryMock(false);
            _assemblyMultiLoader.Load(_uris);

            //This test is going to be legen... wait for it ...dary
            Thread.Sleep(150);

            Assert.AreEqual(0, _downloadEventCount);

            Assert.IsFalse(_assemblyDownloadedRaised, "The event AssemblyDownloaded was raised!");
            Assert.IsTrue(_assemblyDownloadStartedRaised, "The event AssemblyDownloadStarted was not raised!");
            Assert.IsFalse(_assembliesDownloadedRaised, "The event AssembliesDownloaded was raised!");
            Assert.IsTrue(_assembliesDownloadFailedRaised, "The event AssembliesDownloadFailed was not raised!");
        }
    }
}
