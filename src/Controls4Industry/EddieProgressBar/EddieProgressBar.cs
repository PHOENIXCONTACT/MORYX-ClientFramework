using System;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    public class EddieProgressBar : ProgressBar
    {
        static EddieProgressBar() 
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieProgressBar), new FrameworkPropertyMetadata(typeof(EddieProgressBar)));
        }

        public static readonly DependencyProperty TextFormatProperty = DependencyProperty.Register(
            "TextFormat", typeof (string), typeof (EddieProgressBar), new PropertyMetadata(default(string)));

        public string TextFormat
        {
            get { return (string) GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof (string), typeof (EddieProgressBar), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty AnimationVisibilityProperty = DependencyProperty.Register(
            "AnimationVisibility", typeof (Visibility), typeof (EddieProgressBar), new PropertyMetadata(Visibility.Visible));

        public Visibility AnimationVisibility
        {
            get { return (Visibility) GetValue(AnimationVisibilityProperty); }
            set { SetValue(AnimationVisibilityProperty, value); }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateText();
        }

        protected override void OnValueChanged(double oldValue, double newValue)
        {
            base.OnValueChanged(oldValue, newValue);

            UpdateText();

            AnimationVisibility = Value.Equals(Maximum) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void UpdateText()
        {
            if (string.IsNullOrEmpty(TextFormat))
            {
                return;
            }

            var percent = Math.Round((Value/Maximum)*100, 0, MidpointRounding.ToEven);

            Text = TextFormat
                .Replace(@"%percentage", "" + percent)
                .Replace(@"%value", "" + Value)
                .Replace("%min", "" + Minimum)
                .Replace("%max", "" + Maximum);
        }
    }
}
