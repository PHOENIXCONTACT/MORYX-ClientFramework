// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows;
using System.Windows.Controls;

namespace Moryx.WpfToolkit
{
    /// <inheritdoc />
    /// <summary>
    /// Implementation of <see cref="ProgressBar"/> with additional features like text with percentage / min and max value
    /// </summary>
    public class EddieProgressBar : ProgressBar
    {
        static EddieProgressBar() 
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieProgressBar), new FrameworkPropertyMetadata(typeof(EddieProgressBar)));
        }

        /// <summary>
        /// Indicates the text format of the shown text
        /// </summary>
        public static readonly DependencyProperty TextFormatProperty = DependencyProperty.Register(
            "TextFormat", typeof (string), typeof (EddieProgressBar), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets the TextFormat
        /// </summary>
        public string TextFormat
        {
            get { return (string) GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }

        /// <summary>
        /// Text to be shown on the progress bar
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof (string), typeof (EddieProgressBar), new PropertyMetadata(default(string)));

        /// <summary>
        /// Gets or sets the text to be shown
        /// </summary>
        public string Text
        {
            get { return (string) GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Animation visibility property
        /// </summary>
        public static readonly DependencyProperty AnimationVisibilityProperty = DependencyProperty.Register(
            "AnimationVisibility", typeof (Visibility), typeof (EddieProgressBar), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Gets or sets the visibility of the animation
        /// </summary>
        public Visibility AnimationVisibility
        {
            get { return (Visibility) GetValue(AnimationVisibilityProperty); }
            set { SetValue(AnimationVisibilityProperty, value); }
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            UpdateText();
        }

        /// <inheritdoc />
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
