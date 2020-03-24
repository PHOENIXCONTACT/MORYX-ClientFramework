// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Media;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Loader view
    /// </summary>
    public partial class LoaderView : ILoaderView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoaderView"/> class.
        /// </summary>
        public LoaderView()
        {
            InitializeComponent();

            StatusMessage = "One moment please ...";
        }

        /// <summary>
        /// Maximum progress value
        /// </summary>
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof (int), typeof (LoaderView), new PropertyMetadata(default(int)));

        /// <inheritdoc />
        public int Maximum
        {
            get { return (int) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        /// <summary>
        /// Current progress value
        /// </summary>
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof (int), typeof (LoaderView), new PropertyMetadata(default(int)));

        /// <inheritdoc />
        public int Value
        {
            get { return (int) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        /// <summary>
        /// Status message
        /// </summary>
        public static readonly DependencyProperty StatusMessageProperty = DependencyProperty.Register(
            "StatusMessage", typeof (string), typeof (LoaderView), new PropertyMetadata(default(string)));

        /// <inheritdoc />
        public string StatusMessage
        {
            get { return (string) GetValue(StatusMessageProperty); }
            set { SetValue(StatusMessageProperty, value); }
        }

        /// <summary>
        /// App name
        /// </summary>
        public static readonly DependencyProperty AppNameProperty = DependencyProperty.Register(
            "AppName", typeof (string), typeof (LoaderView), new PropertyMetadata(default(string)));

        /// <inheritdoc />
        public string AppName
        {
            get { return (string) GetValue(AppNameProperty); }
            set { SetValue(AppNameProperty, value); }
        }

        /// <summary>
        /// Gets called to indicate an error
        /// </summary>
        public void IndicateError()
        {
            ProgressBar.Foreground = new SolidColorBrush(Colors.Red);
        }
    }
}
