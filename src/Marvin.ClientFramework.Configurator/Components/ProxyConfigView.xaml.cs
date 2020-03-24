// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;

namespace Marvin.ClientFramework.Configurator
{
    /// <summary>
    /// Interaction logic for ProxyConfigView.xaml
    /// </summary>
    public partial class ProxyConfigView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProxyConfigView"/> class.
        /// </summary>
        public ProxyConfigView()
        {
            InitializeComponent();
        }

        private void DefaultCheckBox_OnChecked(object sender, RoutedEventArgs e)
        {
            var checkbox = (CheckBox) sender;

            if (checkbox.IsChecked.HasValue && checkbox.IsChecked.Value)
            {
                Adress.Visibility = Visibility.Collapsed;
                Port.Visibility = Visibility.Collapsed;
            }
            else
            {
                Adress.Visibility = Visibility.Visible;
                Port.Visibility = Visibility.Visible;
            }

        }
    }
}
