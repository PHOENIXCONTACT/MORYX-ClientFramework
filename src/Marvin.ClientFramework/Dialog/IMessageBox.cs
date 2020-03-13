// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Caliburn.Micro;

namespace Marvin.ClientFramework.Dialog 
{
    /// <summary>
    /// Interface for MVVM MessageBoxes
    /// </summary>
    public interface IMessageBox : IScreen
    {
        /// <summary>
        /// Initializes the message box, sets the message, options and the image type
        /// </summary>
        void Initialize(string displayName, string message, MessageBoxOptions options, MessageBoxImage image);

        /// <summary>
        /// The message wich would be shown by the message box
        /// </summary>
        string Message { get; }

        /// <summary>
        /// Actions which can be performed by the message box
        /// E.g. Ok, Cancel, ...
        /// </summary>
        MessageBoxOptions Options { get; }

        /// <summary>
        /// Image of the messagebox. <see cref="MessageBoxImage"/> for all types
        /// </summary>
        MessageBoxImage Image { get; }

        /// <summary>
        /// Command for the Ok button
        /// </summary>
        void Ok();

        /// <summary>
        /// Command for the Cancel button
        /// </summary>
        void Cancel();

        /// <summary>
        /// Command for the Yes button
        /// </summary>
        void Yes();

        /// <summary>
        /// Command for the No button
        /// </summary>
        void No();

        /// <summary>
        /// Result for the message box 
        /// </summary>
        MessageBoxOptions Result { get; }
    }
}
