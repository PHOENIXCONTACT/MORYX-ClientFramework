// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <summary>
    /// Custom busy indicator
    /// </summary>
    public class EddieBusyIndicator : ContentControl
    {
        /// <summary>
        /// Dependency property for the <see cref="IndicatorWidth"/>
        /// </summary>
        public static readonly DependencyProperty IndicatorWidthProperty = DependencyProperty.Register(
            "IndicatorWidth", typeof(double), typeof(EddieBusyIndicator), new PropertyMetadata(double.PositiveInfinity));

        /// <summary>
        /// Dependency property for the <see cref="IndicatorHeight"/>
        /// </summary>
        public static readonly DependencyProperty IndicatorHeightProperty = DependencyProperty.Register(
            "IndicatorHeight", typeof(double), typeof(EddieBusyIndicator), new PropertyMetadata(double.PositiveInfinity));

        /// <summary>
        /// Dependency property for the <see cref="IsIndicatorEnabled"/>
        /// </summary>
        public static readonly DependencyProperty IsIndicatorEnabledProperty = DependencyProperty.Register(
            "IsIndicatorEnabled", typeof(bool), typeof(EddieBusyIndicator), new PropertyMetadata(false));


        static EddieBusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieBusyIndicator), new FrameworkPropertyMetadata(typeof(EddieBusyIndicator)));
        }

        /// <summary>
        /// The width of the indicator
        /// </summary>
        public double IndicatorWidth
        {
            get { return (double)GetValue(IndicatorWidthProperty); }
            set { SetValue(IndicatorWidthProperty, value); }
        }

        /// <summary>
        /// The height of the indicator
        /// </summary>
        public double IndicatorHeight
        {
            get { return (double)GetValue(IndicatorHeightProperty); }
            set { SetValue(IndicatorHeightProperty, value); }
        }

        /// <summary>
        /// The height of the indicator
        /// </summary>
        public bool IsIndicatorEnabled
        {
            get { return (bool)GetValue(IsIndicatorEnabledProperty); }
            set { SetValue(IsIndicatorEnabledProperty, value); }
        }
    }
}
