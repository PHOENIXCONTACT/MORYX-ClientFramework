// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Moryx.ClientFramework.Shell;
using Moryx.Container;
using Moryx.Users;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Base class for the client frameworks run modes
    /// Will provide configuration, global container structure and user information
    /// </summary>
    public abstract class RunModeBase : IRunMode
    {
        #region Dependencies

        /// <summary>
        /// Config provider to load the modules configuration and more
        /// </summary>
        public IConfigProvider ConfigProvider { get; set; }

        /// <summary>
        /// Global container to register the modules, shell and other components
        /// </summary>
        public IContainer GlobalContainer { get; set; }

        /// <summary>
        /// The global <see cref="IUserInfoProvider"/> will be initialized here
        /// It will be filled with several information like the user, groups and others
        /// </summary>
        public IUserInfoProvider UserInfoProvider { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Name of the RunMode
        /// </summary>
        protected abstract string Name { get; }

        #endregion

        /// <inheritdoc />
        public virtual void Initialize()
        {
            // Register FallbackShell
            GlobalContainer.SetInstance(new FallbackShellViewModel() as IModuleShell, FallbackShellViewModel.ShellName);
        }

        /// <inheritdoc />
        public abstract void LoadModulesConfiguration();

        /// <summary>
        /// Will select the shell with the name in the configuration
        /// </summary>
        protected void SelectShell(IEnumerable<Assembly> assemblies, ModulesConfiguration modulesConfig)
        {
            Type selectedShell;
            var shells = assemblies.SelectMany(ass => ass.GetTypes())
                    .Where(t => typeof (IModuleShell).IsAssignableFrom(t) && t.GetCustomAttribute<ModuleShellAttribute>() != null);

            if (string.IsNullOrEmpty(modulesConfig.Shell.ShellName))
            {
                selectedShell = shells.FirstOrDefault();
            }
            else
            {
                selectedShell = shells.FirstOrDefault(shellType =>
                    shellType.GetCustomAttribute<ModuleShellAttribute>().Name.Equals(modulesConfig.Shell.ShellName));
            }

            if (selectedShell == null)
            {
                var fallbackShell = (FallbackShellViewModel)GlobalContainer.Resolve<IModuleShell>(FallbackShellViewModel.ShellName);
                fallbackShell.RunMode = Name;
                fallbackShell.ConfiguredShell = modulesConfig.Shell.ShellName;

                modulesConfig.Shell.ShellName = FallbackShellViewModel.ShellName;
                return;
            }

            var shellAttr = selectedShell.GetCustomAttribute<ModuleShellAttribute>();
            modulesConfig.Shell.ShellName = shellAttr.Name;
        }

        /// <summary>
        /// Gets the module configs from assembly.
        /// </summary>
        protected IEnumerable<ModulConfig> GetModuleConfigsFromAssembly(Assembly assembly, ModulesConfiguration modulesConfiguration)
        {
            var moduleConfigs = new List<ModulConfig>();
            var modules = assembly.GetTypes().Where(t => typeof(IClientModule).IsAssignableFrom(t)).ToList();

            foreach (var module in modules)
            {
                var att = module.GetCustomAttribute<ClientModuleAttribute>();
                if (att == null)
                    continue;

                var configuredModule = modulesConfiguration.Modules.FirstOrDefault(m => m.ModuleName == att.Name);
                if (configuredModule != null)
                    continue;

                var libraryName = module.Assembly.ManifestModule.ScopeName;

                configuredModule = new ModulConfig
                {
                    ModuleName = att.Name,
                    SortIndex = 9999,
                    IsEnabled = true,
                    Accesses = new Dictionary<string, OperationAccess>()
                };

                moduleConfigs.Add(configuredModule);
            }

            return moduleConfigs;
        }

        private static IEnumerable<string> GetAllLocalUsergroups()
        {
            var currentIdentity = WindowsIdentity.GetCurrent();
            var groups = new List<string>();

            if (currentIdentity.Groups == null)
            {
                return groups.ToList();
            }

            foreach (var group in currentIdentity.Groups)
            {
                try
                {
                    var newGroup = @group.Translate(typeof(NTAccount)).ToString();
                    groups.Add(newGroup);
                }
                // ReSharper disable once EmptyGeneralCatchClause
                catch (Exception)
                {
                    //dont break here
                }
            }
            return groups.ToList();
        }

        /// <summary>
        /// Will intialize the <see cref="IUserInfoProvider"/> with basic information
        /// </summary>
        protected void LoadUserInfos()
        {
            LoadUserInfos(string.Empty, null);
        }

        /// <summary>
        /// Will initialize the <see cref="IUserInfoProvider"/> with basic information and given groups
        /// </summary>
        protected void LoadUserInfos(IEnumerable<string> userGroups)
        {
            LoadUserInfos(string.Empty, userGroups);
        }

        /// <summary>
        /// Will initialize the <see cref="IUserInfoProvider"/> with basic information, given groups and user name
        /// </summary>
        protected void LoadUserInfos(string userName, IEnumerable<string> userGroups)
        {
            var currentIdentity = WindowsIdentity.GetCurrent();

            if (string.IsNullOrEmpty(userName))
            {
                userName = currentIdentity.Name;
            }

            if (userGroups == null)
            {
                try
                {
                    userGroups = GetAllLocalUsergroups();
                }
                catch (Exception)
                {
                    userGroups = new List<string>();
                }
            }

            const string firstName = "unknown";
            const string lastName = "unknown";

            UserInfoProvider.InitializeOnce(userName, userGroups.ToList(), firstName, lastName);
        }

        #region RaiseEvents

        /// <inheritdoc />
        public event EventHandler<AssemblyConfiguration> AssemblyConfigurationLoaded;

        /// <summary>
        /// Raises the <see cref="AssemblyConfigurationLoaded"/> event if someone is registered.
        /// </summary>
        protected virtual void RaiseAssemblyConfigurationLoaded(AssemblyConfiguration assemblyConfiguration)
        {
            AssemblyConfigurationLoaded?.Invoke(this, assemblyConfiguration);
        }

        /// <inheritdoc />
        public event EventHandler<AssemblyConfig> AssemblyLoaded;

        /// <summary>
        /// Raises the <see cref="AssemblyLoaded"/> event if someone is registered.
        /// </summary>
        protected virtual void RaiseAssemblyLoaded(AssemblyConfig assemblyConfig)
        {
            AssemblyLoaded?.Invoke(this, assemblyConfig);
        }

        /// <inheritdoc />
        public event EventHandler<IEnumerable<Assembly>> AssembliesLoaded;

        /// <summary>
        /// Raises the <see cref="AssembliesLoaded"/> event if someone is registered.
        /// </summary>
        protected internal void RaiseAssembliesLoaded(IEnumerable<Assembly> assemblies)
        {
            AssembliesLoaded?.Invoke(this, assemblies);
        }

        /// <inheritdoc />
        public event EventHandler<ModulesConfiguration> LoadModulesConfigurationCompleted;

        /// <summary>
        /// Raises the <see cref="LoadModulesConfigurationCompleted"/> event if someone is registered.
        /// </summary>
        protected virtual void RaiseModulesConfigurationLoaded(ModulesConfiguration e)
        {
            LoadModulesConfigurationCompleted?.Invoke(this, e);
        }

        /// <inheritdoc />
        public event EventHandler<ClientException> ExceptionOccurred;

        /// <summary>
        /// Raises the <see cref="ExceptionOccurred"/> event if someone is registered.
        /// </summary>
        protected virtual void RaiseException(ClientException e)
        {
            ExceptionOccurred?.Invoke(this, e);
        }

        #endregion
    }
}
