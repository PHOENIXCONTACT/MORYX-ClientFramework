// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Controls;
using Marvin.Serialization;

namespace Marvin.Controls.Converter
{
    /// <summary>
    /// Selects a template depending on the <see cref="EntryValueType"/>
    /// </summary>
    public class ValueTypeTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Template for <see cref="EntryValueType.Enum"/>
        /// </summary>
        public DataTemplate DropDownTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.Boolean"/>
        /// </summary>
        public DataTemplate BoolTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.String"/>
        /// </summary>
        public DataTemplate StringTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryUnitType.Password"/>
        /// </summary>
        public DataTemplate PasswordTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.Class"/>
        /// </summary>
        public DataTemplate ClassTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.Collection"/>
        /// </summary>
        public DataTemplate CollectionTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.String"/> and <see cref="EntryUnitType.File"/>
        /// </summary>
        public DataTemplate FilePickerTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.String"/> and <see cref="EntryUnitType.Directory"/>
        /// </summary>
        public DataTemplate DirectoryPickerTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.Stream"/>
        /// </summary>
        public DataTemplate StreamTemplate { get; set; }

        /// <summary>
        /// Template for <see cref="EntryValueType.Exception"/>
        /// </summary>
        public DataTemplate ExceptionTemplate { get; set; }

        /// <inheritdoc />
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dataTemplate = StringTemplate;

            var configEntryModel = item as EntryViewModel;
            if (configEntryModel == null)
                return dataTemplate;

            switch (configEntryModel.ValueType)
            {
                case EntryValueType.Boolean:
                    dataTemplate = BoolTemplate;
                    break;
                case EntryValueType.Enum:
                    dataTemplate = DropDownTemplate;
                    break;
                case EntryValueType.Class:
                    dataTemplate = ClassTemplate;
                    break;
                case EntryValueType.Collection:
                    dataTemplate = CollectionTemplate;
                    break;
                case EntryValueType.String:
                    switch (configEntryModel.UnitType)
                    {
                        case EntryUnitType.Password:
                            dataTemplate = PasswordTemplate;
                            break;
                        case EntryUnitType.File:
                            dataTemplate = FilePickerTemplate;
                            break;
                        case EntryUnitType.Directory:
                            dataTemplate = DirectoryPickerTemplate;
                            break;
                        default:
                            dataTemplate = configEntryModel.PossibleValues == null ? StringTemplate : DropDownTemplate;
                            break;
                    }
                    break;
                case EntryValueType.Stream:
                    dataTemplate = StreamTemplate;
                    break;
                case EntryValueType.Exception:
                    dataTemplate = ExceptionTemplate;
                    break;
                default:
                    dataTemplate = configEntryModel.PossibleValues == null ? StringTemplate : DropDownTemplate;
                    break;
            }

            return dataTemplate;
        }
    }
}
