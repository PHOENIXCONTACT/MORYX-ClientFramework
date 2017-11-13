using System.Windows;
using System.Windows.Controls;
using Marvin.Serialization;

namespace Marvin.Controls.Converter
{
    public class ValueTypeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DropDownTemplate { get; set; }

        public DataTemplate BoolTemplate { get; set; }

        public DataTemplate StringTemplate { get; set; }

        public DataTemplate ClassTemplate { get; set; }

        public DataTemplate CollectionTemplate { get; set; }

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
                default:
                    dataTemplate = configEntryModel.PossibleValues == null ? StringTemplate : DropDownTemplate;
                    break;
            }

            return dataTemplate;
        }
    }
}