using System;
using System.Windows;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Extensions for the HeartOfLead
    /// </summary>
    public static class HeartOfLeadExtensions
    {
        /// <summary>
        /// Adds a style resource dictionary to the current application
        /// </summary>
        public static void OverrideDefaultStyle<TCommandLineArguments>(this HeartOfLead<TCommandLineArguments> heartOfLead, Uri styleUri)
            where TCommandLineArguments : DefaultCommandLineArguments
        {
            // The HeartOfLead have to be initialized before adding resources to the Application
            if (!heartOfLead.IsInitialied)
                throw new InvalidOperationException("HeartOfLead is not initialized");

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = styleUri
            });
        }
    }
}