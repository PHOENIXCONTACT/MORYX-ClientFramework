using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Marvin.Serialization;

namespace Marvin.Controls
{
    /// <summary>
    /// Interaction logic for ConfigEditorGridxaml.xaml
    /// </summary>
    public partial class ConfigEditor : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigEditor"/> class.
        /// </summary>
        public ConfigEditor()
        {
            InitializeComponent();
        }

        #region Dependency properties

        public static readonly DependencyProperty RootEntryProperty = DependencyProperty.Register(
            "RootEntry", typeof(EntryViewModel), typeof(ConfigEditor), new PropertyMetadata(null, RootEntryChanged));

        public EntryViewModel RootEntry
        {
            get { return (EntryViewModel)GetValue(RootEntryProperty); }
            set { SetValue(RootEntryProperty, value); }
        }

        private static void RootEntryChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = (ConfigEditor)sender;
            var root = (EntryViewModel) args.NewValue;

            control.Path.Clear();
            control.Path.Add(root);

            control.CurrentEntry = root;
        }

        public static readonly DependencyProperty BreadcrumbVisibilityProperty = DependencyProperty.Register(
            "BreadcrumbVisibility", typeof (Visibility), typeof (ConfigEditor), new PropertyMetadata(Visibility.Visible));

        public Visibility BreadcrumbVisibility
        {
            get { return (Visibility) GetValue(BreadcrumbVisibilityProperty); }
            set { SetValue(BreadcrumbVisibilityProperty, value); }
        }

        public static readonly DependencyProperty IsEditModeProperty = DependencyProperty.Register(
            "IsEditMode", typeof (bool), typeof (ConfigEditor), new PropertyMetadata(default(bool)));

        public bool IsEditMode
        {
            get { return (bool) GetValue(IsEditModeProperty); }
            set { SetValue(IsEditModeProperty, value); }
        }

        #endregion

        public ObservableCollection<EntryViewModel> Path { get; } = new ObservableCollection<EntryViewModel>();

        private EntryViewModel _currentEntry;
        public EntryViewModel CurrentEntry
        {
            get { return _currentEntry; }
            set
            {
                _currentEntry = value;

                if (_currentEntry?.PossibleValues != null)
                {
                    var desiredType = _currentEntry.PossibleValues.FirstOrDefault(p => p == _currentEntry.Value);
                    if (desiredType == null)
                    {
                        desiredType = _currentEntry.PossibleValues.First();
                    }

                    DesiredType = desiredType;
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(CanBack));
                OnPropertyChanged(nameof(CanForward));
            }
        }

        private string _desiredType;
        public string DesiredType
        {
            get { return _desiredType; }
            set
            {
                _desiredType = value;
                OnPropertyChanged();
            }
        }

        private void DelveIntoSubEntry(object sender, RoutedEventArgs e)
        {
            var entry = GetEntry(sender);
            // If allready in there just move on
            if (Path.Contains(entry))
            {
                CurrentEntry = entry;
                return;
            }

            // Remove everything from current to end
            var index = Path.IndexOf(CurrentEntry);
            while (index < Path.Count - 1)
                Path.RemoveAt(index + 1);
            Path.Add(entry);
            CurrentEntry = entry;
        }

        private void RemoveCollectionEntry(object sender, RoutedEventArgs e)
        {
            CurrentEntry.SubEntries.Remove(GetEntry(sender));
        }

        public bool CanBack => Path.IndexOf(CurrentEntry) > 0;

        private void Back(object sender, RoutedEventArgs e)
        {
            var index = Path.IndexOf(CurrentEntry);
            CurrentEntry = Path.ElementAt(--index);
        }

        public bool CanForward => Path.IndexOf(CurrentEntry) < Path.Count - 1;

        private void Forward(object sender, RoutedEventArgs e)
        {
            var index = Path.IndexOf(CurrentEntry);
            CurrentEntry = Path.ElementAt(++index);
        }

        private void ItemRequested(object sender, RoutedEventArgs e)
        {
            if (CurrentEntry.ValueType == EntryValueType.Collection)
            {
                CurrentEntry.AddPrototype(DesiredType);
            }
            else
            {
                CurrentEntry.ReplaceWithPrototype(DesiredType);
            }
        }

        private static EntryViewModel GetEntry(object eventSender)
        {
            // Find sending entry
            var button = (Button)eventSender;
            return (EntryViewModel)button.DataContext;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}