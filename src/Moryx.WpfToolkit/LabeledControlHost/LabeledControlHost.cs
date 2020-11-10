// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Moryx.WpfToolkit
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a label and content combination. This label supports two labels and a content.
    /// </summary>
    [TemplatePart(Name = "LabelHost", Type = typeof(Panel))]
    public class LabeledControlHost : ContentControl
    {
        private Panel _labelHost;

        /// <summary>
        /// Text of label A
        /// </summary>
        public static readonly DependencyProperty LabelAProperty = DependencyProperty.Register(
            "LabelA", typeof (string), typeof (LabeledControlHost), new PropertyMetadata(default(string)));

        /// <summary>
        /// Text of label B
        /// </summary>
        public static readonly DependencyProperty LabelBProperty = DependencyProperty.Register(
            "LabelB", typeof (string), typeof (LabeledControlHost), new PropertyMetadata(default(string)));

        /// <summary>
        /// Minimum width of label
        /// </summary>
        public static readonly DependencyProperty LabelMinWidthProperty = DependencyProperty.Register(
            "LabelMinWidth", typeof(double), typeof(LabeledControlHost), new PropertyMetadata(default(double)));

        /// <summary>
        /// Maximum width of label
        /// </summary>
        public static readonly DependencyProperty LabelMaxWidthProperty = DependencyProperty.Register(
            "LabelMaxWidth", typeof(double), typeof(LabeledControlHost), new PropertyMetadata(double.PositiveInfinity));

        /// <summary>
        /// Shared group size name
        /// </summary>
        public static readonly DependencyProperty SharedSizeGroupNameProperty = DependencyProperty.Register(
            "SharedSizeGroupName", typeof(string), typeof(LabeledControlHost), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, OnSharedSizeGroupName));

        private static void OnSharedSizeGroupName(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            (dependencyObject as LabeledControlHost)?.CalculateSharedLabelWidth();
        }

        static LabeledControlHost()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (LabeledControlHost),
                new FrameworkPropertyMetadata(typeof (LabeledControlHost)));
        }

        /// <summary>
        /// Gets or sets label's minimum width
        /// </summary>
        public double LabelMinWidth
        {
            get { return (double)GetValue(LabelMinWidthProperty); }
            set { SetValue(LabelMinWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets label's maximum width
        /// </summary>
        public double LabelMaxWidth
        {
            get { return (double)GetValue(LabelMaxWidthProperty); }
            set { SetValue(LabelMaxWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets label A
        /// </summary>
        public string LabelA
        {
            get { return (string) GetValue(LabelAProperty); }
            set { SetValue(LabelAProperty, value); }
        }

        /// <summary>
        /// Gets or sets label B
        /// </summary>
        public string LabelB
        {
            get { return (string) GetValue(LabelBProperty); }
            set { SetValue(LabelBProperty, value); }
        }

        /// <summary>
        /// Gets or sets shared size group name
        /// </summary>
        public string SharedSizeGroupName
        {
            get { return (string)GetValue(SharedSizeGroupNameProperty); }
            set { SetValue(SharedSizeGroupNameProperty, value); }
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _labelHost = Template.FindName("LabelHost", this) as Panel;
            if(_labelHost == null)
            {
                throw new InvalidOperationException("LabelHost not found in template");
            }

            _labelHost.SizeChanged += OnLabelHostSizeChanged;
        }

        private void OnLabelHostSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            CalculateSharedLabelWidth();
        }

        private void CalculateSharedLabelWidth()
        {
            var parentPanel = Parent as Panel;

            if (_labelHost == null || parentPanel == null || string.IsNullOrWhiteSpace(SharedSizeGroupName))
                return;

            var labeledControlHosts = parentPanel.Children.OfType<LabeledControlHost>().Where(l => l.SharedSizeGroupName == SharedSizeGroupName && l._labelHost != null).ToList();

            if (labeledControlHosts.Count > 1)
            {
                var maxWidth = labeledControlHosts.Max(l => l._labelHost.RenderSize.Width + l._labelHost.Margin.Left + l._labelHost.Margin.Right);

                foreach (var labeledControlHost in labeledControlHosts)
                {
                    labeledControlHost.LabelMinWidth = maxWidth;
                }
            }
        }
    }
}
