// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Caliburn.Micro;
using Marvin.Configuration;

namespace Marvin.ClientFramework.Configurator
{
    /// <summary>
    /// Interface for the configured hosted configuration views
    /// </summary>
    public interface IConfigViewModel : IScreen
    {
        /// <summary>
        /// Gets the image source.
        /// </summary>
        string ImageSource { get; }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        void SaveConfig();
    }

    /// <summary>
    /// Base class for the hosted configuration views
    /// Will load the typed config from the <see cref="IKernelConfigManager"/> from the framework
    /// When the config will be saved, the view model will save the config via the config manager if and only
    /// if the view model was initialized
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ConfigViewModelBase<T> : Screen, IConfigViewModel where T : class, IConfig, new()
    {
        #region Dependency Injection

        /// <summary>
        /// Gets or sets the core configuration manager of the framework.
        /// </summary>
        public IKernelConfigManager KernelConfigManager { get; set; }

        #endregion

        #region Fields and Properties

        private T _config;

        #endregion

        ///
        public abstract override string DisplayName { get; }

        ///
        public abstract string ImageSource { get; }

        /// <summary>
        /// Called when initializing.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Config = KernelConfigManager.GetConfiguration<T>();
        }

        ///
        public virtual void SaveConfig()
        {
            if (!IsInitialized)
                return;

            KernelConfigManager.SaveConfiguration(Config);
        }

        ///
        public T Config
        {
            get { return _config; }
            set
            {
                _config = value;
                NotifyOfPropertyChange(() => Config);
            }
        }
    }
}
