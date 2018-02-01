using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    [TemplatePart(Name = "LabelHost", Type = typeof(Panel))]
    public class LabeledControlHost : ContentControl
    {
        private Panel _labelHost;

        public static readonly DependencyProperty LabelAProperty = DependencyProperty.Register(
            "LabelA", typeof (string), typeof (LabeledControlHost), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelBProperty = DependencyProperty.Register(
            "LabelB", typeof (string), typeof (LabeledControlHost), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(
            "LabelWidth", typeof(double), typeof(LabeledControlHost), new PropertyMetadata(default(double)));

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

        public double LabelWidth
        {
            get { return (double)GetValue(LabelWidthProperty); }
            set { SetValue(LabelWidthProperty, value); }
        }

        public string LabelA
        {
            get { return (string) GetValue(LabelAProperty); }
            set { SetValue(LabelAProperty, value); }
        }

        public string LabelB
        {
            get { return (string) GetValue(LabelBProperty); }
            set { SetValue(LabelBProperty, value); }
        }

        public string SharedSizeGroupName
        {
            get { return (string)GetValue(SharedSizeGroupNameProperty); }
            set { SetValue(SharedSizeGroupNameProperty, value); }
        }

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

            var labeledControlHosts = parentPanel.Children.OfType<LabeledControlHost>().Where(l => l.SharedSizeGroupName == SharedSizeGroupName).ToList();

            if (labeledControlHosts.Count > 1)
            {
                var maxWidth = labeledControlHosts.Max(l => l._labelHost.RenderSize.Width + l._labelHost.Margin.Left + l._labelHost.Margin.Right);

                foreach (var labeledControlHost in labeledControlHosts)
                {
                    labeledControlHost.LabelWidth = maxWidth;
                }
            }
        }
    }
}