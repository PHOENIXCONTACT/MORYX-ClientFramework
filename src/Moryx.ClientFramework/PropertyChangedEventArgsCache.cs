// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.ComponentModel;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Provides a cache for <see cref="PropertyChangedEventArgs"/> instances.
    /// </summary>
    internal sealed class PropertyChangedEventArgsCache
    {
        /// <summary>
        /// The underlying dictionary. This instance is its own mutex.
        /// </summary>
        private readonly Dictionary<string, PropertyChangedEventArgs> _cache = new Dictionary<string, PropertyChangedEventArgs>();

        /// <summary>
        /// The global instance of the cache.
        /// </summary>
        private static PropertyChangedEventArgsCache _instance;

        /// <summary>
        /// Private constructor to prevent other instances.
        /// </summary>
        private PropertyChangedEventArgsCache()
        {

        }

        /// <summary>
        /// The global instance of the cache.
        /// </summary>
        public static PropertyChangedEventArgsCache Instance => _instance ??= new PropertyChangedEventArgsCache();

        /// <summary>
        /// Retrieves a <see cref="PropertyChangedEventArgs"/> instance for the specified property,
        /// creating it and adding it to the cache if necessary.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        public PropertyChangedEventArgs Get(string propertyName)
        {
            lock (_cache)
            {
                PropertyChangedEventArgs result;
                if (_cache.TryGetValue(propertyName, out result))
                    return result;

                result = new PropertyChangedEventArgs(propertyName);
                _cache.Add(propertyName, result);

                return result;
            }
        }
    }
}
