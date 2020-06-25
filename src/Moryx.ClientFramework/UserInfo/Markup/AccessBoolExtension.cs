// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Operation access extension for bool
    /// </summary>
    public class AccessBoolExtension : OperationAccessExtensionBase
    {
        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CheckAccess();
        }
    }
}
