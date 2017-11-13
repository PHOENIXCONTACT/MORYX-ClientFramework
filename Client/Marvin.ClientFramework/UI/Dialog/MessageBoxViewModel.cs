using System.Windows;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.Container;
using MessageBoxImage = Marvin.ClientFramework.Dialog.MessageBoxImage;
using MessageBoxOptions = Marvin.ClientFramework.Dialog.MessageBoxOptions;

namespace Marvin.ClientFramework.UI 
{
    /// <summary>
    /// Base ViewModel for MessageBoxes
    /// </summary>
    [GlobalComponent(LifeCycle.Transient, typeof(IMessageBox))]
    public class MessageBoxViewModel : Screen, IMessageBox
    {
        MessageBoxOptions _selection;
        private MessageBoxImage _image = MessageBoxImage.None;

        ///
        public void Initialize(string displayName, string message, MessageBoxOptions options, MessageBoxImage image)
        {
            DisplayName = displayName;
            Message = message;
            Options = options;
            Image = image;
        }

        ///
        public MessageBoxOptions Result => _selection;

        ///
        public bool OkVisible => IsVisible(MessageBoxOptions.Ok);

        ///
        public bool CancelVisible => IsVisible(MessageBoxOptions.Cancel);

        ///
        public bool YesVisible => IsVisible(MessageBoxOptions.Yes);

        ///
        public bool NoVisible => IsVisible(MessageBoxOptions.No);

        ///
        public void Ok()
        {
            Select(MessageBoxOptions.Ok);
        }

        ///
        public void Cancel()
        {
            Select(MessageBoxOptions.Cancel);
        }

        ///
        public void Yes() 
        {
            Select(MessageBoxOptions.Yes);
        }

        ///
        public void No() 
        {
            Select(MessageBoxOptions.No);
        }

        ///
        private bool IsVisible(MessageBoxOptions option) 
        {
            return (Options & option) == option;
        }

        ///
        private void Select(MessageBoxOptions option)
        {
            _selection = option;
            TryClose();
        }

        ///
        public string Message { get; private set; }

        ///
        public MessageBoxOptions Options { get; private set; }

        ///
        public MessageBoxImage Image
        {
            get { return _image; }
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