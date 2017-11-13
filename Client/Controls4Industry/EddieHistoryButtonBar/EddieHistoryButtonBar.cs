using System;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    public class EddieHistoryButtonBar : Control
    {
        static EddieHistoryButtonBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieHistoryButtonBar), new FrameworkPropertyMetadata(typeof(EddieHistoryButtonBar)));
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof (string), typeof (EddieHistoryButtonBar), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public event EventHandler ClickForward;
        protected virtual void OnClickForward()
        {
            var handler = ClickForward;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler ClickPrevious;
        protected virtual void OnClickPreviews()
        {
            var handler = ClickPrevious;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler ClickShowHistory;
        protected virtual void OnClickShowHistory()
        {
            var handler = ClickShowHistory;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public override void OnApplyTemplate()
        {
            var button = GetTemplateChild("PART_NextButton") as Button;
            if (button != null)
            {
                button.Click += FordwardClicked;
            }

            button = GetTemplateChild("PART_ListHistoryButton") as Button;
            if (button != null)
            {
                button.Click += ShowHistoryClicked;
            }

            button = GetTemplateChild("PART_PreviousButton") as Button;
            if (button != null)
            {
                button.Click += PreviewsClicked;
            }

            base.OnApplyTemplate();
        }

        private void FordwardClicked(object sender, RoutedEventArgs e)
        {
            OnClickForward();
        }

        private void PreviewsClicked(object sender, RoutedEventArgs e)
        {
            OnClickPreviews();
        }

        private void ShowHistoryClicked(object sender, RoutedEventArgs e)
        {
            OnClickShowHistory();
        }
    }
}