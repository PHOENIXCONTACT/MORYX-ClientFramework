using Marvin.ClientFramework.Kernel.Properties;
using Marvin.Container;
using Marvin.Modules;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Component initializer loader adapter
    /// </summary>
    [KernelComponent(typeof(ILoaderAdapter))]
    public class ComponentInitializerLoaderAdapter : LoaderAdapterBase, ILoaderAdapter
    {
        private IComponentInitializer _componentInitializer;

        /// <inheritdoc />
        public bool CanAdapt(object component)
        {
            return component is IComponentInitializer;
        }

        /// <inheritdoc />
        public void Adapt(object component)
        {
            _componentInitializer = (IComponentInitializer) component;

            _componentInitializer.Starting += OnStarting;
            _componentInitializer.InitializingComponent += OnInitializingComponent;
        }
      

        private void OnInitializingComponent(object sender, IInitializable initializable)
        {
            
            RaiseChangeValueWithMessage(string.Format(Strings.ComponentInitializerLoaderAdapter_InitializingComponent, initializable.GetType().Name));
        }

        private void OnStarting(object sender, int numberOfComponents)
        {
            
            RaiseChangeMessage(string.Format(Strings.ComponentInitializerLoaderAdapter_ComponentInitializingStarting, numberOfComponents));
            RaiseAddToMax(numberOfComponents);
        }
    }
}