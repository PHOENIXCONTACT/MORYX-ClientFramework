using System;
using System.Collections.Generic;
using Marvin.Modules;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// The initializer initialize all components which are given through <see cref="Initializables"/>
    /// </summary>
    public interface IComponentInitializer
    {
        /// <summary>
        /// Gets or sets the initializables.
        /// </summary>
        IEnumerable<IInitializable> Initializables { get; set; }

        /// <summary>
        /// Starts the initialization of the components
        /// The implementation should be async
        /// </summary>
        void Initialize();

        /// <summary>
        /// Occurs when the initialization of a component will be started
        /// </summary>
        event EventHandler<IInitializable> InitializingComponent;

        /// <summary>
        /// Occurs when starting to initilaize the whole components
        /// </summary>
        event EventHandler<int> Starting;

        /// <summary>
        /// Occurs when completed to initialize all components
        /// </summary>
        event EventHandler Completed;
    }
}