// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows.Controls;
using System.Windows.Input;

namespace Moryx.ClientFramework.Dialog
{
    /// <summary>
    /// Interaction logic for DialogConductorView.xaml
    /// </summary>
    public partial class DialogConductorView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogConductorView"/> class.
        /// </summary>
        public DialogConductorView()
        {
            InitializeComponent();
            // limit all the keyboard navigations to this UI (and its childs)
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
            KeyboardNavigation.SetControlTabNavigation(this, KeyboardNavigationMode.Cycle);
            KeyboardNavigation.SetDirectionalNavigation(this, KeyboardNavigationMode.Cycle);
        }
    }
}
