// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;

namespace Moryx.WpfToolkit
{
    /// <summary> 
    /// Control that implements a list of selectable items.
    /// </summary> 
    public class EddieListBox : ListBox
    {
        /// <summary>
        /// Initializes the <see cref="EddieListBox"/> class.
        /// </summary>
        static EddieListBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieListBox), new FrameworkPropertyMetadata(typeof(EddieListBox)));
        }

        /// 
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is EddieListBoxItem;
        }

        ///
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new EddieListBoxItem();
        }
    }
}
