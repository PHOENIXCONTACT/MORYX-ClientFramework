using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;
using Marvin.Container;
using Marvin.Logging;
using Marvin.Modules;
using Marvin.Threading;
using Marvin.Tools.Wcf;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Base class for ClientModules, will host a container and registers some references
    /// The base is also providing several functionality as logging and lifecycle management
    /// </summary>
    /// <typeparam name="TConf">The type of the conf.</typeparam>
    public abstract class ClientModuleBase<TConf> : IClientModule, ILoggingHost 
        where TConf : class, IClientModuleConfig, new()
    {
        #region Dependency Injection

        /// <summary>
        /// Factory to create the local client module container
        /// </summary>
        public IModuleContainerFactory ContainerFactory { get; set; }

        /// <summary>
        /// The config provider is providing module based configurations
        /// </summary>
        public IConfigProvider ConfigProvider { get; set; }

        /// <summary>
        /// Kernel logger for this module
        /// </summary>
        public IModuleLogger Logger { get; set; }

        /// <summary>
        /// Logger management to create module based logger
        /// </summary>
        public ILoggerManagement LoggerManagement { get; set; }

        /// <summary>
        /// ClientFactoy for consuming wcf services
        /// </summary>
        public IWcfClientFactory ClientFactoy { get; set; }

        /// <summary>
        /// Gets or sets the user information provider to recieve user based information
        /// E.g.: Groups, name, full name
        /// </summary>
        public IUserInfoProvider UserInfoProvider { get; set; }

        /// <summary>
        /// Specialized configuration for the module
        /// </summary>
        public TConf Config { get; set; }

        IClientModuleConfig IClientModule.Config => Config;

        #endregion

        #region Fields 

        /// <summary>
        /// Get the name of this module
        /// </summary>
        protected string ModuleName { get; }
        private readonly ClientNotificationCollection _notifications = new ClientNotificationCollection();

        #endregion

        #region Properties

        /// <summary>
        /// Local module based container
        /// </summary>
        protected IContainer Container { get; private set; }

        /// <summary>
        /// Notifications of this module
        /// </summary>
        public INotificationCollection Notifications => _notifications;

        string IModule.Name => ModuleName;

        string ILoggingHost.Name => ModuleName;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientModuleBase{TConf}"/> class.
        /// </summary>
        protected ClientModuleBase()
        {
            var attr = GetType().GetCustomAttribute<RegistrationAttribute>();
            ModuleName = attr == null ? "" : attr.Name;
        }

        /// <summary>
        /// Called when initializing the module.
        /// </summary>
        protected abstract void OnInitialize();

        /// <summary>
        /// Called when activating the module.
        /// </summary>
        protected abstract void OnActivate();

        /// <summary>
        /// Called when deactivating the module.
        /// </summary>
        protected abstract void OnDeactivate(bool close);

        /// <inheritdoc />
        public virtual void Initialize()
        {
            Container = ContainerFactory.Create(new Dictionary<Type, string>(), GetType().Assembly)
                .Register<IParallelOperations, ParallelOperations>();

            Config = ConfigProvider.GetModuleConfiguration<TConf>(ModuleName);

            //add several components to internal container
            Container.SetInstance(ConfigProvider)
                .SetInstance(ClientFactoy)
                .SetInstance(Config)
                .SetInstance(UserInfoProvider);

            AdditionalInitialize();

            LoggerManagement.ActivateLogging(this);
            Logger.Log(LogLevel.Info, "{0} is initializing...", ModuleName);
            Container.SetInstance(Logger);

            OnInitialize();

            // Execute SubInitializer
            var subInits = Container.ResolveAll<ISubInitializer>() ?? new ISubInitializer[0];
            foreach (var subInitializer in subInits)
            {
                subInitializer.Initialize(Container);
            }
        }

        internal abstract void AdditionalInitialize();

        /// <summary>
        /// Activates the client module
        /// </summary>
        public void Activate()
        {
            Logger.Log(LogLevel.Info, "{0} is activating...", ModuleName);

            try
            {
                OnActivate();
            }
            catch (Exception ex)
            {
                Logger.LogException(LogLevel.Error, ex, "Error while activating module {0}!", ModuleName);
            }

            IsActive = true;
        }

        /// <summary>
        /// Gets a value indicating whether this module is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Deactivates the client module
        /// </summary>
        /// <param name="close"></param>
        public void Deactivate(bool close)
        {
            Logger.Log(LogLevel.Info, "{0} is deactivating...", ModuleName);
            RaiseAttemptingDeactivation(new DeactivationEventArgs());

            try
            {
                OnDeactivate(close);
                
            }
            catch (Exception ex)
            {
                Logger.LogException(LogLevel.Error, ex, "Error while deactivating module {0}!", ModuleName);
            }

            IsActive = false;
            RaiseDeactivated(new DeactivationEventArgs());
        }

        /// <summary>
        /// Occurs when the module is attemping to deactivate
        /// </summary>
        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation;

        private void RaiseAttemptingDeactivation(DeactivationEventArgs e)
        {
            AttemptingDeactivation?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when the module will be deactivated.
        /// </summary>
        public event EventHandler<DeactivationEventArgs> Deactivated;

        private void RaiseDeactivated(DeactivationEventArgs e)
        {
            Deactivated?.Invoke(this, e);
        }

        void IDisposable.Dispose()
        {
            ConfigProvider.SaveConfiguration(Config);

            if (Container == null) 
                return;

            Container.Destroy();
            Container = null;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{ModuleName}";
        }

    }
}