// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows.Data;

namespace C4I
{
    /// <summary>
    /// Inverses a boolean value
    /// </summary>
    public class InverseBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Inverses a boolean value
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>False if value is null or an inversed boolean value</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value != null && !(bool)value;
        }

        /// <inheritdoc />
        /// <summary>
        /// Not supported
        /// </summary>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
