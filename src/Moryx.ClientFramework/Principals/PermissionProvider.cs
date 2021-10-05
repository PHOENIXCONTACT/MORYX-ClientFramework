// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Class to provide attached dependency property for permission based authorization
    /// </summary>
    public class PermissionProvider : DependencyObject
    {
        /// <summary>
        /// Property to handle the default resource for the <see cref="PermissionExtensionBase"/>
        /// </summary>
        public static readonly DependencyProperty DefaultResourceProperty = DependencyProperty.RegisterAttached(
            "DefaultResource", typeof(string), typeof(PermissionProvider), new PropertyMetadata(default(string)));

        /// <summary>
        /// Sets the default resource
        /// </summary>
        public static void SetDefaultResource(DependencyObject element, string value)
        {
            element.SetValue(DefaultResourceProperty, value);
        }

        /// <summary>
        /// Returns the default resource
        /// </summary>
        public static string GetDefaultResource(DependencyObject element)
        {
            return (string) element.GetValue(DefaultResourceProperty);
        }
    }
}