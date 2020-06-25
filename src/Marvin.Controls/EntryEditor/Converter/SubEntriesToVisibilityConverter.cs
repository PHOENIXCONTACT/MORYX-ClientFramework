using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Marvin.Serialization;

namespace Marvin.Controls.Converter
{
    public class SubEntriesToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hasSubEntries = false;
            var entry = value as EntryViewModel;

            if (entry == null) 
                return Visibility.Collapsed;

            if (entry.ValueType == EntryValueType.Collection || entry.ValueType == EntryValueType.Class)
            {
                hasSubEntries = true;
            }

            return hasSubEntries ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
