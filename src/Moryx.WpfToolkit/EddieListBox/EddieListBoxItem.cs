// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;

namespace Moryx.WpfToolkit
{
    /// <summary> 
    /// A child of a <see cref="EddieListBox" />.
    /// </summary> 
    public class EddieListBoxItem : ListBoxItem
    {
        static EddieListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieListBoxItem), new FrameworkPropertyMetadata(typeof(EddieListBoxItem)));
        }
    }
}
