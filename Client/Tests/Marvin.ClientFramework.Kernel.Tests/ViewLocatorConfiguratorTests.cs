using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Marvin.ClientFramework.Kernel.Tests
{
    [TestFixture]
    public class ViewLocatorConfiguratorTests
    {
        private ViewLocatorConfigurator _viewLocatorConfigurator;

        [OneTimeSetUp]
        public void Setup()
        {
            _viewLocatorConfigurator = new ViewLocatorConfigurator
            {
                Presets = new List<IViewLocatorConfiguratorPreset>
                {
                    new DefaultPreset(),
                    new TouchPreset(),
                    new WideScreenPreset()
                }
            };
        }

        [Test]
        public void ActivateSetOfAKnownPresetLeadsToSuccess()
        {
            // Arrange
            // Act
            _viewLocatorConfigurator.ActivateSet("Default");

            // Assert
            Assert.AreEqual(_viewLocatorConfigurator.ActivatedSet, "Default");
        }

        [Test]
        public void ActivateSetOfAnUnknownPresetLeadsToAnException()
        {
            // Arrange
            // Act
            // Assert
            Assert.Throws<NotSupportedException>(() => _viewLocatorConfigurator.ActivateSet("NotDefinedPreset"));
        }
    }
}
