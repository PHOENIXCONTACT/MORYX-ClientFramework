// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Reflection;
using Moryx.ClientFramework.Kernel.Properties;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Adapts the run mode for the <see cref="ILoaderHandler"/>
    /// Will provide the assembly loading state and progress
    /// </summary>
    [KernelComponent(typeof(ILoaderAdapter))]
    public class RunModeLoaderAdapter : LoaderAdapterBase, ILoaderAdapter
    {
        ///
        public bool CanAdapt(object component)
        {
            return component is IRunMode;
        }

        ///
        public void Adapt(object component)
        {
            var runMode = (IRunMode) component;
            runMode.AssemblyConfigurationLoaded += OnAssemblyConfigurationLoaded;
            runMode.AssemblyLoaded += OnAssemblyLoaded;
            runMode.AssembliesLoaded += OnAssembliesLoaded;
            runMode.LoadModulesConfigurationCompleted += OnModulesConfigurationLoaded;
            runMode.ExceptionOccurred += OnExceptionOccurred;
        }

        /// <summary>
        /// Called when assembly configuration loaded
        /// </summary>
        private void OnAssemblyConfigurationLoaded(object sender, AssemblyConfiguration e)
        {
            RaiseAddToMax(e.Assemblies.Count);
            RaiseChangeMessage(string.Format(Strings.RunModeLoaderAdapter_AssemblyConfigurationLoaded, e.Assemblies.Count));
        }

        /// <summary>
        /// Called when a exception occurred
        /// </summary>
        private void OnExceptionOccurred(object sender, ClientException e)
        {
            RaiseIndicateError(e);
        }

        /// <summary>
        /// Called when the modules configuration loaded
        /// </summary>
        private void OnModulesConfigurationLoaded(object sender, ModulesConfiguration e)
        {

        }

        /// <summary>
        /// Called when assemblies are loaded
        /// </summary>
        private void OnAssembliesLoaded(object sender, IEnumerable<Assembly> e)
        {

        }

        /// <summary>
        /// Called when a single assembly was loaded
        /// </summary>
        private void OnAssemblyLoaded(object sender, AssemblyConfig e)
        {
            RaiseChangeValueWithMessage(string.Format(Strings.RunModeLoaderAdapter_AssemblyLoaded, e.Assembly));
        }
    }
}
