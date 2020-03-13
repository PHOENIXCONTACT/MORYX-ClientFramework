// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Converts a null value to collapsed
    /// </summary>
    public class NullToCollapsedConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Value conversion
        /// </summary>
        /// <param name="value">Object instance</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Returns collapsed if value is null, otherwise visible</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? Visibility.Collapsed : Visibility.Visible;
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
