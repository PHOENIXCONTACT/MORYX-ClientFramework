// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Moryx.ClientFramework.Shell;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Base class for local run modes. 
    /// Will load <see cref="IClientModule"/> and <see cref="IModuleShell"/> from the app domain
    /// </summary>
    public abstract class LocalRunModeBase : RunModeBase
    {
        #region Dependencies

        /// <summary>
        /// Config manager to load kernel configurations
        /// </summary>
        public IKernelConfigManager ConfigManager { get; set; }

        #endregion

        #region Fields and Properties

        /// <summary>
        /// Currently loaded assemblies
        /// </summary>
        protected List<Assembly> Assemblies { get; } = new List<Assembly>();

        /// <summary>
        /// Filter for the <see cref="IClientModule"/> and <see cref="IModuleShell"/>
        /// </summary>
        protected abstract Predicate<Type> TypeLoadFilter { get; }

        #endregion

        ///
        public override void Initialize()
        {
            base.Initialize();

            LoadUserInfos();

            // Load modules
            GlobalContainer.LoadComponents<IClientModule>(TypeLoadFilter);
            GlobalContainer.LoadComponents<IModuleShell>(TypeLoadFilter);

            // Get types
            var moduleTypes = GlobalContainer.GetRegisteredImplementations(typeof(IClientModule));
            var shellTypes = GlobalContainer.GetRegisteredImplementations(typeof(IModuleShell)).ToArray();

            // Get assemblies
            Assemblies.AddRange(moduleTypes.Union(shellTypes).Distinct().Select(t => t.Assembly).Distinct().ToList());

            // Raise load event for AssemblyConfiguration
            RaiseAssemblyConfigurationLoaded(new AssemblyConfiguration
            {
                Assemblies = Assemblies.Select(m => new AssemblyConfig(Path.GetFileName(m.Location))).ToList()
            });

            // Raise AssemblyLoaded event for earch assembly
            foreach (var module in Assemblies)
            {
                RaiseAssemblyLoaded(new AssemblyConfig(Path.GetFileName(module.Location)));
            }

            RaiseAssembliesLoaded(Assemblies);
        }

        ///
        public override void LoadModulesConfiguration()
        {
            
        }
    }
}
