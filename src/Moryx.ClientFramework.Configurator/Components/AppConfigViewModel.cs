// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Moryx.ClientFramework.Configurator.Properties;
using Moryx.ClientFramework.Kernel;
using Moryx.Configuration;
using Moryx.Container;

namespace Moryx.ClientFramework.Configurator
{
    [ConfigViewModelPlugin]
    internal class AppConfigViewModel : ConfigViewModelBase<AppConfig>
    {
        #region Dependency Injection

        public IEnumerable<IRunMode> RunModes { get; set; }

        public IAppDataConfigManager AppDataConfigManager { get; set; }

        public IUserInfoProvider UserInfoProvider { get; set; }

        public IEnumerable<IViewLocatorConfiguratorPreset> Presets { get; set; }

        #endregion

        #region Fields and Properties

        public override string DisplayName => Strings.AppConfigViewModel_Title;

        public override string ImageSource => "/Moryx.WpfToolkit;component/Images/wrench.png";

        #endregion

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            SystemProperties = new SystemProperties
            {
                Fullname = UserInfoProvider.FullName,
                Groups = UserInfoProvider.Groups,
                UserName = UserInfoProvider.UserName,
                ConfigPath = KernelConfigManager.ConfigDirectory,
                AppDataPath = AppDataConfigManager.ConfigDirectory,
            };
            NotifyOfPropertyChange(() => SystemProperties);
        }

        public IEnumerable ConfigStateValues => Enum.GetValues(typeof (ConfigState));

        public IEnumerable PossibleRunModes
        {
            get
            {
                var runModes = new List<string>();
                foreach (var runMode in RunModes)
                {
                    var att = runMode.GetType().GetCustomAttribute<KernelComponentAttribute>();
                    if (att == null)
                        continue;

                    runModes.Add(att.Name);
                }
                return runModes;
            }
        }

        public IEnumerable AvailableViewPresets
        {
            get { return Presets.Select(p => p.Name).ToArray(); }
        }

        private string _selectedRunMode;
        public string SelectedRunMode
        {
            get
            {
                return _selectedRunMode ?? Config.RunMode;
            }
            set
            {
                _selectedRunMode = value;

                if (_selectedRunMode != null)
                    Config.RunMode = _selectedRunMode;

                NotifyOfPropertyChange(() => SelectedRunMode);
            }
        }

        public SystemProperties SystemProperties { get; private set; }
    }

    internal class SystemProperties
    {
        public string UserName { get; set; }
        public List<string> Groups { get; set; }
        public string Fullname { get; set; }
        public string ConfigPath { get; set; }
        public string AppDataPath { get; set; }
    }
}
