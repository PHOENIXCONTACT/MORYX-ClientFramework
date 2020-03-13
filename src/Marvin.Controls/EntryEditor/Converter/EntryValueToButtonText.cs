// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows.Data;
using Marvin.Controls.Properties;
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
                return Localizations.AddEntry;

            if (entry.ValueType == EntryValueType.Class)
                return Localizations.ReplaceWith;

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
