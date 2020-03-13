// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <summary> 
    /// EddieRangeSlider control lets the user select a range of values by moving a minimum and a maximum slider. 
    /// EddieRangeSlider is used to enable to user to gradually modify the values (range selection).
    /// </summary>
    [TemplatePart(Name = PartLowerSlider, Type = typeof(Slider))]
    [TemplatePart(Name = PartUpperSlider, Type = typeof(Slider))]
    public class EddieRangeSlider : Control
    {
        internal const string PartLowerSlider = "PART_LowerSlider";
        internal const string PartUpperSlider = "PART_UpperSlider";

        private Slider _lowerSlider;
        private Slider _upperSlider;

        /// <summary>
        /// Initializes the <see cref="EddieRangeSlider"/> class.
        /// </summary>
        static EddieRangeSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieRangeSlider), new FrameworkPropertyMetadata(typeof(EddieRangeSlider)));
        }

        #region Dependency Properties

        /// <summary>
        /// The DependencyProperty for the <see cref="Minimum"/> property. 
        /// Default Value: 0
        /// </summary>
        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum", typeof (int), typeof (EddieRangeSlider), new PropertyMetadata(0));

        /// <summary>
        /// The Minimum property
        /// </summary>
        public int Minimum
        {
            get { return (int) GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        /// <summary>
        /// The DependencyProperty for the <see cref="Maximum"/> property. 
        /// Default Value: 100
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof (int), typeof (EddieRangeSlider), new PropertyMetadata(100));

        /// <summary>
        /// The Maximum property
        /// </summary>
        public int Maximum
        {
            get { return (int) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// The DependencyProperty for the <see cref="UpperValue"/> property. 
        /// Default Value: 40
        /// </summary>
        public static readonly DependencyProperty UpperValueProperty = DependencyProperty.Register(
            "UpperValue", typeof (int), typeof (EddieRangeSlider), new FrameworkPropertyMetadata(
                40,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnUpperValueChanged,
                UpperValueConstrainToRange));

        /// <summary>
        /// The upper value
        /// </summary>
        public int UpperValue
        {
            get { return (int) GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        /// <summary>
        /// Called when the upper value dependency property changed
        /// </summary>
        private static void OnUpperValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (EddieRangeSlider)d;
            ctrl.OnUpperValueChanged((int)e.OldValue, (int)e.NewValue);
        }

        /// <summary>
        /// Called when the upper value changed.
        /// </summary>
        protected virtual void OnUpperValueChanged(int oldValue, int newValue)
        {
            RaiseValueChangedEvent(UpperValueChangedEvent, oldValue, newValue);
        }

        /// <summary>
        /// Returns the upper value after applying the contraint
        /// </summary>
        private static object UpperValueConstrainToRange(DependencyObject d, object value)
        {
            var ctrl = (EddieRangeSlider) d;
            var upperValue = (int)value;
            var max = ctrl.Maximum;

            return upperValue > max ? max : value;
        }

        /// <summary>
        /// The DependencyProperty for the <see cref="LowerValue"/> property. 
        /// Default Value: 10
        /// </summary>
        public static readonly DependencyProperty LowerValueProperty = DependencyProperty.Register(
            "LowerValue", typeof (int), typeof (EddieRangeSlider), new FrameworkPropertyMetadata(
                10,
                FrameworkPropertyMetadataOptions.BindsTwoWayByDefault | FrameworkPropertyMetadataOptions.Journal,
                OnLowerValueChanged,
                LowerValueConstrainToRange));

        /// <summary>
        /// LowerValue property
        /// </summary>
        public int LowerValue
        {
            get { return (int)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        /// <summary>
        /// Called when the lower value dependency property changed
        /// </summary>
        private static void OnLowerValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = (EddieRangeSlider)d;
            ctrl.OnLowerValueChanged((int)e.OldValue, (int)e.NewValue);
        }

        /// <summary>
        /// Called when the lower value changed.
        /// </summary>
        protected virtual void OnLowerValueChanged(int oldValue, int newValue)
        {
            RaiseValueChangedEvent(LowerValueChangedEvent, oldValue, newValue);
        }

        /// <summary>
        /// Returns the lower value after applying the contraint
        /// </summary>
        private static object LowerValueConstrainToRange(DependencyObject d, object value)
        {
            var ctrl = (EddieRangeSlider) d;
            var lowerValue = (int) value;
            double min = ctrl.Minimum;

            return lowerValue < min ? min : value;
        }

        /// <summary>
        /// The DependencyProperty for the <see cref="ShowValues"/> property. 
        /// Default Value: true
        /// </summary>
        public static readonly DependencyProperty ShowValuesProperty = DependencyProperty.Register(
            "ShowValues", typeof (bool), typeof (EddieRangeSlider), new PropertyMetadata(true));

        /// <summary>
        /// Show Values
        /// </summary>
        public bool ShowValues
        {
            get { return (bool) GetValue(ShowValuesProperty); }
            set { SetValue(ShowValuesProperty, value); }
        }

        #endregion

        #region Routed Events

        /// <summary> 
        /// Event correspond to lower value changed event 
        /// </summary>
        public static readonly RoutedEvent LowerValueChangedEvent = EventManager.RegisterRoutedEvent(
            "LowerValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>),
            typeof (EddieRangeSlider));

        /// <summary>
        /// Add / Remove LowerValueChangedEvent handler
        /// </summary> 
        public event RoutedPropertyChangedEventHandler<int> LowerValueChanged
        {
            add { AddHandler(LowerValueChangedEvent, value); }
            remove { RemoveHandler(LowerValueChangedEvent, value); }
        }

        /// <summary> 
        /// Event correspond to upper value changed event 
        /// </summary>
        public static readonly RoutedEvent UpperValueChangedEvent = EventManager.RegisterRoutedEvent(
            "UpperValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<int>),
            typeof (EddieRangeSlider));

        /// <summary>
        /// Add / Remove UpperValueChangedEvent handler
        /// </summary> 
        public event RoutedPropertyChangedEventHandler<int> UpperValueChanged
        {
            add { AddHandler(UpperValueChangedEvent, value); }
            remove { RemoveHandler(UpperValueChangedEvent, value); }
        }

        /// <summary>
        /// Raises the given value changed event with the given values
        /// </summary>
        private void RaiseValueChangedEvent(RoutedEvent theEvent, int oldValue, int newValue)
        {
            var args = new RoutedPropertyChangedEventArgs<int>(oldValue, newValue)
            {
                RoutedEvent = theEvent
            };
            RaiseEvent(args);
        }

        #endregion

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            //Load lower slider from template
            _lowerSlider = GetTemplateChild(PartLowerSlider) as Slider;
            if (_lowerSlider == null)
                throw new ArgumentException($"{PartLowerSlider} slider was not found in template");
            _lowerSlider.ValueChanged += OnLowerValueValueChanged;

            //Load upper slider from template
            _upperSlider = GetTemplateChild(PartUpperSlider) as Slider;
            if (_upperSlider == null)
                throw new ArgumentException($"{PartUpperSlider} slider was not found in template");
            _upperSlider.ValueChanged += OnUpperValueValueChanged;
        }

        private void OnUpperValueValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _lowerSlider.Value = Math.Min(_upperSlider.Value, _lowerSlider.Value);
        }

        private void OnLowerValueValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _upperSlider.Value = Math.Max(_upperSlider.Value, _lowerSlider.Value);
        }
    }
}
