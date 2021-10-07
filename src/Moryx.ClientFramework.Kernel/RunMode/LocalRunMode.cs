// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Reflection;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Local run mode, will load all needed assemblies from the app domain
    /// This run mode will ignore types with the <see cref="ComponentForRunModeAttribute"/>
    /// </summary>
    [KernelComponent(typeof(IRunMode), Name = ComponentName)]
    public class LocalRunMode : LocalRunModeBase
    {
        internal const string ComponentName = "Local_RunMode";

        /// <inheritdoc />
        protected override string Name => ComponentName;

        /// <inheritdoc />
        protected override Predicate<Type> TypeLoadFilter
        {
            get { return type => type.GetCustomAttribute<ComponentForRunModeAttribute>() == null; }
        }

        /// <inheritdoc />
        public override void LoadModulesConfiguration()
        {
            var modulesConfig = ConfigManager.GetConfiguration<ModulesConfiguration>();

            foreach (var assembly in Assemblies)
            {
                modulesConfig.Modules.AddRange(GetModuleConfigsFromAssembly(assembly, modulesConfig));
            }

            SelectShell(Assemblies, modulesConfig);

            ConfigProvider = new LocalConfigProvider(ConfigManager, modulesConfig);
            ConfigProvider.SaveConfiguration(modulesConfig);

            RaiseModulesConfigurationLoaded(modulesConfig);
        }
    }
}
