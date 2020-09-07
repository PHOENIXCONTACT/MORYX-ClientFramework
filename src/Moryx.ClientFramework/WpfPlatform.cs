// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Reflection;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Wpf Platform description
    /// </summary>
    public class WpfPlatform : Platform
    {
        /// <summary>
        /// Set product
        /// </summary>
        public static void SetProduct()
        {
            var startAssembly = Assembly.GetEntryAssembly();
            Current = new WpfPlatform
            {
                PlatformName = "ClientFramework",
                PlatformVersion = typeof(WpfPlatform).Assembly.GetName().Version,
                ProductName = startAssembly.GetCustomAttribute<AssemblyProductAttribute>()?.Product ?? "MORYX Application",
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
