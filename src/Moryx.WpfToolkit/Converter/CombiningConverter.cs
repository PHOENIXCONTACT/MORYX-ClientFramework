// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows.Data;

namespace Moryx.WpfToolkit
{
    /// <inheritdoc />
    /// <summary>
    /// Converts an input value through a chain of two configured <see cref="T:System.Windows.Data.IValueConverter" />'s. The result of the previous conversion
    /// step is used for the second conversion.
    /// </summary>
    public class CombiningConverter : IValueConverter
    {
        /// <summary>
        /// <see cref="IValueConverter"/> that is used for the first conversion step
        /// </summary>
        public IValueConverter Converter1 { get; set; }

        /// <summary>
        /// <see cref="IValueConverter"/> that is used for the second conversion step
        /// </summary>
        public IValueConverter Converter2 { get; set; }

        /// <summary>
        /// Conversion method
        /// </summary>
        /// <param name="value">Any object instance. Please not that the correct converters have been selected. This value is only converted by the first converter.</param>
        /// <param name="targetType">Value is passed to both converters</param>
        /// <param name="parameter">Value is passed to both converters</param>
        /// <param name="culture">Value is passed to both converters</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var convertedValue = Converter1.Convert(value, targetType, parameter, culture);
            return Converter2.Convert(convertedValue, targetType, parameter, culture);
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
