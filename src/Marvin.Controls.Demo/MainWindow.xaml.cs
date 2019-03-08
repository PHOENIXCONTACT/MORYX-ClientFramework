using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using C4I;
using Marvin.Configuration;
using Marvin.Serialization;

namespace Marvin.Controls.Demo
{
    public class TestListViewEntry
    {
        public TestListViewEntry(long id, string text1, string text2, string text3, string text4, string text5)
        {
            Id = id;
            Text1 = text1;
            Text2 = text2;
            Text3 = text3;
            Text4 = text4;
            Text5 = text5;
        }

        public long Id { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Text5 { get; set; }
    }

    public class TestComboBoxEntry
    {
        public TestComboBoxEntry(string content)
        {
            Content = content;
        }

        public string Content { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            ShowExceptionCmd = new RelayCommand(parameters =>
            {
                var entry = (EntryViewModel) parameters;
                MessageBox.Show(entry.Value, "Exception");
            });

            InitializeComponent();

            ListViewEntries = new ObservableCollection<TestListViewEntry>();

            Loaded += OnLoaded;

            DataContext = new MainWindowViewModel();
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            ComboBoxEntries = new ObservableCollection<TestComboBoxEntry>()
            {
                new TestComboBoxEntry("Entry1"),
                new TestComboBoxEntry("Entry2"),
                new TestComboBoxEntry("Entry3"),
                new TestComboBoxEntry("Entry4"),
                new TestComboBoxEntry("Entry5"),
            };

            SelectedComboBoxEntry = ComboBoxEntries.First();

            OnPropertyChanged("ComboBoxEntries");
            OnPropertyChanged("SelectedComboBoxEntry");

            var entryModel = new EntryClass
            {
                ArrayOfByte = new byte[] { 0x01, 0xF3 },
                ChainOfChars = "HelloWorld",
                ListSubClass = new List<EntrySubClass>
                {
                    new EntrySubClass
                    {
                        AByte = 0x33
                    }
                },
                SubClass = new EntrySubClass
                {
                    AByte = 0xE2
                },
                File = "",
                Password = "secret"
            };

            var entry = EntryConvert.EncodeObject(entryModel);
            EntryViewModels = new EntryViewModel(entry);

            OnPropertyChanged(nameof(EntryViewModels));
        }

        public RelayCommand ShowExceptionCmd { get; set; }

        public EntryViewModel EntryViewModels { get; set; }

        private class EntryClass
        {
            [Description("Represents a string"), DefaultValue("Some default")]
            public string ChainOfChars { get; set; }

            public EntrySubClass SubClass { get; set; }

            public List<EntrySubClass> ListSubClass { get; set; }

            public byte[] ArrayOfByte { get; set; }

            [FileSystemPath(FileSystemPathType.File)]
            public string File { get; set; }

            [FileSystemPath(FileSystemPathType.Directory)]
            public string Directory { get; set; }

            [Password]
            public string Password { get; set; }

            public MemoryStream Stream { get; set; }

            public string ExceptionEntry
            {
                get { throw new InvalidOperationException("This is ver long Exception text to test if the exception editor is readable. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim."); }
            }
        }

        private class EntrySubClass
        {
            public byte AByte { get; set; }
        }

        public ObservableCollection<TestListViewEntry> ListViewEntries { get; set; }

        private void SpeedTestButtonClick(object sender, RoutedEventArgs e)
        {
            SpeedTestButton.Icon = CommonShapeType.Refresh;

            var speedTest = new SpeedTestWindow();
            speedTest.ShowDialog();

            SpeedTestButton.Icon = CommonShapeType.Unset;
        }

        public TestComboBoxEntry SelectedComboBoxEntry { get; set; }

        public ObservableCollection<TestComboBoxEntry> ComboBoxEntries { get; set; }

        private void ProgressBarIncreaseValueClick(object sender, RoutedEventArgs e)
        {
            ProgressBarTest.Value = ProgressBarTest.Value + 25;
            var prozent = Math.Round((100 / ProgressBarTest.Maximum) * ProgressBarTest.Value, 0).ToString();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You got a hug for free.");
        }

        private int _toggleState = 0;

        private void ToggleStateButton_OnClick(object sender, RoutedEventArgs e)
        {
            ToggleTextBoxState(TogglingTextBox);
        }

        private void ToggleTextBoxState(EddieTextBox box)
        {
            switch (_toggleState)
            {
                default:
                    box.Text = "Normal";
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState = 1;
                    break;
                case 1:
                    box.Text = "Readonly";
                    box.IsReadOnly = true;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
                case 2:
                    box.Text = "Disabled";
                    box.IsReadOnly = false;
                    box.IsEnabled = false;
                    _toggleState++;
                    break;
                case 3:
                    box.Text = "Changed Icon";
                    box.Icon = CommonShapeType.AttentionTriangle;
                    box.IconPath = null;
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
                case 4:
                    box.Text = "Changed Path";
                    box.IconPath = Geometry.Parse("F1M40.71,0.183C15.667,0.183 0.404,14.806 0.404,32.845 0.404,50.885 15.667,65.312 40.71,65.312 43.761,65.312 46.742,64.429 49.625,64.015 61.284,78.753 79.67,75.854 79.67,75.854 66.683,69.707 66.946,59.526 69.255,58.17 79.449,52.181 85.578,43.061 85.578,32.845 85.578,14.806 65.753,0.183 40.71,0.183z");
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
            }
        }

        private Thread _t;
        private bool _run = false;

        private void MultiProgressBarClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null)
                return;

            switch ((string) btn.Tag)
            {
                case "0":
                    if (_t == null)
                    {
                        MultiProgressBarTest.StepItems[0].Value = 0;
                        MultiProgressBarTest.StepItems[1].Value = 10;
                        MultiProgressBarTest.StepItems[2].Value = 0;
                        MultiProgressBarTest.StepItems[2].Value = 30;

                        _run = true;
                        _t = new Thread(o =>
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                ThreadControlButton.Content = "Stop";
                            });

                            while (_run)
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    if (MultiProgressBarTest.StepItems[0].Value + MultiProgressBarTest.StepItems[2].Value <
                                        MultiProgressBarTest.Max)
                                    {
                                        MultiProgressBarTest.StepItems[0].Value += 10;
                                    }
                                    else
                                    {
                                        MultiProgressBarTest.StepItems[0].Value += 10;
                                        MultiProgressBarTest.StepItems[2].Value -= 10;
                                    }
                                    if (MultiProgressBarTest.StepItems[2].Value <= 0)
                                    {
                                        _run = false;
                                        _t = null;
                                    }
                                });
                                Thread.Sleep(100);
                            }

                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                ThreadControlButton.Content = "Start";
                            });
                        }
                        );
                        _t.Start();
                    }
                    else
                    {
                        _run = false;
                        try
                        {
                            _t.Join(new TimeSpan(0, 0, 0, 0, 100));
                        }
                        catch (Exception exception)
                        {
                        }
                        finally
                        {
                            ThreadControlButton.Content = "Start";
                            _t = null;
                        }
                    }

                    break;
                case "1":
                    MultiProgressBarTest.StepItems[0].Value += 10;
                    break;
                case "2":
                    MultiProgressBarTest.StepItems[1].Value += 10;
                    break;
                case "3":
                    MultiProgressBarTest.StepItems[2].Value += 10;
                    break;
                case "4":
                    MultiProgressBarTest.StepItems[2].Value -= 10;
                    break;
                default:
                    throw new NotImplementedException("btn.tag has to be between a string between \"0\" and \"4\"");
            }
        }
    }
}
