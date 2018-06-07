using System;
using System.Collections;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.Container;
using Action = System.Action;
using MessageBoxImage = Marvin.ClientFramework.Dialog.MessageBoxImage;
using MessageBoxOptions = Marvin.ClientFramework.Dialog.MessageBoxOptions;

namespace Marvin.ClientFramework.UI
{
    /// <summary>
    /// Dialog conductor to display <see cref="IScreen"/> in an overlay.
    /// The manager also allows to display message boxes
    /// </summary>
    [KernelComponent(typeof(IDialogManager))]
    public class DialogConductorViewModel : PropertyChangedBase, IDialogManager, IConductor
    {
        /// <summary>
        /// Currently active dialog
        /// </summary>
        public IScreen ActiveItem { get; private set; }
        
        /// <inheritdoc />
        public void ShowDialog<T>(T dialogModel) where T : IScreen
        {
            ShowDialog(dialogModel, null);
        }

        /// <inheritdoc />
        public void ShowDialog<T>(T dialogViewModel, Action<T> callback) where T : IScreen
        {
            if (callback != null)
                AttachCallback(dialogViewModel, () => callback(dialogViewModel));

            AttachFocusContext(dialogViewModel);
            ActivateItem(dialogViewModel);
        }

        /// <inheritdoc />
        public Task ShowDialogAsync<T>(T dialogViewModel) where T : IScreen
        {
            return ShowDialogAsync<bool, T>(dialogViewModel, tcs => tcs.SetResult(true));
        }

        /// <inheritdoc />
        public void ShowMessageBox(string message, string title, MessageBoxOptions options, MessageBoxImage image, Action<IMessageBox> callback)
        {
            var messageBox = new MessageBoxViewModel();
            messageBox.Initialize(title, message, options, image);
            ShowDialog(messageBox, callback);
        }

        /// <inheritdoc />
        public Task<MessageBoxOptions> ShowMessageBoxAsync(string message, string title, MessageBoxOptions options, MessageBoxImage image)
        {
            var messageBox = new MessageBoxViewModel();
            messageBox.Initialize(title, message, options, image);
            return ShowDialogAsync<MessageBoxOptions, MessageBoxViewModel>(messageBox, tcs => tcs.SetResult(messageBox.Result));
        }

        /// <summary>
        /// Shows an async dialog with a typed result
        /// </summary>
        private Task<TResult> ShowDialogAsync<TResult, TDialog>(TDialog dialogViewModel, Action<TaskCompletionSource<TResult>> callback)
            where TDialog : IScreen
        {
            var tcs = new TaskCompletionSource<TResult>();
            AttachCallback(dialogViewModel, () => callback(tcs));

            AttachFocusContext(dialogViewModel);
            ActivateItem(dialogViewModel);

            return tcs.Task;
        }

        /// <summary>
        /// Attaches the dialog callback to the deactivated event of the given sceen
        /// </summary>
        private static void AttachCallback<T>(T dialogViewModel, Action callback) where T : IScreen
        {
            EventHandler<DeactivationEventArgs> callbackHandler = null;
            callbackHandler = delegate
            {
                dialogViewModel.Deactivated -= callbackHandler;
                callback();
            };
            dialogViewModel.Deactivated += callbackHandler;
        }

        /// <summary>
        /// Will set the focus to the given view. 
        /// Will be attached to the <see cref="FrameworkElement"/> MoveFocus method
        /// </summary>
        private static void AttachFocusContext<T>(T dialogViewModel) where T : IScreen
        {
            var viewAware = dialogViewModel as IViewAware;
            if (viewAware == null) 
                return;

            EventHandler<ViewAttachedEventArgs> callbackHandler = null;
            callbackHandler = delegate(object sender, ViewAttachedEventArgs args)
            {
                viewAware.ViewAttached -= callbackHandler;
                var view = (FrameworkElement)args.View;

                RoutedEventHandler loadedCallback = null;
                loadedCallback = delegate
                {
                    view.Loaded -= loadedCallback;

                    // set the keyboard focus to the first focusable item of this UI
                    view.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                };

                view.Loaded += loadedCallback;
            };

            viewAware.ViewAttached += callbackHandler;
        }

        /// <inheritdoc />
        public void ActivateItem(object item) 
        {
            ActiveItem = item as IScreen;

            // ReSharper disable once SuspiciousTypeConversion.Global
            var child = ActiveItem as IChild;
            if (child != null)
            {
                child.Parent = this;
            }

            ActiveItem?.Activate();

            NotifyOfPropertyChange(() => ActiveItem);
            ActivationProcessed(this, new ActivationProcessedEventArgs
            {
                Item = ActiveItem, Success = true
            });
        }

        /// <inheritdoc />
        public void CloseItem(object item)
        {
            var guard = item as IGuardClose;
            if(guard != null) 
            {
                guard.CanClose(result => 
                {
                    if(result)
                        CloseActiveItemCore();
                });
            }
            else 
                CloseActiveItemCore();
        }

        /// <inheritdoc />
        public void CloseActiveItemCore() 
        {
            var oldItem = ActiveItem;

            ActivateItem(null);

            oldItem?.Deactivate(true);
        }

        /// <inheritdoc />
        public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed = delegate { };

        /// <inheritdoc />
        public IEnumerable GetChildren()
        {
            return null;
        }

        /// <inheritdoc />
        public void DeactivateItem(object item, bool close)
        {
            CloseItem(item);
        }

        #region Overloads

        /// <inheritdoc />
        public void ShowMessageBox(string message, string title, MessageBoxOptions options, MessageBoxImage image)
        {
            ShowMessageBox(message, title, options, image, null);
        }

        /// <inheritdoc />
        public void ShowMessageBox(string message, string title, Action<IMessageBox> callback)
        {
            ShowMessageBox(message, title, MessageBoxOptions.Ok, MessageBoxImage.None, callback);
        }

        /// <inheritdoc />
        public Task ShowMessageBoxAsync(string message, string title)
        {
            return ShowMessageBoxAsync(message, title, MessageBoxOptions.Ok, MessageBoxImage.None);
        }

        /// <inheritdoc />
        public void ShowMessageBox(string message, string title)
        {
            ShowMessageBox(message, title, MessageBoxOptions.Ok, MessageBoxImage.None, null);
        }

        #endregion
    }
}