using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
{
    [KernelComponent(typeof(ILoaderAdapter))]
    public class ModuleManagerLoaderAdapter : LoaderAdapterBase, ILoaderAdapter
    {
        public bool CanAdapt(object component)
        {
            return component is IModuleManager;
        }

        public void Adapt(object component)
        {
            var moduleManager = (IModuleManager) component;

            moduleManager.StartInitilizingModules += OnStartInitilizingModules;
            moduleManager.StartInitializeModule += OnStartInitializeModule;
            moduleManager.InitializingModuleDone += OnInitializingModuleDone;
        }

        private void OnStartInitilizingModules(object sender, int i)
        {
            RaiseAddToMax(i);
        }

        private void OnStartInitializeModule(object sender, IClientModule clientModule)
        {
            RaiseChangeMessage($"Initializing {clientModule.Name}");
        }

        private void OnInitializingModuleDone(object sender, IClientModule clientModule)
        {
            RaiseChangeValueWithMessage($"Initializing {clientModule.Name} done ...");
        }
    }
}