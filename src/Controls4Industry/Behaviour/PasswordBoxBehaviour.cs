﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace C4I
{
    /// <summary>
    /// Makes <see cref="PasswordBox.Password"/> bindable
    /// </summary>
    public class PasswordBoxBehaviour : Behavior<PasswordBox>
    { 
        /// <summary>
        /// Dependency property for the <see cref="PasswordBox.Password"/>
        /// </summary>
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxBehaviour),
                    new FrameworkPropertyMetadata(null, OnPasswordChangedCallback));

        /// <summary>
        /// Sets password on associated <see cref="PasswordBox"/>
        /// </summary>
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Password = Password;
            AssociatedObject.PasswordChanged += OnAssociatedObjectPasswordChanged;
        }

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            AssociatedObject.PasswordChanged += OnAssociatedObjectPasswordChanged;

            base.OnDetaching();
        }

        private void OnAssociatedObjectPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = AssociatedObject.Password;
        }

        private static void OnPasswordChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var behaviour = (PasswordBoxBehaviour) d;
            if (behaviour.AssociatedObject != null)
            {
                if (behaviour.AssociatedObject.Password != behaviour.Password)
                {
                    behaviour.AssociatedObject.Password = behaviour.Password;
                }
            }
        }
    }
}