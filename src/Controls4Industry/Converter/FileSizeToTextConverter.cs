using System;
using System.Globalization;
using System.Windows.Data;

namespace C4I
{
    public class FileSizeToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ret = "";

            if (!(value is long)) 
                return ret;

            var fileSize = (long)value;

            if (fileSize < 1024)
            {
                ret = string.Format("{0} bytes", fileSize);
            }
            else
            {
                var f = Math.Round(fileSize/1024.0, 2);
                if (f < 1000)
                {
                    ret = string.Format("{0} KB", f);
                }
                else
                {
                    f = Math.Round(f / 1024.0, 2);
                    if (f < 1000)
                    {
                        ret = string.Format("{0} MB", f);
                    }
                    else
                    {
                        f = Math.Round(f / 1024.0, 2);
                        if (f < 1000)
                        {
                            ret = string.Format("{0} GB", f);
                        }
                    }
                }
            }

            return ret;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
