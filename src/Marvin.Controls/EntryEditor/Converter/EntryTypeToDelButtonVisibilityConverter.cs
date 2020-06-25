using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Marvin.Serialization;

namespace Marvin.Controls.Converter
{
    public class EntryTypeToDelButtonVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entry = value as EntryViewModel;
            if (entry == null)
                return Visibility.Collapsed;

            return entry.ValueType == EntryValueType.Collection ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}