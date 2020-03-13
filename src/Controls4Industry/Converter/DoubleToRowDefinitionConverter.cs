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
    /// Converts a double value to <see cref="T:System.Windows.GridLength" /> typed value
    /// </summary>
    public class DoubleToRowDefinitionConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Double to <see cref="T:System.Windows.GridLength" /> conversion
        /// </summary>
        /// <param name="value">Double</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Either a starred <see cref="T:System.Windows.GridLength" /> if the provided value is not a double. Or a pixel based <see cref="T:System.Windows.GridLength" />
        /// where the pixel value is as same as the given double value.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gridLength = new GridLength(0.0, GridUnitType.Star);

            if (value is double)
            {
                gridLength = new GridLength((double)value, GridUnitType.Pixel);
            }

            return gridLength;
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
