using System;
using System.Collections.Generic;
using System.Reflection;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Interface for the client frameworks runmode.
    /// The runmodes will handled by the <see cref="IRunModeController"/>
    /// </summary>
    public interface IRunMode
    {
        /// <summary>
        /// Gets the configuration provider.
        /// </summary>
        IConfigProvider ConfigProvider { get; }

        /// <summary>
        /// Initializes the runmode and starts the run operation
        /// </summary>
        void Initialize();

        /// <summary>
        /// Occurs when all assemblies are loaded.
        /// </summary>
        event EventHandler<IEnumerable<Assembly>> AssembliesLoaded;

        /// <summary>
        /// Occurs when [assembly configuration loaded].
        /// </summary>
        event EventHandler<AssemblyConfiguration> AssemblyConfigurationLoaded;

        /// <summary>
        /// Occurs when [assembly loaded].
        /// </summary>
        event EventHandler<AssemblyConfig> AssemblyLoaded;

        /// <summary>
        /// Occurs when an exeption occured.
        /// </summary>
        event EventHandler<ClientException> ExeptionOccured;

        /// <summary>
        /// Gets the modules configuration.
        /// </summary>
        void LoadModulesConfiguration();

        /// <summary>
        /// Occurs when [modules configuration loaded].
        /// </summary>
        event EventHandler<ModulesConfiguration> LoadModulesConfigurationCompleted;
    }
}