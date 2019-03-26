using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Converts a boolean value to GridLength instance
    /// </summary>
    public class BooleanToGridRowHeightConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Returns a starred <see cref="GridLength"/> if value is true otherwise a zero length <see cref="GridLength"/></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (this.AsBool(value)) ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }

        /// <inheritdoc />
        /// <summary>
        /// Not supported
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
