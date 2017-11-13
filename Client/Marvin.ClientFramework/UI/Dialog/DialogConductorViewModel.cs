using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.Container;
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
        /// 
        public IScreen ActiveItem { get; private set; }

        ///
        public IMessageBoxFactory MessageBoxFactory { get; set; }

        /// 
        public void ShowDialog<T>(T dialogModel) where T : IScreen
        {
            ShowDialog(dialogModel, null);
        }

        /// 
        public void ShowDialog<T>(T dialogViewModel, Action<T> callback) where T : IScreen
        {
            if (callback != null)
                AttachCallback(dialogViewModel, callback);

            AttachFocusContext(dialogViewModel);
            ActivateItem(dialogViewModel);
        }

        /// 
        public void ShowMessageBox(string message, string title, MessageBoxOptions options, MessageBoxImage image, Action<IMessageBox> callback)
        {
            var messageBox = MessageBoxFactory.Create();
            messageBox.Initialize(title, message, options, image);

            ShowDialog(messageBox, callback);
        }

        /// <summary>
        /// Attaches the dialog callback to the deactivated event of the given sceen
        /// </summary>
        private static void AttachCallback<T>(T dialogViewModel, Action<T> callback) where T : IScreen
        {
            EventHandler<DeactivationEventArgs> callbackHandler = null;
            callbackHandler = delegate
            {
                dialogViewModel.Deactivated -= callbackHandler;
                callback(dialogViewModel);
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

        /// 
        public IEnumerable GetConductedItems()
        {
            return ActiveItem != null ? new[] {ActiveItem} : new object[0];
        }

        /// 
        public void ActivateItem(object item) 
        {
            var current = ActiveItem as IMessageBox;
            if (current != null)
            {
                MessageBoxFactory.Destroy(current);
            }

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

        /// 
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

        /// 
        public void CloseActiveItemCore() 
        {
            var oldItem = ActiveItem;

            ActivateItem(null);

            oldItem?.Deactivate(true);
        }

        /// 
        public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed = delegate { };

        /// 
        public IEnumerable GetChildren()
        {
            return null;
        }

        /// 
        public void DeactivateItem(object item, bool close)
        {
            CloseItem(item);
        }

        #region Overloads

        /// 
        public void ShowMessageBox(string message, string title, MessageBoxOptions options, MessageBoxImage image)
        {
            ShowMessageBox(message, title, options, image, null);
        }

        /// 
        public void ShowMessageBox(string message, string title, Action<IMessageBox> callback)
        {
            ShowMessageBox(message, title, MessageBoxOptions.Ok, MessageBoxImage.None, callback);
        }

        /// 
        public void ShowMessageBox(string message, string title)
        {
            ShowMessageBox(message, title, MessageBoxOptions.Ok, MessageBoxImage.None, null);
        }

        #endregion
    }
}