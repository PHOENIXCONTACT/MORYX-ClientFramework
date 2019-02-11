using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Marvin.Controls.Converter
{
    internal class Base64StringLengthToByteLength : IValueConverter
    {
        private const int OneKilobyte = 1024;
        private const int OneMegabyte = 1048576;
        private const int OneGigabyte = 1073741824;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var base64String = value as string;

            if (string.IsNullOrEmpty(base64String))
            {
                return "0 Byte";
            }

            var characterCount = base64String.Length;
            var paddingCount = base64String.Substring(characterCount - 2, 2).Count(c => c == '=');
            var byteCount =  3 * (characterCount / 4) - paddingCount;

            if (byteCount < OneKilobyte)
            {
                return $"{byteCount} Bytes";
            }

            if (byteCount < OneMegabyte)
            {
                return $"{byteCount / OneKilobyte:F} KB";
            }

            if (byteCount < OneGigabyte)
            {
                return $"{byteCount / OneMegabyte:F} MB";
            }

            return $"{byteCount / OneGigabyte:F} GB"; ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}