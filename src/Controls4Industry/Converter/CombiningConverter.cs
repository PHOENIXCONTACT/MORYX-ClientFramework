using System;
using System.Windows.Data;

namespace C4I
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

        #region IValueConverter Members

        /// <summary>
        /// Conversion method
        /// </summary>
        /// <param name="value">Any object instance. Please not that the correct converters have been selected. This value is only converted by the first converter.</param>
        /// <param name="targetType">Value is passed to both converters</param>
        /// <param name="parameter">Value is passed to both converters</param>
        /// <param name="culture">Value is passed to both converters</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object convertedValue = Converter1.Convert(value, targetType, parameter, culture);
            return Converter2.Convert(convertedValue, targetType, parameter, culture);
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
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
