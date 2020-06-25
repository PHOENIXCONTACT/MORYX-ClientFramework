// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Moryx.Controls.Properties;

namespace Moryx.Controls.Converter
{
    /// <summary>
    /// Cut a text at the first occurence of a delimiter.
    /// </summary>
    public class TextToDelimiteredTextConverter : IValueConverter
    {
        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var text = value as string;
            if (text == null)
                throw new InvalidOperationException(Strings.TextToDelimiteredTextConverter_Error_Message);

            return text.Split('\n').FirstOrDefault();
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
