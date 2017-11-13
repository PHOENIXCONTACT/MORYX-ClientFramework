using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Marvin.ClientFramework.Tests
{
    [TestFixture]
    public class PlatformTests
    {
        [Test]
        public void WpfPlatformTest()
        {
            const string platformName = "Wpf";
            WpfPlatform.SetProduct(platformName);
            Assert.AreEqual(platformName, WpfPlatform.Current.ProductName);
        }
    }
}