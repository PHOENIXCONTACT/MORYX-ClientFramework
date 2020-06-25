// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Moryx.Serialization;

namespace Moryx.Controls.Converter
{
    /// <summary>
    /// Returns <see cref="Visibility"/> depending on eth sub entry count
    /// </summary>
    public class SubEntriesToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Conversion method
        /// </summary>
        /// <param name="value"><see cref="T:Moryx.Controls.EntryViewModel" /></param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Returns <see cref="Visibility.Visible"/> if entry is of type collection or class and has at least one sub entry. Otherwise <see cref="Visibility.Collapsed"/>.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hasSubEntries = false;
            var entry = value as EntryViewModel;

            if (entry == null) 
                return Visibility.Collapsed;

            if (entry.ValueType == EntryValueType.Collection || entry.ValueType == EntryValueType.Class)
            {
                hasSubEntries = true;
            }

            return hasSubEntries ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        /// <summary>
        /// Not supported
        /// </summary>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
