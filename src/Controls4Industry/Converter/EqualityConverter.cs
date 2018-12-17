using System;
using System.Globalization;
using System.Windows.Data;

namespace C4I
{
    /// <summary>
    /// Converter to compare two objects with <see cref="object.Equals(object)"/>
    /// </summary>
    public class EqualityConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => 
            values.Length >= 2 && values[0].Equals(values[1]);

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
