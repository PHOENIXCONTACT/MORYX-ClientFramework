using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;
using C4I;

namespace Marvin.Controls.Demo
{
    public class MainWindowViewModel : PropertyChangedBase
    {
        public ICollection<string> ShapeTypes { get; }

        private ObservableCollection<TestListViewEntry> _listViewItemsList = new ObservableCollection<TestListViewEntry>();
        private bool _isNavigationBarLocked;
        private string _selectedCulture = "en";

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

            TestCommand = new RelayCommand(obj => MessageBox.Show("Works"));
        }

        public bool IsNavigationBarLocked
        {
            get { return _isNavigationBarLocked; }
            set
            {
                if (_isNavigationBarLocked != value)
                {
                    _isNavigationBarLocked = value;
                    OnPropertyChanged();
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
                    OnPropertyChanged();

                    CultureInfoHandler.Instance.ChangeCulture(CultureInfo.GetCultureInfo(_selectedCulture));
                }
            }
        }
    }
}
