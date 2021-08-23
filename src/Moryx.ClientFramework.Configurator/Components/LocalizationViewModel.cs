// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Moryx.ClientFramework.Configurator.Properties;
using Moryx.ClientFramework.Localization;

namespace Moryx.ClientFramework.Configurator
{
    [ConfigViewModelPlugin]
    internal class LocalizationViewModel : Screen, IConfigViewModel
    {
        private CultureInfo _selectedCulture;
        private UserLanguageConfig _languageConfig;

        public override string DisplayName => Strings.LocalizationViewModel_Title;

        public string ImageSource => "/Moryx.WpfToolkit;component/Images/globe.png";

        public IAppDataConfigManager AppDataConfigManager { get; set; }

        public CultureInfo SelectedCulture
        {
            get => _selectedCulture;
            set
            {
                _selectedCulture = value;
                NotifyOfPropertyChange();
            }
        }

        public CultureInfo[] Cultures { get; private set; }

        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);

            _languageConfig = AppDataConfigManager.GetConfiguration<UserLanguageConfig>();

            var cultures = GetSupportedCulture().ToList();
            var enCulture = new CultureInfo("en");
            if (!cultures.Contains(enCulture))
                cultures.Add(enCulture);

            Cultures = cultures.ToArray();
            SelectedCulture = Cultures.FirstOrDefault(c => c.IetfLanguageTag == _languageConfig.Culture);
        }

        public void SaveConfig()
        {
            if (SelectedCulture == null)
                return;

            _languageConfig.Culture = SelectedCulture.IetfLanguageTag;
            AppDataConfigManager.SaveConfiguration(_languageConfig);
        }

        public IEnumerable<CultureInfo> GetSupportedCulture()
        {
            //Get all culture
            var culture = CultureInfo.GetCultures(CultureTypes.AllCultures);

            //Find the location where application installed.
            var exeLocation = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));

            //Return all culture for which satellite folder found with culture code.
            return culture.Where(cultureInfo => !string.IsNullOrEmpty(cultureInfo.IetfLanguageTag) && Directory.Exists(Path.Combine(exeLocation, cultureInfo.IetfLanguageTag)));
        }
    }
}
