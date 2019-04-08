using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using C4I;
using Caliburn.Micro;
using Marvin.Controls.Demo.ViewModels;

namespace Marvin.Controls.Demo.Shell
{
    public class ShellViewModel : Screen
    {
        private string _selectedCulture = "en";
        private Screen _selectedTabItem;

        public List<Screen> TabItems { get; }

        public Screen SelectedTabItem
        {
            get { return _selectedTabItem; }
            set
            {
                if (_selectedTabItem != value)
                {
                    _selectedTabItem = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public string[] AvailableCultures => new[]
        {
            CultureInfo.GetCultureInfo("en").Name, CultureInfo.GetCultureInfo("de").Name,
            CultureInfo.GetCultureInfo("pl").Name
        };

        public string SelectedCulture
        {
            get { return _selectedCulture; }
            set
            {
                if (_selectedCulture != value)
                {
                    _selectedCulture = value;
                    NotifyOfPropertyChange();

                    CultureInfoHandler.Instance.ChangeCulture(CultureInfo.GetCultureInfo(_selectedCulture));
                }
            }
        }

        public ShellViewModel()
        {
            TabItems = new List<Screen>
            {
                new ButtonsViewModel(),
                new ComboBoxesViewModel(),
                new ProgressViewModel(),
                new ItemSelectionViewModel(),
                new TextBoxesViewModel(),
                new PanelsViewModel(),
                new ListViewViewModel(),
                new ListBoxViewModel(),
                new TreeViewViewModel(),
                new TabControlViewModel(),
                new IconsViewModel(),
                new SliderViewModel(),
                new TemplatesViewModel(),
                new NavigationBarViewModel(),
                new EntryEditorViewModel()
            };

            SelectedTabItem = TabItems.First();
        }
    }
}
