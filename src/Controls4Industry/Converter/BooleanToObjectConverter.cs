using System;
using System.Globalization;
using System.Windows.Data;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Converts a boolean value either to TrueObject or FalseObject
    /// </summary>
    public class BooleanToObjectConverter : IValueConverter
    {
        /// <summary>
        /// Object that will be retrieved if boolean value is true
        /// </summary>
        public object TrueObject { get; set; }

        /// <summary>
        /// Object that will be retrieved if boolean value is false
        /// </summary>
        public object FalseObject { get; set; }

        /// <summary>
        /// Retrieves either TrueObject on true or FalseObject on false
        /// </summary>
        /// <param name="value">Boolean value</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.AsBool(value) ? TrueObject : FalseObject;
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
