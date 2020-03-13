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
    /// Converter class for converting boolean values into Visibility
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Trigger value. When set to true then boolean true value would be converted to Visible.
        /// When set to false then boolean true value would be converted to Collapsed/Hidden
        /// </summary>
        public bool TriggerValue { get; set; } = true;

        /// <summary>
        /// If set to true, then return would be Visibility.Hidden, when set to false then return value would be Visibility.Collapsed
        /// </summary>
        public bool IsHidden { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Converts boolean value <see cref="P:C4I.BooleanToVisibilityConverter.TriggerValue" /> and <see cref="P:C4I.BooleanToVisibilityConverter.IsHidden" />
        /// </summary>
        /// <param name="value">Boolean</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;

            var objValue = (bool)value;

            if ((objValue && !TriggerValue && IsHidden) || (!objValue && TriggerValue && IsHidden))
            {
                return Visibility.Hidden;
            }

            if ((objValue && !TriggerValue && !IsHidden) || (!objValue && TriggerValue && !IsHidden))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
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
