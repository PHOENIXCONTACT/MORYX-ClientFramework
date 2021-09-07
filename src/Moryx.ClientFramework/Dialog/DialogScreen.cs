// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading.Tasks;
using Caliburn.Micro;

namespace Moryx.ClientFramework.Dialog
{
    /// <summary>
    /// Dialog screen implementation which provides the try close parameter in the result property
    /// </summary>
    public class DialogScreen : Screen, IDialogScreen
    {
        /// <inheritdoc />
        // ReSharper disable once OptionalParameterHierarchyMismatch
        public override Task TryCloseAsync(bool? dialogResult = null)
        {
            Result = dialogResult.HasValue && dialogResult.Value;

            return base.TryCloseAsync(dialogResult);
        }

        /// <inheritdoc />
        /// <summary>
        /// Returns the result of the TryClose
        /// </summary>
        public bool Result { get; private set; }
    }
}
