using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace C4I
{
    /// <summary>
    /// Converter class for converting boolean values into Visibility
    /// </summary>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        private bool _triggerValue = true;
        private bool _isHidden = false;

        /// <summary>
        /// Trigger value. When set to true then boolean true value would be converted to Visible.
        /// When set to false then boolean true value would be converted to Collapsed/Hidden
        /// </summary>
        public bool TriggerValue
        {
            get { return _triggerValue; }
            set { _triggerValue = value; }
        }

        /// <summary>
        /// If set to true, then return would be Visibility.Hidden, when set to false then return value would be Visibility.Collapsed
        /// </summary>
        public bool IsHidden
        {
            get { return _isHidden; }
            set { _isHidden = value; }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return DependencyProperty.UnsetValue;

            var objValue = (bool)value;

            if ((objValue && !TriggerValue && IsHidden) || (!objValue && TriggerValue && IsHidden))
            {
                return Visibility.Hidden;
            }

            if ((objValue && !TriggerValue && !IsHidden) || (!objValue && TriggerValue && !IsHidden))
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
