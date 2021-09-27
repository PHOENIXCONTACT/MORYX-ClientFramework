// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Extension to determine the length of a grid depends to the permission
    /// </summary>
    public class GridLengthPermissionExtension : PermissionExtensionBase
    {
        /// <inheritdoc />
        protected override object ProvidePermissionBasedValue(bool hasPermission)
        {
            return hasPermission ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }
    }
}