// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Moryx.ClientFramework.Commands;
using Moryx.ClientFramework.Configurator.Properties;
using Moryx.ClientFramework.Dialog;
using Moryx.Container;
using MessageBoxImage = Moryx.ClientFramework.Dialog.MessageBoxImage;
using MessageBoxOptions = Moryx.ClientFramework.Dialog.MessageBoxOptions;

namespace Moryx.ClientFramework.Configurator
{
    /// <summary>
    /// Conductor for all <see cref="IConfigViewModel"/>
    /// </summary>
    [Plugin(LifeCycle.Singleton, typeof(IConfigConductorViewModel))]
    internal class ConfigConductorViewModel : Conductor<IConfigViewModel>.Collection.OneActive, IConfigConductorViewModel
    {
        #region Dependency Injection

        /// <summary>
        /// Gets or sets the dialog manager.
        /// </summary>
        public IDialogManager DialogManager { get; set; }

        /// <summary>
        /// Gets or sets the configuration screens.
        /// </summary>
        public IEnumerable<IConfigViewModel> ConfigScreens { get; set; }

        #endregion

        #region Fields and Properties

        public AsyncCommand SaveAndRestartCmd { get; private set; }

        public AsyncCommand SaveAllCmd { get; private set; }

        #endregion

        /// <summary>
        /// Called when initializing.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            SaveAndRestartCmd = new AsyncCommand(SaveAndRestart);
            SaveAllCmd = new AsyncCommand(SaveAll);

            Items.Clear();

            foreach (var screen in ConfigScreens.OrderBy(t => t.DisplayName))
            {
                Items.Add(screen);
            }

            ActivateItem(Items.First());
        }

        /// <summary>
        /// Button interaction to save the current active configuration
        /// </summary>
        public async Task SaveAndRestart(object parameters)
        {
            await SaveAll(parameters);

            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Button interaction to save all configurations
        /// </summary>
        public Task SaveAll(object parameters)
        {
            foreach (var configViewModel in ConfigScreens)
                configViewModel.SaveConfig();

            return DialogManager.ShowMessageBoxAsync(Strings.ConfigConductorViewModel_ConfigsSaved_Message,
                Strings.ConfigConductorViewModel_ConfigsSaved_Title, MessageBoxOptions.Ok, MessageBoxImage.Ok);
        }
    }
}
