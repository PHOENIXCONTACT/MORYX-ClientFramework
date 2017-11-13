using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Marvin.Serialization;

namespace Marvin.Controls.Converter
{
    public class EntryValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entry = value as EntryViewModel;
            if (entry == null)
                return Visibility.Hidden;

            if (entry.ValueType == EntryValueType.Collection)
                return Visibility.Visible;

            if (entry.ValueType == EntryValueType.Class && entry.PossibleValues != null)
                return Visibility.Visible;

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
