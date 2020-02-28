using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Marvin.ClientFramework.Configurator.Properties;
using Marvin.ClientFramework.Kernel;
using Marvin.Configuration;
using Marvin.Container;

namespace Marvin.ClientFramework.Configurator
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

        public override string DisplayName => Strings.AppConfig_ShortTitle;

        public override string ImageSource => "/Controls4Industry;component/Images/wrench.png";

        #endregion

        protected override void OnInitialize()
        {
            base.OnInitialize();

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