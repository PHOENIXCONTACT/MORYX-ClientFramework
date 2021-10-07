// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;

namespace Moryx.WpfToolkit
{
    /// <summary>
    /// Helper class for the password box to bind the password
    /// </summary>
    public static class PasswordBoxHelper
    {
        /// <summary>
        /// Attached property for the bound password
        /// </summary>
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        /// <summary>
        /// Attached property for the bind password
        /// </summary>
        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false, OnBindPasswordChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxHelper), new PropertyMetadata(false));

        /// <summary>
        /// Gets the <see cref="BindPassword"/> value
        /// </summary>
        public static bool GetBindPassword(DependencyObject dp) =>
            (bool)dp.GetValue(BindPassword);

        /// <summary>
        /// Sets the <see cref="BindPassword"/> value
        /// </summary>
        public static void SetBindPassword(DependencyObject dp, bool value) =>
            dp.SetValue(BindPassword, value);

        /// <summary>
        /// Gets the <see cref="BoundPassword"/> value
        /// </summary>
        public static string GetBoundPassword(DependencyObject dp) =>
            (string)dp.GetValue(BoundPassword);

        /// <summary>
        /// Sets the <see cref="BoundPassword"/> value
        /// </summary>
        public static void SetBoundPassword(DependencyObject dp, string value) =>
            dp.SetValue(BoundPassword, value);

        private static bool GetUpdatingPassword(DependencyObject dp) =>
            (bool)dp.GetValue(UpdatingPassword);

        private static void SetUpdatingPassword(DependencyObject dp, bool value) =>
            dp.SetValue(UpdatingPassword, value);

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var box = d as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (d == null || !GetBindPassword(d))
                return;

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            var newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
                box.Password = newPassword;

            box.PasswordChanged += HandlePasswordChanged;
        }

        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            // When the BindPassword attached property is set on a PasswordBox,
            // start listening to its PasswordChanged event
            if (dp is not PasswordBox box)
                return;

            var wasBound = (bool)e.OldValue;
            var needToBind = (bool)e.NewValue;

            if (wasBound)
                box.PasswordChanged -= HandlePasswordChanged;

            if (needToBind)
                box.PasswordChanged += HandlePasswordChanged;
        }

        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            var box = sender as PasswordBox;

            // set a flag to indicate that we're updating the password
            SetUpdatingPassword(box, true);
            // push the new password into the BoundPassword property
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }
    }
}
