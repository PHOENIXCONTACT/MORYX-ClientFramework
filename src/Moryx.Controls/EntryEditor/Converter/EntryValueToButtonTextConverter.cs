// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Globalization;
using System.Windows.Data;
using Moryx.Controls.Properties;
using Moryx.Serialization;

namespace Moryx.Controls.Converter
{
    /// <inheritdoc />
    /// <summary>
    /// Converts entry type to text
    /// </summary>
    public class EntryValueToButtonTextConverter : IValueConverter
    {
        /// <inheritdoc />
        /// <summary>
        /// Conversion method
        /// </summary>
        /// <param name="value"><see cref="EntryViewModel"/></param>
        /// <param name="targetType">Not used</param>
        /// <param name="parameter">Not used</param>
        /// <param name="culture">Not used</param>
        /// <returns>Returns "Add entry" if the type is <see cref="EntryValueType.Collection"/> or "Replace with" if type is <see cref="EntryValueType.Class"/>. Otherwise an empty string is returned.</returns>
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

        /// <inheritdoc />
        /// <summary>
        /// Not supported
        /// </summary>
        /// <returns>null</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
