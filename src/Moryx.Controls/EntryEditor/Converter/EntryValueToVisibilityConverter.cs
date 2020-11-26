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
    /// Converts to a <see cref="Visibility"/> depending on type and <see cref="EntryViewModel.PossibleValues"/> count
    /// </summary>
    public class EntryValueToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Conversion method
        /// </summary>
        /// <param name="value"><see cref="EntryViewModel"/></param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Returns <see cref="Visibility.Visible"/> if value type is a class or a collection and at least one possible value is existent. Otherwise <see cref="Visibility.Hidden"/> is returned.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entry = value as EntryViewModel;
            if (entry == null)
                return Visibility.Hidden;

            var isEntrySettable = (entry.ValueType == EntryValueType.Collection ||
                                  entry.ValueType == EntryValueType.Class &&
                                  entry.PossibleValues != null &&
                                  entry.PossibleValues.Count > 1) && !entry.IsReadOnly;

            if (isEntrySettable)
            {
                if (entry.Parent != null)
                {
                    isEntrySettable = entry.Parent.ValueType != EntryValueType.Collection;
                }
            }

            return isEntrySettable ? Visibility.Visible : Visibility.Collapsed;
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
