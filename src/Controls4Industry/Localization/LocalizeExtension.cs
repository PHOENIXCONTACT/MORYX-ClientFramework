using System;
using System.Globalization;
using System.Resources;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace C4I
{
    /// <summary>
    /// Provides a localizations functionalities on the fly
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
        /// <param name="localizationKey"></param>
        public LocalizeExtension(string localizationKey)
        {
            LocalizationKey = localizationKey;
        }

        /// <summary>
        /// Name of the key of the string that needs to be localized
        /// </summary>
        [ConstructorArgument("localizationKey")]
        public string LocalizationKey { get; set; }

        /// <summary>
        /// ResourceManager to be used
        /// </summary>
        public ResourceManager LocalizationSource { get; set; }

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

        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (LocalizationSource != null)
            {
                _localizationBinding = new Binding
                {
                    Path = new PropertyPath(nameof(LocalizationData.Value)),
                    Source = new LocalizationData(LocalizationSource, LocalizationKey),
                    StringFormat = StringFormat,
                    Converter = Converter,
                    ConverterParameter = ConverterParameter
                };

                return _localizationBinding.ProvideValue(serviceProvider);
            }
                
            throw new InvalidOperationException("Source cannot be empty");
        }
    }

    internal class LocalizationData : PropertyChangedBase
    {
        private readonly ResourceManager _resourceManager;
        private readonly string _key;

        public LocalizationData(ResourceManager resourceManager, string key)
        {
            _resourceManager = resourceManager;
            _key = key;

            CultureInfoHandler.Instance.CultureChanged += OnCultureChanged;
        }

        public string Value => _resourceManager.GetString(_key);

        private void OnCultureChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Value));
        }
    }
}