// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Moryx.ClientFramework.Dialog
{
    /// <summary>
    /// Interface for a dialog providing component
    /// Conductor to display <see cref="IScreen"/> in an overlay.
    /// The manager also allows to display message boxes
    /// </summary>
    public interface IDialogManager
    {
        /// <summary>
        /// Will show a dialog of the type <see cref="IScreen"/>
        /// </summary>
        void ShowDialog<T>(T dialogViewModel) where T : IScreen;

        /// <summary>
        /// Will show a dialog of the type <see cref="IScreen"/>.
        /// This is an async implementation and returns if the dialog was closed
        /// </summary>
        Task ShowDialogAsync<T>(T dialogViewModel) where T : IScreen;

        /// <summary>
        /// Will show a dialog of the type <see cref="IScreen"/>
        /// It will provide a callback if the dialog was closed
        /// </summary>
        void ShowDialog<T>(T dialogViewModel, Action<T> callback) where T : IScreen;

        /// <summary>
        /// Shows a MessageBox with multiple options
        /// This is an async implementation and returns if the dialog was closed
        /// </summary>
        /// <param name="message">The message string</param>
        /// <param name="title">The title of the opened box</param>
        /// <param name="options">The options for the opened box.</param>
        /// <param name="image">The visible image of the box</param>
        Task<MessageBoxOptions> ShowMessageBoxAsync(string message, string title, MessageBoxOptions options, MessageBoxImage image);

        /// <summary>
        /// Shows a MessageBox with multiple options
        /// </summary>
        /// <param name="message">The message string</param>
        /// <param name="title">The title of the opened box</param>
        /// <param name="options">The options for the opened box.</param>
        /// <param name="image">The visible image of the box</param>
        /// <param name="callback">The callback if the MessageBox returns</param>
        void ShowMessageBox(string message, string title, MessageBoxOptions options, MessageBoxImage image, Action<IMessageBox> callback);

        /// <summary>
        /// Shows a MessageBox with multiple options
        /// </summary>
        /// <param name="message">The message string</param>
        /// <param name="title">The title of the opened box</param>
        /// <param name="options">The options for the opened box.</param>
        /// <param name="image">The visible image of the box</param>
        void ShowMessageBox(string message, string title, MessageBoxOptions options, MessageBoxImage image);

        /// <summary>
        /// Shows a MessageBox with multiple options
        /// This is an async implementation and returns if the dialog was closed
        /// </summary>
        /// <param name="message">The message string</param>
        /// <param name="title">The title of the opened box</param>
        Task ShowMessageBoxAsync(string message, string title);

        /// <summary>
        /// Shows a MessageBox with multiple options
        /// </summary>
        /// <param name="message">The message string</param>
        /// <param name="title">The title of the opened box</param>
        /// <param name="callback">The callback if the MessageBox returns</param>
        void ShowMessageBox(string message, string title, Action<IMessageBox> callback);

        /// <summary>
        /// Shows a MessageBox with multiple options
        /// </summary>
        /// <param name="message">The message string</param>
        /// <param name="title">The title of the opened box</param>
        void ShowMessageBox(string message, string title);
    }
}
