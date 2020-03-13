// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace C4I
{
    /// <summary>
    /// Coverter so set the margin to tree view elements regarding to their hierary
    /// </summary>
    internal class TreeViewHierarchyToThicknessConverter : IValueConverter
    {
        ///
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TreeViewItem;
            if (item == null) 
                return null;

            int offset;
            if (!int.TryParse((string) parameter, out offset)) 
                return null;

            var hierarchicalThickness = new Thickness();

            var hierachy = 0;

            var parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeView))
            {
                if (parent is TreeViewItem) hierachy++;
                parent = VisualTreeHelper.GetParent(parent);
            }

            hierarchicalThickness.Left = offset * hierachy;
            return hierarchicalThickness;
        }

        ///
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
