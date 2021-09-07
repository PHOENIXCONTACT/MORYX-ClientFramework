// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Moryx.Container;

namespace Moryx.ClientFramework.Shell
{
    /// <summary>
    /// Shell region controller base which handles the container structure for the
    /// Shell regions and configuration.
    /// Will host a container with multiple regions in it
    /// </summary>
    public abstract class ShellRegionController : IShellRegionController
    {
        #region Fields and properties

        /// <summary>
        /// Region configuration which holds the configured regions and their plugins
        /// </summary>
        protected ShellRegionConfig RegionConfig { get; private set; }

        /// <summary>
        /// Container of the region container
        /// </summary>
        protected IContainer ShellContainer { get; set; }

        #endregion

        /// <inheritdoc />
        public virtual void Initialize(IModuleContainerFactory containerFactory, IModuleManager moduleManager, IConfigProvider configProvider)
        {
            RegionConfig = configProvider.GetConfiguration<ShellRegionConfig>();

            // Initialize config if necessary
            if (!RegionConfig.Initialized)
            {
                BuildConfig(RegionConfig);
                RegionConfig.Initialized = true;
                configProvider.SaveConfiguration(RegionConfig);
            }

            // Load components from local assembly, inherited assembly and regions from directory
            ShellContainer = containerFactory.Create(new Dictionary<Type, string>(), GetType().Assembly);

            // Register region plugins in local container
            foreach (var module in moduleManager.EnabledModules.OfType<IRegionModule>())
            {
                ShellContainer.SetInstance(module, module.Name);
            }

            LoadPlugins(ShellContainer);
        }

        /// <inheritdoc />
        public void Register<T>(T component)  where T : class
        {
            ShellContainer.SetInstance(component);
        }

        /// <inheritdoc />
        public Region FetchRegion(string regionName = null)
        {
            var config = RegionConfig.Regions.First(r => r.RegionName == regionName);
            var region = ShellContainer.Resolve<IShellRegion>(config.PluginName);
            return new Region(config.Visibility, region);
        }

        /// <summary>
        /// Will build up a the shell region config by defaults
        /// </summary>
        protected abstract void BuildConfig(ShellRegionConfig config);

        /// <summary>
        /// Additional loading of shell regions from the container
        /// </summary>
        protected abstract void LoadPlugins(IContainer container);
    }
}
