using System;
using System.Windows;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Extensions for the HeartOfLead
    /// </summary>
    public static class HeartOfLeadExtensions
    {
        /// <summary>
        /// Adds a style resource dictionary to the current application
        /// </summary>
        public static void AddStyleExtension<TCommandLineArguments>(this HeartOfLead<TCommandLineArguments> heartOfLead, Uri styleUri)
            where TCommandLineArguments : DefaultCommandLineArguments
        {
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = styleUri
            });
        }
    }
}