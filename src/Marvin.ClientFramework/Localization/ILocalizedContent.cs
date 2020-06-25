using System;
using System.Collections.Generic;
using System.Globalization;

namespace Marvin.ClientFramework.Localization
{
    /// <summary>
    /// Interface for the generated localization components
    /// </summary>
    public interface ILocalizedContent
    {
        /// <summary>
        /// The target plugin dll name which should be localized
        /// </summary>
        string TargetPlugin { get; }

        /// <summary>
        /// Gets the dictionary of language keys and their values.
        /// </summary>
        Dictionary<string, string> Dictionary { get; set; }

        /// <summary>
        /// Fills the properties of the localization class
        /// </summary>
        /// <param name="rawValues">The raw values. 1. key 2. value</param>
        void FillProperties(Dictionary<string, string> rawValues);

        /// <summary>
        /// Sets the localization provider to self-update the language
        /// </summary>
        /// <param name="provider">The provider.</param>
        void SetProvider(ILocalizationProvider provider);

        /// <summary>
        /// Updates the language.
        /// </summary>
        /// <param name="ietfTag">The ietf tag.</param>
        void UpdateLanguage(string ietfTag);

        /// <summary>
        /// Recreates the instance and resets all properties
        /// </summary>
        void ClearLanguage();

        /// <summary>
        /// Gets the translation by key.
        /// </summary>
        string GetTranslationByKey(string key);

        /// <summary>
        /// Occurs when [language changed].
        /// </summary>
        event EventHandler LanguageChanged;

        /// <summary>
        /// Gets the current language.
        /// </summary>
        CultureInfo CurrentLanguage { get; }
    }
}
