// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Markup;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Extension to determine the visibility depends to the permission
    /// </summary>
    public class VisibilityPermissionExtension : PermissionExtension
    {
        /// <summary>
        /// Flag to inverse the visibility result
        /// </summary>
        [ConstructorArgument("Inverse")]
        public bool Inverse { get; set; }

        /// <inheritdoc />
        protected override object ProvidePermissionBasedValue(bool hasPermission)
        {
            if (Inverse)
                return hasPermission ? Visibility.Collapsed : Visibility.Visible;

            return hasPermission ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}