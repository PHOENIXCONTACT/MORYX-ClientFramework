using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace C4I
{
    /// <summary>
    /// Converts an integer value to <see cref="Visibility"/> value
    /// </summary>
    public class IntToVisibilityConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Performs conversion
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Returns collapsed if value is null or 0, otherwise visible</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Visibility.Collapsed;

            if (System.Convert.ToInt32(value) > 0)
                return Visibility.Visible;

            return Visibility.Collapsed;
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
