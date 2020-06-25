// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows;

namespace Moryx.ClientFramework
{
    /// <inheritdoc />
    /// <summary>
    /// Operation access extension for <see cref="T:System.Windows.GridLength" />
    /// </summary>
    public class AccessGridLenghtExtension : OperationAccessExtensionBase
    {
        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CheckAccess() ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }
    }
}
