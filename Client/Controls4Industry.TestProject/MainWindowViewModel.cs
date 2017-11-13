using System.Collections.ObjectModel;
using C4I;

namespace Controls4Industry.TestProject
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        private ObservableCollection<TestListViewEntry> _listViewItemsList = new ObservableCollection<TestListViewEntry>();

        public ObservableCollection<TestListViewEntry> ListViewItemsList
        {
            get { return _listViewItemsList; }
            set
            {
                _listViewItemsList = value;
                OnPropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            for (var i = 0; i < 1000; i++)
            {
                ListViewItemsList.Add(new TestListViewEntry(i, "Hello" + i, "World" + i, "Hello" + i, "Marvin" + i,
                    "Team" + i));
            }

        }
    }
}
