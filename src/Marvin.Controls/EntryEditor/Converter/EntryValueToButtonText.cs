using System;
using System.Globalization;
using System.Windows.Data;
using Marvin.Serialization;

namespace Marvin.Controls.Converter
{
    public class EntryValueToButtonText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entry = value as EntryViewModel;
            if (entry == null)
                return "";

            if (entry.ValueType == EntryValueType.Collection)
                return "Add entry";

            if (entry.ValueType == EntryValueType.Class)
                return "Replace with";

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
