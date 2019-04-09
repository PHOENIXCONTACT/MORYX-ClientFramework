using System;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace C4I
{
    /// <summary>
    /// Provides a localizations functionality on the fly
    /// </summary>
    public class LocalizeExtension : MarkupExtension
    {
        private Binding _localizationBinding;

        /// <summary>
        /// Constructor
        /// </summary>
        public LocalizeExtension()
        {
        }

        /// <summary>
        /// Extended constructor
        /// </summary>
        public LocalizeExtension(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Name of the key of the string that needs to be localized
        /// </summary>
        [ConstructorArgument("key")]
        public string Key { get; set; }

        /// <summary>
        /// ResourceManager to be used
        /// </summary>
        public Type ResourceType { get; set; }

        /// <summary>
        /// <see>
        /// <cref>Binding.StringFormat</cref>
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

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ResourceType == null)
                throw new InvalidOperationException("ResourceType cannot be empty");

            _localizationBinding = new Binding
            {
                Path = new PropertyPath(nameof(LocalizationData.Value)),
                Source = new LocalizationData(ResourceType, Key),
                StringFormat = StringFormat,
                Converter = Converter,
                ConverterParameter = ConverterParameter
            };

            return _localizationBinding.ProvideValue(serviceProvider);
        }
    }

    internal class LocalizationData : PropertyChangedBase
    {
        private readonly Type _resourceType;
        private readonly string _key;

        public LocalizationData(Type resourceType, string key)
        {
            _resourceType = resourceType;
            _key = key;

            CultureInfoHandler.Instance.CultureChanged += OnCultureChanged;
        }

        public string Value => GetLocalizableValue();

        public string GetLocalizableValue()
        {
            // Get the property from the resource type for this resource key
            var property = _resourceType.GetRuntimeProperty(_key);

            // We need to detect bad configurations so that we can throw exceptions accordingly
            var badlyConfigured = false;

            // Make sure we found the property and it's the correct type, and that the type itself is public
            if (!_resourceType.IsVisible || property == null ||
                property.PropertyType != typeof(string))
            {
                badlyConfigured = true;
            }
            else
            {
                // Ensure the getter for the property is available as public static
                // TODO - check that GetMethod returns the same as old GetGetMethod()
                // in all situations regardless of modifiers
                var getter = property.GetMethod;
                if (getter == null || !(getter.IsPublic && getter.IsStatic))
                {
                    badlyConfigured = true;
                }
            }

            // If the property is not configured properly, then throw a missing member exception
            if (badlyConfigured)
            {
                throw new InvalidOperationException($"No member {_key} found in {_resourceType.FullName}");
            }

            // We have a valid property, so return the resource
            return (string)property.GetValue(null, null);
        }

        private void OnCultureChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Value));
        }
    }
}