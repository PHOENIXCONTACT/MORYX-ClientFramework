using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace C4I
{
    public class DoubleToRowDefinitionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var gridLength = new GridLength(0.0, GridUnitType.Star);

            if (value is double)
            {
                gridLength = new GridLength((double)value, GridUnitType.Pixel);
            }

            return gridLength;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
