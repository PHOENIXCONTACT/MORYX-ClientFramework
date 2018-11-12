using System;
using System.Reflection;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Wpf Platform description
    /// </summary>
    public class WpfPlatform : Platform
    {
        /// <summary>
        /// Set product
        /// </summary>
        public static void SetProduct(string name)
        {
            var startAssembly = Assembly.GetEntryAssembly();
            Current = new WpfPlatform
            {
                PlatformName = "ClientFramework",
                PlatformVersion = typeof(WpfPlatform).Assembly.GetName().Version,
                // TODO: Is this the right place to get this information
                ProductName = startAssembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "MARVIN Application",
                ProductVersion = new Version(startAssembly.GetCustomAttribute<AssemblyVersionAttribute>()?.Version ?? "1.0.0"),
                ProductDescription = startAssembly.GetCustomAttribute<AssemblyDescriptionAttribute>()?.Description ?? "No Description provided!",
            };
        }

        /// <summary>
        /// Type of this platform characterized with enum flags
        /// </summary>
        public override PlatformType Type => PlatformType.Client;

        /// <summary>
        /// Current version string of the runtime platform
        /// </summary>
        public static string ClientVersion => Current.PlatformVersion.ToString(3);
    }
}