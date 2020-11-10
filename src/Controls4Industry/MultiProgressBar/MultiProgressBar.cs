// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// Used to visualize muliple progresses in different colors in one bar.
    /// </summary>
    [TemplatePart(Name = "PART_Track", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_Indicator", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_GlowRect", Type = typeof(FrameworkElement))]
    public class MultiProgressBar : Control
    {
        private double _value = 0.0;

        private const string TrackTemplateName = "PART_Track";
        private const string IndicatorTemplateName = "PART_Indicator";
        private const string GlowingRectTemplateName = "PART_GlowRect";

        private FrameworkElement _track;
        private FrameworkElement _indicator;
        private FrameworkElement _glow;

        #region Properties with DependencyProperties 
        /// <summary>
        /// Max-Value for the summed values of the segments
        /// </summary>
        public double Max
        {
            get { return (double)GetValue(MaxProperty); }
            set
            {
                SetValue(MaxProperty, value);
                LayoutMultiProgressBar();
            }
        }
        /// <summary>
        /// <see cref="Max"/>
        /// </summary>
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register("Max", typeof(double), typeof(MultiProgressBar), new PropertyMetadata(0.0));

        /// <summary>
        /// Collection of the progresses to be diplayed in the MultiProgressBar
        /// </summary>
        public ObservableCollection<ProgressItem> StepItems
        {
            get { return (ObservableCollection<ProgressItem>)GetValue(StepItemsProperty); }
            set { SetValue(StepItemsProperty, value); }
        }

        /// <summary>
        /// <see cref="StepItems"/>
        /// </summary>
        public static readonly DependencyProperty StepItemsProperty = DependencyProperty.Register("StepItems", typeof(ObservableCollection<ProgressItem>), typeof(MultiProgressBar), new PropertyMetadata(null));

        /// <summary>
        /// Determines if the glow-effect is visible
        /// </summary>
        public Visibility AnimationVisibility
        {
            get { return (Visibility)GetValue(AnimationVisibilityProperty); }
            set { SetValue(AnimationVisibilityProperty, value); }
        }
        /// <summary>
        /// <see cref="AnimationVisibility"/>
        /// </summary>
        public static readonly DependencyProperty AnimationVisibilityProperty = DependencyProperty.Register("AnimationVisibility", typeof(Visibility), typeof(MultiProgressBar), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Specify the CornerRadius for Borders within this Control
        /// </summary>
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        /// <summary>
        /// <see cref="CornerRadius"/>
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(double), typeof(MultiProgressBar), new PropertyMetadata(2.5));
        #endregion

        /// <summary>
        /// Static constructor
        /// </summary>
        static MultiProgressBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiProgressBar), new FrameworkPropertyMetadata(typeof(MultiProgressBar)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiProgressBar"/> class.
        /// </summary> 
        public MultiProgressBar()
        {
            StepItems = new ObservableCollection<ProgressItem>();
            StepItems.CollectionChanged += StepItemCollectionChanged;
            IsVisibleChanged += (s, e) => { UpdateAnimation(); };
        }

        private void StepItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var newItem in e.NewItems)
                    {
                        ((ProgressItem)newItem).ValueChanged += StepItemChanged;
                        LayoutMultiProgressBar();
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var oldItem in e.OldItems)
                    {
                        ((ProgressItem)oldItem).ValueChanged -= StepItemChanged;
                        LayoutMultiProgressBar();
                    }
                    break;
            }
        }

        private void StepItemChanged(object sender, EventArgs e)
        {
            LayoutMultiProgressBar();
        }

        private readonly object _layoutLock = new object();
        private void LayoutMultiProgressBar()
        {
            lock (_layoutLock)
            {
                var borderWidth = BorderThickness.Left + BorderThickness.Right;

                if (!StepItems.Any() || ActualWidth <= borderWidth)
                    return;

                double factor;
                var actOrig = ActualWidth - borderWidth;
                var widthSum = 0.0;
                var stepItemsValueSum = 0;

                ProgressItem first = null;
                ProgressItem last = null;

                foreach (var stepItem in StepItems)
                {
                    stepItemsValueSum += stepItem.Value;

                    stepItem.FirstVisible = false;
                    stepItem.LastVisible = false;

                    if (stepItem.Value > 0 && first == null)
                    {
                        first = stepItem;
                    }

                    if (stepItem.Value > 0)
                        last = stepItem;
                }

                if (first != null)
                    first.FirstVisible = true;

                if (last != null)
                    last.LastVisible = true;

                if (Max > stepItemsValueSum)
                {
                    factor = Max > 0 ? actOrig / Max : 1.0;
                }
                else
                {
                    factor = Max > 0 ? actOrig / stepItemsValueSum : 1.0;
                }
                foreach (ProgressItem stepItem in StepItems)
                {
                    var partWidth = factor * stepItem.Value;

                    if (widthSum < actOrig) // Check if there is space left in the bar
                    {
                        if (widthSum + partWidth <= actOrig) // Check if there is enough space for the next progressItem
                        {
                            stepItem.Width = Math.Max(0, partWidth);
                        }
                        else // Use the remaining space of the bar if the width of this item is too much
                        {
                            stepItem.Width = Math.Max(0, actOrig - widthSum);
                        }
                        widthSum += stepItem.Width;
                    }
                    else
                    {
                        stepItem.Width = 0;
                    }
                }
                _value = stepItemsValueSum > Max ? Max : stepItemsValueSum;
                SetProgressBarIndicatorLength();
            }
        }

        #region Original ProgressBar
        #region Properties

        /// <summary>
        ///     The DependencyProperty for the IsIndeterminate property.
        ///     Flags:          none
        ///     DefaultValue:   false
        /// </summary>
        public static readonly DependencyProperty IsIndeterminateProperty =
            DependencyProperty.Register(
                "IsIndeterminate",
                typeof(bool),
                typeof(MultiProgressBar),
                new FrameworkPropertyMetadata(
                    false,
                    OnIsIndeterminateChanged));

        /// <summary>
        ///     Determines if ProgressBar shows actual values (false)
        ///     or generic, continuous progress feedback (true).
        /// </summary>
        /// <value></value>
        public bool IsIndeterminate
        {
            get { return (bool)GetValue(IsIndeterminateProperty); }
            set { SetValue(IsIndeterminateProperty, value); }
        }

        /// <summary>
        ///     Called when IsIndeterminateProperty is changed on "d".
        /// </summary>
        private static void OnIsIndeterminateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiProgressBar progressBar = (MultiProgressBar)d;

            // Invalidate automation peer
            ProgressBarAutomationPeer peer = UIElementAutomationPeer.FromElement(progressBar) as ProgressBarAutomationPeer;
            if (peer != null)
            {
                peer.InvalidatePeer();
            }

            progressBar.SetProgressBarGlowElementBrush();

            progressBar.SetProgressBarIndicatorLength();

            progressBar.LayoutMultiProgressBar();
        }

        ///// <summary>
        ///// DependencyProperty for <see cref="Orientation" /> property.
        ///// </summary>
        //public static readonly DependencyProperty OrientationProperty =
        //        DependencyProperty.Register(
        //                "Orientation",
        //                typeof(Orientation),
        //                typeof(ProgressBar),
        //                new FrameworkPropertyMetadata(
        //                        Orientation.Horizontal,
        //                        FrameworkPropertyMetadataOptions.AffectsMeasure,
        //                        new PropertyChangedCallback(OnOrientationChanged)),
        //                new ValidateValueCallback(IsValidOrientation));

        ///// <summary>
        ///// Specifies orientation of the ProgressBar.
        ///// </summary>
        //public Orientation Orientation
        //{
        //    get { return (Orientation)GetValue(OrientationProperty); }
        //    set { SetValue(OrientationProperty, value); }
        //}

        //internal static bool IsValidOrientation(object o)
        //{
        //    Orientation value = (Orientation)o;
        //    return value == Orientation.Horizontal
        //        || value == Orientation.Vertical;
        //}

        //private static void OnOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    ProgressBar progressBar = (ProgressBar)d;
        //    progressBar.SetProgressBarIndicatorLength();
        //}

        #endregion Properties

        #region Event Handler

        // Set the width/height of the contract parts
        private void SetProgressBarIndicatorLength()
        {
            if (_indicator != null)
            {
                double min = 0;//Minimum;

                // When indeterminate or maximum == minimum, have the indicator stretch the 
                // whole length of track
                if (_value <= Max)
                {
                    var borderWidth = BorderThickness.Left + BorderThickness.Right;
                    double percent = IsIndeterminate || Max <= min ? 1.0 : (_value - min) / (Max - min);
                    _indicator.Width = Math.Max(0, percent * ActualWidth - borderWidth);
                }
                else
                {
                    _indicator.Width = 0.0;
                }
                UpdateAnimation();
            }
        }

        // This is used to set the correct brush/opacity mask on the indicator.
        private void SetProgressBarGlowElementBrush()
        {
            if (_glow == null)
                return;

            _glow.InvalidateProperty(UIElement.OpacityMaskProperty);
            _glow.InvalidateProperty(Shape.FillProperty);
            if (this.IsIndeterminate)
            {

                if (this.Foreground is SolidColorBrush)
                {

                    System.Windows.Media.Color color = ((SolidColorBrush)this.Foreground).Color;
                    //Create the gradient
                    LinearGradientBrush b = new LinearGradientBrush();

                    b.StartPoint = new System.Windows.Point(0, 0);
                    b.EndPoint = new System.Windows.Point(1, 0);

                    b.GradientStops.Add(new GradientStop(Colors.Transparent, 0.0));
                    b.GradientStops.Add(new GradientStop(color, 0.4));
                    b.GradientStops.Add(new GradientStop(color, 0.6));
                    b.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                    _glow.SetCurrentValue(Shape.FillProperty, b);
                }
                else
                {
                    // This is not a solid color brush so we will need an opacity mask.
                    LinearGradientBrush mask = new LinearGradientBrush();
                    mask.StartPoint = new System.Windows.Point(0, 0);
                    mask.EndPoint = new System.Windows.Point(1, 0);
                    mask.GradientStops.Add(new GradientStop(Colors.Transparent, 0.0));
                    mask.GradientStops.Add(new GradientStop(Colors.Black, 0.4));
                    mask.GradientStops.Add(new GradientStop(Colors.Black, 0.6));
                    mask.GradientStops.Add(new GradientStop(Colors.Transparent, 1.0));
                    _glow.SetCurrentValue(UIElement.OpacityMaskProperty, mask);
                    _glow.SetCurrentValue(Shape.FillProperty, this.Foreground);
                }
            }

        }

        //This creates the repeating animation
        private void UpdateAnimation()
        {
            if (_glow != null)
            {
                if (IsVisible
                    && (_glow.Width > 0)
                    && (_indicator.Width > 0)
                    && (_value < Max || StepItems.Any(s => s.Value > 0 && s.KeepGlowEffect && IsUserVisible(s, this))))
                {
                    //Set up the animation
                    double endPos = _indicator.Width + _glow.Width;
                    double startPos = -1 * _glow.Width;

                    TimeSpan translateTime = TimeSpan.FromSeconds(((int)(endPos - startPos) / 200.0)); // travel at 200px /second
                    TimeSpan pauseTime = TimeSpan.FromSeconds(1.0);  // pause 1 second between animations
                    TimeSpan startTime;

                    //Is the animation currenly running (with one pixel fudge factor)
                    if (_glow.Margin.Left >= startPos && _glow.Margin.Left <= endPos - 1)
                    {
                        // make it appear that the timer already started.
                        // To do this find out how many pixels the glow has moved and divide by the speed to get time.
                        startTime = TimeSpan.FromSeconds(-1 * (_glow.Margin.Left - startPos) / 200.0);
                    }
                    else
                    {

                        startTime = TimeSpan.Zero;
                    }

                    ThicknessAnimationUsingKeyFrames animation = new ThicknessAnimationUsingKeyFrames();

                    animation.BeginTime = startTime;
                    animation.Duration = new Duration(translateTime + pauseTime);
                    animation.RepeatBehavior = RepeatBehavior.Forever;

                    //Start with the glow hidden on the left.
                    animation.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(startPos, 0, 0, 0), TimeSpan.FromSeconds(0)));
                    //Move to the glow hidden on the right.
                    animation.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(endPos, 0, 0, 0), translateTime));
                    //There is a pause after the glow is off screen

                    _glow.BeginAnimation(FrameworkElement.MarginProperty, animation);
                }
                else
                {
                    _glow.BeginAnimation(FrameworkElement.MarginProperty, null);
                }
            }
        }

        private bool IsUserVisible(FrameworkElement element, FrameworkElement container)
        {
            if (!element.IsVisible)
                return false;

            Rect bounds = element.TransformToAncestor(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            Rect rect = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);
            return rect.Contains(bounds.TopLeft) || rect.Contains(bounds.BottomRight);
        }
        #endregion

        #region Method Overrides

        /// <summary>
        /// Called when the Template's tree has been generated
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_track != null)
            {
                _track.SizeChanged -= OnTrackSizeChanged;
            }

            _track = GetTemplateChild(TrackTemplateName) as FrameworkElement;
            _indicator = GetTemplateChild(IndicatorTemplateName) as FrameworkElement;
            _glow = GetTemplateChild(GlowingRectTemplateName) as FrameworkElement;

            if (_track != null)
            {
                _track.SizeChanged += OnTrackSizeChanged;
            }

            if (this.IsIndeterminate)
                SetProgressBarGlowElementBrush();
        }

        private void OnTrackSizeChanged(object sender, SizeChangedEventArgs e)
        {
            LayoutMultiProgressBar();
            //SetProgressBarIndicatorLength();
        }

        #endregion
        #endregion
    }
}
