// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows.Markup;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Extension to determine the boolean result depends to the permission
    /// </summary>
    public class BooleanPermissionExtension : PermissionExtensionBase
    {
        /// <summary>
        /// Flag to inverse the boolean result
        /// </summary>
        [ConstructorArgument("Inverse")]
        public bool Inverse { get; set; }

        /// <inheritdoc />
        protected override object ProvidePermissionBasedValue(bool hasPermission)
        {
            return Inverse ? !hasPermission : hasPermission;
        }
    }
}