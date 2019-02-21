using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Marvin.Controls.Converter
{
    /// <summary>
    /// Cut a text at the first occurence of a delimiter.
    /// </summary>
    public class TextToDelimiteredTextConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            var param = parameter as string;
            if (text == null)
            {
                throw new InvalidOperationException("Value is null or not a string");
            }

            if (param == null)
            {
                throw new InvalidOperationException("Parameter is null or not a string");
            }

            return text.Split(new [] { param }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();

        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
