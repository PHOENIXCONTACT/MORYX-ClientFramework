// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Specialized <see cref="ListViewItem"/> for <see cref="EddieListBox"/>
    /// </summary>
    public class EddieListViewItem: ListViewItem
    {
        static EddieListViewItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieListViewItem), new FrameworkPropertyMetadata(typeof(EddieListViewItem)));
        }
    }
}
