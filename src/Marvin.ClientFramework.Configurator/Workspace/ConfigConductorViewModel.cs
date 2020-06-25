using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caliburn.Micro;
using Marvin.ClientFramework.Commands;
using Marvin.ClientFramework.Dialog;
using Marvin.Container;

namespace Marvin.ClientFramework.Configurator
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

        public AsyncCommand SaveCurrentCmd { get; private set; }

        public AsyncCommand SaveAllCmd { get; private set; }

        #endregion

        /// <summary>
        /// Called when initializing.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            SaveCurrentCmd = new AsyncCommand(SaveCurrent);
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
        public async Task SaveCurrent(object parameters)
        {
            ActiveItem?.SaveConfig();

            await DialogManager.ShowMessageBoxAsync("The configuration has been saved successfully!", "Configuration saved",
                MessageBoxOptions.Ok, MessageBoxImage.Ok).ConfigureAwait(false);
        }

        /// <summary>
        /// Button interaction to save all configurations
        /// </summary>
        public async Task SaveAll(object parameters)
        {
            foreach (var configViewModel in ConfigScreens)
                configViewModel.SaveConfig();

            await DialogManager.ShowMessageBoxAsync("All configurations are successfully saved!", "All configs saved!",
                MessageBoxOptions.Ok, MessageBoxImage.Ok).ConfigureAwait(false);
        }
    }
}