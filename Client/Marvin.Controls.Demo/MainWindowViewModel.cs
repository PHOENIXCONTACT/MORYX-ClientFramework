using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using C4I;

namespace Marvin.Controls.Demo
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        public ICollection<string> ShapeTypes { get; }

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

            ShapeTypes = new List<string>(Enum.GetNames(typeof(CommonShapeType)));
        }
    }
}
