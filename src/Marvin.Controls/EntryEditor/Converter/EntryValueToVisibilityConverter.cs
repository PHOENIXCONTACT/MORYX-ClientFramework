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

            var isEntrySettable = entry.ValueType == EntryValueType.Collection ||
                                  entry.ValueType == EntryValueType.Class &&
                                  entry.PossibleValues != null &&
                                  entry.PossibleValues.Count > 1;

            if (isEntrySettable)
            {
                if (entry.Parent != null)
                {
                    isEntrySettable = entry.Parent.ValueType != EntryValueType.Collection;
                }
            }

            return isEntrySettable ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
