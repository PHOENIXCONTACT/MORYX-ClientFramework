// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Moryx.WpfToolkit
{
    /// <inheritdoc />
    /// <summary>
    /// Button for the module bar
    /// </summary>
    public class EddieModuleButton : Control
    {
        static EddieModuleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieModuleButton), new FrameworkPropertyMetadata(typeof(EddieModuleButton)));
        }

        /// <summary>
        /// Text that is displayed on the button
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(EddieModuleButton), new PropertyMetadata(default(string)));

        /// <summary>
        /// Get or sets the text on the button
        /// </summary>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        /// <see cref="EddieButtonStyle"/> for this button
        /// </summary>
        public static readonly DependencyProperty EddieStyleProperty = DependencyProperty.Register(
            "EddieStyle", typeof(EddieButtonStyle), typeof(EddieModuleButton), new PropertyMetadata(EddieButtonStyle.Green));


        /// <summary>
        /// Get or sets the <see cref="EddieButtonStyle"/> for this button
        /// </summary>
        public EddieButtonStyle EddieStyle
        {
            get => (EddieButtonStyle)GetValue(EddieStyleProperty);
            set => SetValue(EddieStyleProperty, value);
        }

        /// <summary>
        /// Notification counter of this button
        /// </summary>
        public static readonly DependencyProperty NotificationsProperty = DependencyProperty.Register(
            nameof(Notifications), typeof (int), typeof (EddieModuleButton), new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets the notification counter
        /// </summary>
        public int Notifications
        {
            get => (int) GetValue(NotificationsProperty);
            set => SetValue(NotificationsProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon),
            typeof(Geometry), typeof(EddieModuleButton),
            new FrameworkPropertyMetadata(null,  FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), null);

        public Geometry Icon
        {
            get => (Geometry) GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// Command that is executed when the button gets clicked
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof (ICommand), typeof (EddieModuleButton), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// Gets or sets the command
        /// </summary>
        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Attached property to set the submenu content template
        /// </summary>
        public static readonly DependencyProperty PopupTemplateProperty = DependencyProperty.Register(
            "PopupTemplate", typeof(DataTemplate), typeof(EddieModuleButton), new PropertyMetadata(default(DataTemplate)));

        /// <summary>
        /// Gets or sets the submenu template
        /// </summary>
        public DataTemplate PopupTemplate
        {
            get => (DataTemplate)GetValue(PopupTemplateProperty);
            set => SetValue(PopupTemplateProperty, value);
        }

        /// <summary>
        /// Click event that is raised when the button gets clicked
        /// </summary>
        public event RoutedEventHandler Click;

        /// <summary>
        /// Raises the <see cref="Click"/> event
        /// </summary>
        protected virtual void RaiseClick(RoutedEventArgs e)
        {
            var handler = Click;
            handler?.Invoke(this, e);
        }

        /// <inheritdoc />
        public override void OnApplyTemplate()
        {
            if (GetTemplateChild("PART_MainButton") is EddieButton button)
            {
                button.Click += OnMainButtonClick;
                button.Command = Command;
            }

            base.OnApplyTemplate();
        }

        private void OnMainButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            RaiseClick(routedEventArgs);
        }
    }
}
