// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Caliburn.Micro;

namespace Marvin.ClientFramework.Dialog
{
    /// <summary>
    /// Screen extension whith an result
    /// </summary>
    public interface IDialogScreen : IScreen
    {
        /// <summary>
        /// Result of the dialog
        /// </summary>
        bool Result { get; }
    }
}
