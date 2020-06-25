// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using C4I;
using Caliburn.Micro;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <inheritdoc />
    [KernelComponent(typeof(IViewLocatorConfigurator))]
    public class ViewLocatorConfigurator : IViewLocatorConfigurator
    {
        #region Dependencies

        /// <summary>
        /// Available Presets
        /// </summary>
        public IEnumerable<IViewLocatorConfiguratorPreset> Presets { get; set; }

        #endregion

        /// <inheritdoc />
        public string ActivatedSet { get; private set; }

        /// <summary>
        /// Activates a set. All <see cref="ContentControl"/>s which under control of <see cref="Caliburn.Micro.View"/> are forced to update their views.
        /// </summary>
        /// <param name="setName"></param>
        /// <exception cref="T:System.NotSupportedException">The exception is thrown if the commited preset was not found.</exception>
        public void ActivateSet(string setName)
        {
            var presetToActivate = Presets.FirstOrDefault(p => p.Name == setName);
            
            if (presetToActivate != null)
            {
                ActivateSet(presetToActivate);

                ActivatedSet = setName;
            }
            else
            {
                throw new NotSupportedException($"Preset {setName} not found.");
            }
        }

        private static void ActivateSet(IViewLocatorConfiguratorPreset set)
        {
            // Use the default views of caliburn (currently 'view' and 'page')
            var typeMappingConfiguration = new TypeMappingConfiguration
            {
                IncludeViewSuffixInViewModelNames = false
            };

            foreach (var viewSuffix in set.ViewSuffixes)
            {
                typeMappingConfiguration.ViewSuffixList.Add("View_" + viewSuffix);
            }

            ViewLocator.ConfigureTypeMappings(typeMappingConfiguration);

            ApplyModelChanges();
        }

        private static void ApplyModelChanges()
        {
            if (Application.Current?.MainWindow == null)
                return;

            foreach (var contentControl in Application.Current.MainWindow.Descendants<ContentControl>())
            {
                var model = View.GetModel(contentControl);
                if (model == null)
                    continue;

                if (model is ViewAware)
                {
                    // A ViewAware instance caches the views so the mechanism would not work because the previous view instance will be used.
                    // So we have to clear the cache here.
                    var viewsProperty = model.GetType().GetProperty("Views", BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic);
                    if (viewsProperty != null)
                    {
                        var viewsValue = viewsProperty.GetValue(model) as IDictionary<object, object>;
                        viewsValue?.Clear();
                    }
                }

                View.SetModel(contentControl, null);
                View.SetModel(contentControl, model);
            }
        }
    }
}
