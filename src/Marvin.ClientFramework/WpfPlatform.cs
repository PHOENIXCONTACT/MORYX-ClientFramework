using System;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Wpf Platform description
    /// </summary>
    public class WpfPlatform : Platform
    {
        private string _productName;

        /// <summary>
        /// Set product
        /// </summary>
        public static void SetProduct(string name)
        {
            CurrentPlatform = new WpfPlatform
            {
                _productName = name
            };
        }

        /// <summary>
        /// Type of this platform characterized with enum flags
        /// </summary>
        public override PlatformType Type => PlatformType.Client;

        /// <summary>
        /// Name of this platform
        /// </summary>
        public override string PlatformName => "ClientFramework";

        /// <summary>
        /// Version of the platform
        /// </summary>
        public override Version PlatformVersion => new Version(3, 0);

        /// <summary>
        /// Name of the product this application belongs to
        /// </summary>
        public override string ProductName => _productName;

        /// <summary>
        /// Current version of this product
        /// </summary>
        public override Version ProductVersion => PlatformVersion;
    }
}