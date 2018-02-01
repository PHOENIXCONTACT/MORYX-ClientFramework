using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Marvin.ClientFramework.UI;
using NUnit.Framework;

namespace Marvin.ClientFramework.Tests
{
    [TestFixture]
    public class ScreenCaptureTests
    {
        private ScreenCapture _screenCapture;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _screenCapture = new ScreenCapture();
        }

        [Test]
        public void CaptureScreenTest()
        {
            Image image = null;

            Assert.DoesNotThrow(delegate
            {
                image = _screenCapture.CaptureScreen();
            });

            Assert.NotNull(image);
        }

        [Test]
        public void CaptureScreenToFileTest()
        {
            var fileInfo = CaptureToFile(file => _screenCapture.CaptureScreenToFile(file, ImageFormat.Jpeg));

            Assert.True(fileInfo.Length > 200);
        }

        private static FileInfo CaptureToFile(Action<string> captureMethod)
        {
            var file = Path.Combine(Environment.CurrentDirectory, "Image.jpg");
            
            Assert.DoesNotThrow(() => captureMethod(file));

            var fileInfo = new FileInfo(file);
            Assert.True(fileInfo.Exists);
            Assert.True(fileInfo.Length > 200);

            return fileInfo;
        }
    }
}