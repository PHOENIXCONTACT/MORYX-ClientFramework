// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows.Data;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Converts a boolean value to it's string representation
    /// </summary>
    public class StringToBooleanConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Converts value to boolean
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Boolean value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.AsBool(value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Converts
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>String value: "True" or "False"</returns>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.AsBool(value) ? "True" : "False";
        }
    }
}
