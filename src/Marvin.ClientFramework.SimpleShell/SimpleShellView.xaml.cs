// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows.Controls;
using Marvin.ClientFramework.Shell;

namespace Marvin.ClientFramework.SimpleShell
{
    /// <summary>
    /// Interaction logic for SimpleShellView.xaml
    /// </summary>
    public partial class SimpleShellView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleShellView"/> class.
        /// </summary>
        public SimpleShellView()
        {
            InitializeComponent();

        }

        private void OnNavigationChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var wrapper = (ModuleWrapper)e.AddedItems[0];
                ((SimpleShellViewModel)DataContext).SelectModule(wrapper);
            }
        }
    }
}
