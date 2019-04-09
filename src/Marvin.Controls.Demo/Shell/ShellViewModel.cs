using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using C4I;
using Caliburn.Micro;
using Marvin.Controls.Demo.ViewModels;

namespace Marvin.Controls.Demo.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        private string _selectedCulture;

        public string[] AvailableCultures { get; }

        public string SelectedCulture
        {
            get { return _selectedCulture; }
            set
            {
                _selectedCulture = value;
                NotifyOfPropertyChange();

                CultureInfoHandler.Instance.ChangeCulture(CultureInfo.GetCultureInfo(_selectedCulture));
            }
        }

        public ShellViewModel()
        {
            AvailableCultures = CultureInfo.GetCultures(
                CultureTypes.AllCultures & ~CultureTypes.SpecificCultures
            ).Select(c => c.IetfLanguageTag).ToArray();

            _selectedCulture = Thread.CurrentThread.CurrentUICulture.IetfLanguageTag;

            Items.AddRange(new Screen[]
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
            });
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            ActivateItem(Items.First());
        }
    }
}
