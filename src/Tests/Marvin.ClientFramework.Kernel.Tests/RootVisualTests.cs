// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading;
using System.Windows;
using NUnit.Framework;

namespace Marvin.ClientFramework.Kernel.Tests
{
    [TestFixture]
    public class RootVisualTests
    {
        private WindowConfig _windowConfig;

        [SetUp]
        public void SetUp()
        {
            _windowConfig = new WindowConfig
            {
                Height = 600,
                Width = 800,
                Left = 0,
                Top = 0,
                StartupLocation = WindowStartupLocation.CenterScreen,
                State = WindowState.Maximized
            };
        }

        [Test, Apartment(ApartmentState.STA)]
        public void ConstructorTest()
        {
            var rootVisual = new RootVisual(_windowConfig);

            Assert.AreEqual(_windowConfig.Top, rootVisual.Top);
            Assert.AreEqual(_windowConfig.Left, rootVisual.Left);
            Assert.AreEqual(_windowConfig.Width, rootVisual.Width);
            Assert.AreEqual(_windowConfig.Height, rootVisual.Height);
            Assert.AreEqual(_windowConfig.State, rootVisual.WindowState);
        }

        [Test, Apartment(ApartmentState.STA)]
        public void CloseRewriteTest()
        {
            var rootVisual = new RootVisual(_windowConfig)
            {
                Height = 200,
                Width = 500,
                Top = 1,
                Left = 1,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                WindowState = WindowState.Normal
            };

            rootVisual.Close();

            Assert.AreEqual(rootVisual.Top, _windowConfig.Top);
            Assert.AreEqual(rootVisual.Left, _windowConfig.Left);
            Assert.AreEqual(rootVisual.Width, _windowConfig.Width);
            Assert.AreEqual(rootVisual.Height, _windowConfig.Height);
            Assert.AreEqual(rootVisual.WindowState, _windowConfig.State);
        }
    }
}
