// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Caliburn.Micro;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Bootstrapper for caliburn.
    /// </summary>
    public class CaliburnBootstrapper : BootstrapperBase
    {
        private readonly IEnumerable<Assembly> _assemblies;
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="CaliburnBootstrapper"/> class.
        /// </summary>
        public CaliburnBootstrapper(IEnumerable<Assembly> assemblies, IContainer container)
        {
            _container = container;
            _assemblies = assemblies.Union(GetLocalAssemblies()).Distinct().ToArray();
        }

        /// <summary>
        /// Will return a list of assemblies which are located in the <see cref="AppDomain.CurrentDomain"/>
        /// </summary>
        private static IEnumerable<Assembly> GetLocalAssemblies()
        {
            var assemblies = new List<Assembly>();
            //TODO: is there a better way to ignore dynamic assemblies?
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try
                {
                    if (assembly is AssemblyBuilder || assembly.Location == null)
                        continue;
                }
                catch (Exception)
                {
                    continue;
                }

                if (assembly.Location.StartsWith(AppDomain.CurrentDomain.BaseDirectory))
                {
                    assemblies.Add(assembly);
                }
            }
            return assemblies;
        }

        /// <summary>
        /// Override to configure the framework and setup your IoC container.
        /// </summary>
        protected override void Configure()
        {           
            // configure the view locator
            var defaultLocator = ViewLocator.LocateTypeForModelType;

            ViewLocator.LocateTypeForModelType = (modelType, displayLocation, context) =>
            {
                var foundView = defaultLocator.Invoke(modelType, displayLocation, context);
                return foundView;
            };
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <param name="key">The key to locate.</param>
        /// <returns> The located service. </returns>
        protected override object GetInstance(Type service, string key)
        {
            return _container.Resolve(service);
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation
        /// </summary>
        /// <param name="service">The service to locate.</param>
        /// <returns> The located services. </returns>
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            var foundInContainer = (IEnumerable<object>) _container.ResolveAll(service);
            return foundInContainer;
        }

        /// <summary>
        /// Override this to provide an IoC specific implementation.
        /// </summary>
        /// <param name="instance">The instance to perform injection on.</param>
        protected override void BuildUp(object instance)
        {
            //TODO: castle container should provide build up method to set dependencies on the given object
        }

        /// <summary>
        /// Override to tell the framework where to find assemblies to inspect for views, etc.
        /// </summary>
        /// <returns> A list of assemblies to inspect. </returns>
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            return _assemblies;
        }
    }
}
