using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    public class LabeledControlHost : ContentControl
    {
        public static readonly DependencyProperty LabelAProperty = DependencyProperty.Register(
            "LabelA", typeof (string), typeof (LabeledControlHost), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelBProperty = DependencyProperty.Register(
            "LabelB", typeof (string), typeof (LabeledControlHost), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LabelWidthProperty = DependencyProperty.Register(
            "LabelWidth", typeof(double), typeof(LabeledControlHost), new PropertyMetadata(default(double)));

        public static readonly DependencyProperty SharedSizeGroupLabelProperty = DependencyProperty.Register(
            "SharedSizeGroupLabel", typeof (string), typeof (LabeledControlHost), new PropertyMetadata("labelGroup"));

        public static readonly DependencyProperty SharedSizeGroupContentProperty = DependencyProperty.Register(
            "SharedSizeGroupContent", typeof(string), typeof(LabeledControlHost), new PropertyMetadata("contentGroup"));

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

        public string SharedSizeGroupLabel
        {
            get { return (string) GetValue(SharedSizeGroupLabelProperty); }
            set { SetValue(SharedSizeGroupLabelProperty, value); }
        }

        public string SharedSizeGroupContent
        {
            get { return (string)GetValue(SharedSizeGroupContentProperty); }
            set { SetValue(SharedSizeGroupContentProperty, value); }
        }
    }
}