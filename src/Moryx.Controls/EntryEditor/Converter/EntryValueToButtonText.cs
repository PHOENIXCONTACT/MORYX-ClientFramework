// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows.Data;
using Moryx.Controls.Properties;
using Moryx.Serialization;

namespace Moryx.Controls.Converter
{
    public class EntryValueToButtonText : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var entry = value as EntryViewModel;
            if (entry == null)
                return "";

            if (entry.ValueType == EntryValueType.Collection)
                return Strings.EntryValueToButtonTextConverter_AddEntry;

            if (entry.ValueType == EntryValueType.Class)
                return Strings.EntryValueToButtonTextConverter_ReplaceWith;

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
