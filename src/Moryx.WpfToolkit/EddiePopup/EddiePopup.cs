// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// EddiePopup
    /// </summary>
    public class EddiePopup : FrameworkElement
    {
        private Popup _popup;

        static EddiePopup()
        {
            EventManager.RegisterClassHandler(typeof(Window), PreviewMouseDownEvent, new MouseButtonEventHandler(OnPreviewMouseDown), true);
        }

        /// <inheritdoc />
        public EddiePopup()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        /// <inheritdoc />
        protected override int VisualChildrenCount => _popup != null ? 1 : 0;

        /// <summary>
        /// Placement target for this popup
        /// </summary>
        public static readonly DependencyProperty PlacementTargetProperty = DependencyProperty.Register(
            "PlacementTarget", typeof(FrameworkElement), typeof(EddiePopup), new PropertyMetadata(default(FrameworkElement), OnPlacementTargetChangedChangedCallback));

        private static void OnPlacementTargetChangedChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var eddiePopup = d as EddiePopup;
            if (eddiePopup != null)
            {
                eddiePopup.PlacementTarget.PreviewMouseLeftButtonUp += eddiePopup.PlacementTargetOnPreviewMouseLeftButtonDown;
            }
        }

        /// <summary>
        /// Sets or gets the PlacementTarget
        /// </summary>
        public FrameworkElement PlacementTarget
        {
            get { return (FrameworkElement)GetValue(PlacementTargetProperty); }
            set { SetValue(PlacementTargetProperty, value); }
        }

        /// <summary>
        /// Template for popup border
        /// </summary>
        public static readonly DependencyProperty PopupBorderTemplateProperty = DependencyProperty.Register(
            "PopupBorderTemplate", typeof(DataTemplate), typeof(EddiePopup), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Sets or gets the template for the popup border
        /// </summary>
        public DataTemplate PopupBorderTemplate
        {
            get { return (DataTemplate)GetValue(PopupBorderTemplateProperty); }
            set { SetValue(PopupBorderTemplateProperty, value); }
        }

        /// <summary>
        /// Popup template for this popup. This template is shown within the popup border.
        /// </summary>
        public static readonly DependencyProperty PopupTemplateProperty = DependencyProperty.Register(
            "PopupTemplate", typeof(DataTemplate), typeof(EddiePopup), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Sets or gets the popup template
        /// </summary>
        public DataTemplate PopupTemplate
        {
            get { return (DataTemplate)GetValue(PopupTemplateProperty); }
            set { SetValue(PopupTemplateProperty, value); }
        }

        /// <summary>
        /// DataContext for the popup content
        /// </summary>
        public static readonly DependencyProperty PopupDataContextProperty = DependencyProperty.Register(
            "PopupDataContext", typeof(object), typeof(EddiePopup), new PropertyMetadata(default(object)));

        /// <summary>
        /// Gets or sets the DataContext for the popup content
        /// </summary>
        public object PopupDataContext
        {
            get { return GetValue(PopupDataContextProperty); }
            set { SetValue(PopupDataContextProperty, value); }
        }

        /// <summary>
        /// HorizontalOffset of the popup
        /// </summary>
        public static readonly DependencyProperty HorizontalPopupOffsetProperty = DependencyProperty.Register(
            "HorizontalPopupOffset", typeof(float), typeof(EddiePopup), new PropertyMetadata(0.0f, OnHorizontalOffsetChangedCallback));

        private static void OnHorizontalOffsetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EddiePopup)?.RecalculatePopupOffsets();
        }

        /// <summary>
        /// Gets or sets the HorizontalOffset of the popup
        /// </summary>
        public float HorizontalPopupOffset
        {
            get { return (float)GetValue(HorizontalPopupOffsetProperty); }
            set { SetValue(HorizontalPopupOffsetProperty, value); }
        }

        /// <summary>
        /// VerticalOffset of the popup
        /// </summary>
        public static readonly DependencyProperty VerticalPopupOffsetProperty = DependencyProperty.Register(
            "VerticalPopupOffset", typeof(float), typeof(EddiePopup), new PropertyMetadata(0.0f, OnVerticalOffsetChangedCallback));

        private static void OnVerticalOffsetChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as EddiePopup)?.RecalculatePopupOffsets();
        }

        /// <summary>
        /// Gets or sets the VerticalOffset of the popup
        /// </summary>
        public float VerticalPopupOffset
        {
            get { return (float)GetValue(VerticalPopupOffsetProperty); }
            set { SetValue(VerticalPopupOffsetProperty, value); }
        }

        /// <inheritdoc />
        protected override Visual GetVisualChild(int index)
        {
            return _popup;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CreatePopup();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            DestroyPopup();
        }

        private void CreatePopup()
        {
            var popupPresenter = new ContentPresenter
            {
                Name = "PART_PopupPresenter",
                Content = PopupDataContext ?? DataContext,
                ContentTemplate = PopupBorderTemplate,
                Focusable = true
            };

            _popup = new Popup
            {
                AllowsTransparency = true,
                PlacementTarget = PlacementTarget,
                Placement = PlacementMode.Absolute,
                IsOpen = false,
                Focusable = false,
                StaysOpen = true,
                Child = popupPresenter
            };

            if (PlacementTarget != null)
            {
                PlacementTarget.PreviewMouseLeftButtonUp += PlacementTargetOnPreviewMouseLeftButtonDown;
            }

            popupPresenter.Loaded += PopupPresenterOnLoaded;
            popupPresenter.SizeChanged += PopupPresenterOnSizeChanged;
            popupPresenter.IsKeyboardFocusWithinChanged += PopupPresenterOnIsKeyboardFocusWithinChanged;

            Application.Current.MainWindow.SizeChanged += MainWindowOnSizeChanged;
        }

        private void DestroyPopup()
        {
            if (PlacementTarget != null)
            {
                PlacementTarget.PreviewMouseLeftButtonDown -= PlacementTargetOnPreviewMouseLeftButtonDown;
            }

            var popupPresenter = _popup.Child as ContentPresenter;
            if (popupPresenter != null)
            {
                popupPresenter.Loaded -= PopupPresenterOnLoaded;
                popupPresenter.SizeChanged -= PopupPresenterOnSizeChanged;
                popupPresenter.IsKeyboardFocusWithinChanged -= PopupPresenterOnIsKeyboardFocusWithinChanged;
            }

            Application.Current.MainWindow.SizeChanged -= MainWindowOnSizeChanged;

            _popup = null;
        }

        private void MainWindowOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Close();
        }

        private void PopupPresenterOnLoaded(object sender, RoutedEventArgs e)
        {
            var targetPresenter = _popup.Child.Descendants<ContentPresenter>().FirstOrDefault();
            if (targetPresenter == null)
                return;

            var borderPresenter = _popup.Child as ContentPresenter;
            targetPresenter.Content = borderPresenter?.Content ?? DataContext;
            targetPresenter.ContentTemplate = PopupTemplate;
        }

        private void PopupPresenterOnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            RecalculatePopupOffsets();
        }

        private void PopupPresenterOnIsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (!(bool)e.NewValue)
            {
                Close();
            }
        }

        private void PlacementTargetOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Open();
        }

        private static void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var eddiePopups = Application.Current.MainWindow.Descendants<EddiePopup>();
            foreach (var eddiePopup in eddiePopups)
            {
                if (eddiePopup.IsLoaded)
                {
                    eddiePopup.Close();
                }
            }
        }

        private void RecalculatePopupOffsets()
        {
            var placementTarget = _popup?.PlacementTarget as FrameworkElement;
            var popupChild = _popup?.Child as FrameworkElement;

            if (placementTarget == null || popupChild == null)
                return;

            var absoulutePosition = placementTarget.PointToScreen(new Point(0, 0));

            _popup.HorizontalOffset = absoulutePosition.X + placementTarget.ActualWidth / 2.0 - HorizontalPopupOffset;
            _popup.VerticalOffset = absoulutePosition.Y - popupChild.ActualHeight - VerticalPopupOffset;
        }

        private void Open()
        {
            _popup.IsOpen = true;
            _popup.Child.Focus();
        }

        private void Close()
        {
            _popup.IsOpen = false;
        }
    }
}
