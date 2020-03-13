// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel;
using System.Windows;
using Caliburn.Micro;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Class wrapper for a region
    /// </summary>
    public class Region : PropertyChangedBase
    {
        /// <summary>
        /// Create region from plugin
        /// </summary>
        public Region(Visibility visibility, INotifyPropertyChanged content)
        {
            Visibility = visibility;
            Content = content;
        }

        /// <summary>
        /// Visibility of this region
        /// </summary>
        public Visibility Visibility { get; private set; }

        /// <summary>
        /// Plugin to display in this region
        /// </summary>
        public INotifyPropertyChanged Content { get; private set; }
    }
}
