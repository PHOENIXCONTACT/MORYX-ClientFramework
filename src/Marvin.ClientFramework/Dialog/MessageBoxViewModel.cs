// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using Caliburn.Micro;

namespace Marvin.ClientFramework.Dialog
{
    /// <summary>
    /// Base ViewModel for MessageBoxes
    /// </summary>
    public class MessageBoxViewModel : Screen, IMessageBox
    {
        MessageBoxOptions _selection;
        private MessageBoxImage _image = MessageBoxImage.None;

        /// <inheritdoc />
        public void Initialize(string displayName, string message, MessageBoxOptions options, MessageBoxImage image)
        {
            DisplayName = displayName;
            Message = message;
            Options = options;
            Image = image;
        }

        /// <inheritdoc />
        public MessageBoxOptions Result => _selection;

        /// <inheritdoc />
        public bool OkVisible => IsVisible(MessageBoxOptions.Ok);

        /// <inheritdoc />
        public bool CancelVisible => IsVisible(MessageBoxOptions.Cancel);

        /// <inheritdoc />
        public bool YesVisible => IsVisible(MessageBoxOptions.Yes);

        /// <inheritdoc />
        public bool NoVisible => IsVisible(MessageBoxOptions.No);

        /// <inheritdoc />
        public void Ok()
        {
            Select(MessageBoxOptions.Ok);
        }

        /// <inheritdoc />
        public void Cancel()
        {
            Select(MessageBoxOptions.Cancel);
        }

        /// <inheritdoc />
        public void Yes()
        {
            Select(MessageBoxOptions.Yes);
        }

        /// <inheritdoc />
        public void No()
        {
            Select(MessageBoxOptions.No);
        }

        /// <inheritdoc />
        private bool IsVisible(MessageBoxOptions option)
        {
            return (Options & option) == option;
        }

        /// <inheritdoc />
        private void Select(MessageBoxOptions option)
        {
            _selection = option;
            TryClose();
        }

        /// <inheritdoc />
        public string Message { get; private set; }

        /// <inheritdoc />
        public MessageBoxOptions Options { get; private set; }

        /// <inheritdoc />
        public MessageBoxImage Image
        {
            get => _image;
            private set
            {
                _image = value;

                HandleIconVisibility(_image);

                NotifyOfPropertyChange(() => Image);
            }
        }

        /// <summary>
        /// Handles the icon visibility and sets only the icon which should be shown to visible.
        /// </summary>
        private void HandleIconVisibility(MessageBoxImage image)
        {
            IconVisibility = Visibility.Visible;
            OkSignVisibility = Visibility.Collapsed;
            ErrorSignVisibility = Visibility.Collapsed;
            ErrorHandSignVisibility = Visibility.Collapsed;
            AttentionSignVisibility = Visibility.Collapsed;
            QuestionMarkSignVisibility = Visibility.Collapsed;
            ExclamationMarkSignVisibility = Visibility.Collapsed;

            switch (image)
            {
                case MessageBoxImage.None:
                    IconVisibility = Visibility.Collapsed;
                    break;
                case MessageBoxImage.Ok:
                    OkSignVisibility = Visibility.Visible;
                    break;
                case MessageBoxImage.Error:
                    ErrorSignVisibility = Visibility.Visible;
                    break;
                case MessageBoxImage.ErrorHand:
                    ErrorHandSignVisibility = Visibility.Visible;
                    break;
                case MessageBoxImage.Attention:
                    AttentionSignVisibility = Visibility.Visible;
                    break;
                case MessageBoxImage.Question:
                    QuestionMarkSignVisibility = Visibility.Visible;
                    break;
                case MessageBoxImage.Exclamation:
                    ExclamationMarkSignVisibility = Visibility.Visible;
                    break;
            }

            NotifyOfPropertyChange(() => IconVisibility);
            NotifyOfPropertyChange(() => OkSignVisibility);
            NotifyOfPropertyChange(() => ErrorSignVisibility);
            NotifyOfPropertyChange(() => ErrorHandSignVisibility);
            NotifyOfPropertyChange(() => AttentionSignVisibility);
            NotifyOfPropertyChange(() => QuestionMarkSignVisibility);
            NotifyOfPropertyChange(() => ExclamationMarkSignVisibility);
        }

        #region Visibility Properties

        /// <summary>
        /// Visibility Property for ImageTypes
        /// </summary>
        public Visibility IconVisibility { get; set; }

        /// <summary>
        /// Visibility Property for ImageTypes
        /// </summary>
        public Visibility OkSignVisibility { get; set; }

        /// <summary>
        /// Visibility Property for ImageTypes
        /// </summary>
        public Visibility ErrorSignVisibility { get; set; }

        /// <summary>
        /// Visibility Property for ImageTypes
        /// </summary>
        public Visibility ErrorHandSignVisibility { get; set; }

        /// <summary>
        /// Visibility Property for ImageTypes
        /// </summary>
        public Visibility AttentionSignVisibility { get; set; }

        /// <summary>
        /// Visibility Property for ImageTypes
        /// </summary>
        public Visibility QuestionMarkSignVisibility { get; set; }

        /// <summary>
        /// Visibility Property for ImageTypes
        /// </summary>
        public Visibility ExclamationMarkSignVisibility { get; set; }

        #endregion
    }
}
