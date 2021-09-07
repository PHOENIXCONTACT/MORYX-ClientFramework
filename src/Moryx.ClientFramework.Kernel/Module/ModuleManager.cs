// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moryx.Container;
using Moryx.Logging;

namespace Moryx.ClientFramework.Kernel
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

        /// <inheritdoc />
        public IEnumerable<IClientModule> EnabledModules => _initializedAndEnabled;

        #endregion

        /// <inheritdoc />
        public async Task InitializeAsync()
        {
            Logger.Log(LogLevel.Debug, "Start initializing of {0} modules", ClientModules.Count());

            RaiseStartInitializingModules(ClientModules.Count());

            foreach (var clientModule in ClientModules)
            {
                RaiseStartInitializeModule(clientModule);

                await InitializeModule(clientModule);

                RaiseInitializingModule(clientModule);
            }
        }

        /// <inheritdoc />
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
        private async Task InitializeModule(IClientModule module)
        {
            try
            {
                await module.InitializeAsync();

                if(module.Config.IsEnabled)
                  _initializedAndEnabled.Add(module);
            }
            catch (Exception ex)
            {
                Logger.LogException(LogLevel.Error, ex, "Error while initializing module '{0}'", module.Name);
            }
        }

        #region Raise Events

        /// <inheritdoc />
        public event EventHandler<IClientModule> StartInitializeModule;

        /// <inheritdoc />
        public event EventHandler<int> StartInitializingModules;

        /// <inheritdoc />
        public event EventHandler<IClientModule> InitializingModuleDone;

        /// <summary>
        /// Raises the StartInitializeModule event.
        /// </summary>
        protected virtual void RaiseStartInitializeModule(IClientModule e)
        {
            StartInitializeModule?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the StartInitializingModules event.
        /// </summary>
        protected virtual void RaiseStartInitializingModules(int e)
        {
            StartInitializingModules?.Invoke(this, e);
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
