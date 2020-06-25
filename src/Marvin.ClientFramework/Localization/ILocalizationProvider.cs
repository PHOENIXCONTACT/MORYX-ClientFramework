using System;
using System.Collections.Generic;
using Marvin.Modules;

namespace Marvin.ClientFramework.Localization
{
    /// <summary>
    /// Interface for localization providers
    /// The provider should fill a <see cref="ILocalizedContent"/> class with localized content
    /// </summary>
    public interface ILocalizationProvider : IInitializable
    {
        /// <summary>
        /// Gets the localized content asynchronous and sets it to the instance (fill properties).
        /// </summary>
        /// <param name="ietfTag">The ietf tag (e.g. en-US).</param>
        /// <param name="instance">The instance which should be filled.</param>
        /// <param name="localizationLoadedCallback">Callback if the loading of the localization was successfull</param>
        void GetLocalizedContentAsync(string ietfTag, ILocalizedContent instance, Action<bool> localizationLoadedCallback = null);

        /// <summary>
        /// Gets the available languages asynchronous.
        /// </summary>
        void GetAvailableLanguagesAsync(Action<List<string>> languagesRecievedCallback);
    }
}
