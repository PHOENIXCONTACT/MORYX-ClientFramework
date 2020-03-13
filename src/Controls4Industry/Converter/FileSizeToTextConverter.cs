// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows.Data;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Converts a long value to a string that represents a file size with unit
    /// </summary>
    public class FileSizeToTextConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Conversion method
        /// </summary>
        /// <param name="value">Long value</param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Returns a file string with the corresponding unit: bytes, KB, MB or GB</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ret = "";

            if (!(value is long)) 
                return ret;

            var fileSize = (long)value;

            if (fileSize < 1024)
            {
                ret = $"{fileSize} bytes";
            }
            else
            {
                var f = Math.Round(fileSize/1024.0, 2);
                if (f < 1000)
                {
                    ret = $"{f} KB";
                }
                else
                {
                    f = Math.Round(f / 1024.0, 2);
                    if (f < 1000)
                    {
                        ret = $"{f} MB";
                    }
                    else
                    {
                        f = Math.Round(f / 1024.0, 2);
                        if (f < 1000)
                        {
                            ret = $"{f} GB";
                        }
                    }
                }
            }

            return ret;
        }

        /// <inheritdoc />
        /// <summary>
        /// Not supported
        /// </summary>
        /// <exception cref="T:System.NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
