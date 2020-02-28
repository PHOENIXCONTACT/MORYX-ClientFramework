using System.Linq;
using Caliburn.Micro;
using Marvin.Controls.Demo.ViewModels;

namespace Marvin.Controls.Demo.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        public ShellViewModel()
        {
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
