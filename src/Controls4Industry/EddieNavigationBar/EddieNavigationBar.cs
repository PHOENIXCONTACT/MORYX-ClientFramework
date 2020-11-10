// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;

namespace Moryx.WpfToolkit
{
    /// <inheritdoc />
    public class EddieNavigationBar : TabControl
    {
        /// <summary>
        /// Dependency property for the <see cref="IsLocked"/>
        /// </summary>
        public static readonly DependencyProperty IsLockedProperty = DependencyProperty.Register("IsLocked", typeof(bool), typeof(EddieNavigationBar),
                                                                                                    new PropertyMetadata(false, OnIsLockedChanged));

        static EddieNavigationBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieNavigationBar), new FrameworkPropertyMetadata(typeof(EddieNavigationBar)));
        }

        /// <summary>
        /// Indicates wether the bar is locked or not
        /// </summary>
        public bool IsLocked
        {
            get { return (bool)GetValue(IsLockedProperty); }
            set { SetValue(IsLockedProperty, value); }
        }

        private static void OnIsLockedChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            (dependencyObject as EddieNavigationBar)?.OnIsLockedChanged();
        }

        private void OnIsLockedChanged()
        {
            foreach (var item in Items)
            {
                if (item != SelectedItem)
                {
                    var tab = ItemContainerGenerator.ContainerFromItem(item) as TabItem;
                    if (tab != null)
                    {
                        tab.IsEnabled = !IsLocked;
                    }
                }
            }
        }
    }
}
