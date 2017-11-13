using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.Container;

namespace Marvin.ClientFramework.Config
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

        /// <summary>
        /// Called when initializing.
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

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
        public void SaveCurrent()
        {
            if (ActiveItem != null)
            {
                ActiveItem.SaveConfig();
            }

            DialogManager.ShowMessageBox("The configuration has been saved successfully!", "Configuration saved",
                MessageBoxOptions.Ok, MessageBoxImage.Ok);
        }

        /// <summary>
        /// Button interaction to save all configurations
        /// </summary>
        public void SaveAll()
        {
            foreach (var configViewModel in ConfigScreens)
            {
                configViewModel.SaveConfig();
            }

            DialogManager.ShowMessageBox("All configurations are successfully saved!", "All configs saved!",
                MessageBoxOptions.Ok, MessageBoxImage.Ok);
        }
    }
}