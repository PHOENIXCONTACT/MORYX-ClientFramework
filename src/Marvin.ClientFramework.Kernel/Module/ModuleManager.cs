using System;
using System.Collections.Generic;
using System.Linq;
using Marvin.Container;
using Marvin.Logging;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Main manager for all client modules
    /// </summary>
    [KernelComponent(typeof(IModuleManager))]
    public class ModuleManager : IModuleManager, ILoggingComponent
    {
        #region Dependencies

        /// <summary>
        /// Inject all client modules from the global container
        /// </summary>
        public IEnumerable<IClientModule> ClientModules { get; set; }

        /// <summary>
        /// Logger for the outside world
        /// </summary>
        [UseChild("ModuleManager")]
        public IModuleLogger Logger { get; set; }

        #endregion

        #region Fields and Properties

        private readonly List<IClientModule> _initializedAndEnabled = new List<IClientModule>();

        ///
        public IEnumerable<IClientModule> EnabledModules => _initializedAndEnabled;

        #endregion

        /// <summary>
        /// Initialize this component and prepare it for incoming taks. This must only involve preparation and must not start
        /// any active functionality and/or periodic execution of logic.
        /// </summary>
        public void Initialize()
        {
            Logger.Log(LogLevel.Debug, "Start initializing of {0} modules", ClientModules.Count());

            RaiseStartInitilizingModules(ClientModules.Count());

            foreach (var clientModule in ClientModules)
            {
                RaiseStartInitializeModule(clientModule);

                InitializeModule(clientModule);

                RaiseInitializingModule(clientModule);
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, 
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            foreach (var clientModule in ClientModules.Where(m => m.Config.IsEnabled))
            {
                clientModule.Dispose();
            }
        }

        /// <summary>
        /// Initializes a single module and raises events.
        /// </summary>
        private void InitializeModule(IClientModule module)
        {
            try
            {
                module.Initialize();

                if(module.Config.IsEnabled)
                  _initializedAndEnabled.Add(module);
            }
            catch (Exception ex)
            {
                Logger.LogException(LogLevel.Error, ex, "Error while intializing module '{0}'", module.Name);
            }
        }

        #region Raise Events

        /// 
        public event EventHandler<IClientModule> StartInitializeModule;

        /// 
        public event EventHandler<int> StartInitilizingModules;

        /// 
        public event EventHandler<IClientModule> InitializingModuleDone;


        /// <summary>
        /// Raises the StartInitializeModule event.
        /// </summary>
        protected virtual void RaiseStartInitializeModule(IClientModule e)
        {
            StartInitializeModule?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the StartInitilizingModules event.
        /// </summary>
        protected virtual void RaiseStartInitilizingModules(int e)
        {
            StartInitilizingModules?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the InitializingModuleDone event.
        /// </summary>
        protected virtual void RaiseInitializingModule(IClientModule e)
        {
            InitializingModuleDone?.Invoke(this, e);
        }

        #endregion
    }
}