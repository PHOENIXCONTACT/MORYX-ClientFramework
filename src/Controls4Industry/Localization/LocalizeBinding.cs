using System;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace C4I
{
    /// <summary>
    /// Binding taht supports localization
    /// </summary>
    public class LocalizeBinding : MarkupExtension
    {
        private DependencyObject _targetObject;
        private DependencyProperty _targetProperty;

        /// <summary>
        /// Instantiates a new instance of <see cref="LocalizeBinding"/>
        /// </summary>
        public LocalizeBinding() : this("")
        {
        }

        /// <summary>
        /// Instantiates a new instance of <see cref="LocalizeBinding"/> with a preset path
        /// </summary>
        public LocalizeBinding(string path)
        {
            Path = new PropertyPath(path);

            CultureInfoHandler.Instance.CultureChanged += OnCultureChanged;
        }

        /// <summary>
        /// ResourceManager to be used
        /// </summary>
        public ResourceManager LocalizationSource { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.StringFormat</cref>
        /// </see>
        /// </summary>
        [ConstructorArgument("path")]
        public PropertyPath Path { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.Source</cref>
        /// </see>
        /// </summary>
        public object Source { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.ElementName</cref>
        /// </see>
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.RelativeSource</cref>
        /// </see>
        /// </summary>
        public RelativeSource RelativeSource { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.StringFormat</cref>
        /// </see>
        /// </summary>
        public string StringFormat { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.Converter</cref>
        /// </see>
        /// </summary>
        public IValueConverter Converter { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.ConverterParameter</cref>
        /// </see>
        /// </summary>
        public object ConverterParameter { get; set; }

        /// <summary>
        /// <see>
        ///     <cref>Binding.ConverterCulture</cref>
        /// </see>
        /// </summary>
        public CultureInfo ConverterCulture { get; set; }

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var valueProvider = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (valueProvider != null)
            {
                _targetObject = valueProvider.TargetObject as DependencyObject;
                _targetProperty = valueProvider.TargetProperty as DependencyProperty;
            }

            var binding = new Binding(Path.Path)
            {
                Converter = Converter,
                ConverterParameter = ConverterParameter,
                ConverterCulture = ConverterCulture,
                StringFormat = StringFormat
            };

            if (ElementName != null)
                binding.ElementName = ElementName;
            if (RelativeSource != null)
                binding.RelativeSource = RelativeSource;
            if (Source != null)
                binding.Source = Source;

            return binding.ProvideValue(serviceProvider);
        }

        private void OnCultureChanged(object sender, EventArgs e)
        {
            if (_targetObject != null && _targetProperty != null)
            {
                var bindingExpression = BindingOperations.GetBindingExpression(_targetObject, _targetProperty);
                bindingExpression?.UpdateTarget();
            }
        }
    }

}
