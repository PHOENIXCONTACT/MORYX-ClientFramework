using System;
using System.Reflection;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Config run mode, will load all needed assemblies for the configurator from the app domain
    /// This run mode will only load types with the <see cref="ComponentForRunModeAttribute"/> and RunMode <see cref="KernelConstants.CONFIG_RUNMODE"/>
    /// </summary>
    [KernelComponent(typeof(IRunMode), Name = KernelConstants.CONFIG_RUNMODE)]
    public class ConfigRunMode : LocalRunModeBase
    {
        ///
        protected override Predicate<Type> TypeLoadFilter
        {
            get
            {
                return delegate(Type type)
                {
                    var runModeAttr = type.GetCustomAttribute<ComponentForRunModeAttribute>();
                    return runModeAttr != null && runModeAttr.RunMode.Equals(KernelConstants.CONFIG_RUNMODE);
                };
            }
        }

        ///
        public override void LoadModulesConfiguration()
        {
            var modulesConfig = new ModulesConfiguration();
            SelectShell(Assemblies, modulesConfig);

            foreach (var assembly in Assemblies)
                modulesConfig.Modules.AddRange(GetModuleConfigsFromAssembly(assembly, modulesConfig));

            ConfigProvider = new LocalConfigProvider(ConfigManager, modulesConfig);

            RaiseModulesConfigurationLoaded(modulesConfig);
        }
    }
}