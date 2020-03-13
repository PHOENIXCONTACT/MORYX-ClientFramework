// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// A button bar for controlling the history
    /// </summary>
    public class EddieHistoryButtonBar : Control
    {
        static EddieHistoryButtonBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieHistoryButtonBar), new FrameworkPropertyMetadata(typeof(EddieHistoryButtonBar)));
        }

        /// <summary>
        /// Text of the <see cref="EddieHistoryButtonBar"/>
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof (string), typeof (EddieHistoryButtonBar), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets the text
        /// </summary>
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Event that gets fired when forward button gets clicked
        /// </summary>
        public event EventHandler ClickForward;
        /// <summary>
        /// Method that gets called when forward button gets clicked
        /// </summary>
        protected virtual void OnClickForward()
        {
            var handler = ClickForward;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Event that gets fired when previous button gets clicked
        /// </summary>
        public event EventHandler ClickPrevious;
        /// <summary>
        /// Method that gets called when previous button gets clicked
        /// </summary>
        protected virtual void OnClickPreviews()
        {
            var handler = ClickPrevious;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Event that gets fired when show history button gets clicked
        /// </summary>
        public event EventHandler ClickShowHistory;
        /// <summary>
        /// Method that gets called when show history button gets clicked
        /// </summary>
        protected virtual void OnClickShowHistory()
        {
            var handler = ClickShowHistory;
            handler?.Invoke(this, EventArgs.Empty);
        }

        /// <inheritdoc />
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
