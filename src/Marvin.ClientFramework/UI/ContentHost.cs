using System.Windows;
using System.Windows.Controls;
using Caliburn.Micro;

namespace Marvin.ClientFramework.UI
{
    /// <summary>
    /// Content host
    /// </summary>
    public class ContentHost : ContentControl
    {
        private readonly Grid _contentGrid;
        private UIElement _currentView;

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        public ContentHost()
        {
            _contentGrid = new Grid();
            Content = _contentGrid;
            Focusable = false;
        }

        /// <summary>
        /// Gets or sets the current item
        /// </summary>
        public object CurrentItem
        {
            get { return GetValue(CurrentItemProperty); }
            set { SetValue(CurrentItemProperty, value); }
        }

        /// <summary>
        /// Current item
        /// </summary>
        public static readonly DependencyProperty CurrentItemProperty =
                DependencyProperty.Register("CurrentItem", typeof(object), typeof(ContentHost),
                new PropertyMetadata(null, (s, e) => ((ContentHost)s).OnCurrentItemChanged()));

        private void OnCurrentItemChanged()
        {
            var newView = EnsureItem(CurrentItem);
            SendToBack(_currentView);
            _currentView = newView;
        }

        private UIElement EnsureItem(object source)
        {
            if (source == null)
            {
                return null;
            }

            var view = GetView(source);

            if (!_contentGrid.Children.Contains(view))
            {
                SubscribeDeactivation(source);
                _contentGrid.Children.Add(view);
            }

            BringToFront(view);
            return view;
        }

        // logic from Caliburn.Micro
        private UIElement GetView(object viewModel)
        {
            var context = View.GetContext(this);
            var view = ViewLocator.LocateForModel(viewModel, this, context);

            ViewModelBinder.Bind(viewModel, view, context);
            return view;
        }

        private void SubscribeDeactivation(object source)
        {
            var sourceScreen = source as IScreen;
            if (sourceScreen != null)
            {
                sourceScreen.Deactivated += SourceScreen_Deactivated;
            }
        }

        private void SourceScreen_Deactivated(object sender, DeactivationEventArgs e)
        {
            if (e.WasClosed)
            {
                var sourceScreen = sender as IScreen;
                if (sourceScreen != null)
                {
                    sourceScreen.Deactivated -= SourceScreen_Deactivated;
                }

                var view = GetView(sourceScreen);
                _contentGrid.Children.Remove(view);
            }
        }

        private void BringToFront(UIElement control)
        {
            control.Visibility = Visibility.Visible;
        }

        private void SendToBack(UIElement control)
        {
            if (control != null)
            {
                control.Visibility = Visibility.Collapsed;
            }
        }
    }
}
