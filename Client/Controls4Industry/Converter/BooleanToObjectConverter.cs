using System;
using System.Globalization;
using System.Windows.Data;

namespace C4I
{
    public class BooleanToObjectConverter : IValueConverter
    {
        public object TrueObject { get; set; }
        public object FalseObject { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return this.AsBool(value) ? TrueObject : FalseObject;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
