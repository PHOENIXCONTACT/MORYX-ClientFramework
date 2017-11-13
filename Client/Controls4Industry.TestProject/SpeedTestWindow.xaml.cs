using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using C4I;

namespace Controls4Industry.TestProject
{
    /// <summary>
    /// Interaction logic for SpeedTestWindow.xaml
    /// </summary>
    public partial class SpeedTestWindow : Window
    {
        public SpeedTestWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var stop = new Stopwatch();
            stop.Start();

            for (var i = 0; i < 1000; i++)
            {
                this.VisualPanel.Children.Add(new LabeledControlHost()
                {
                    LabelA = "Hello Speed",
                    LabelB = "Label B",
                    LabelWidth = 120.0,
                    Content = new EddieButton()
                    {
                        Content = "Testbutton"
                    }
                });
            }

            this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                stop.Stop();
                MessageBox.Show("Took " + (stop.ElapsedMilliseconds) + " ms");
            }));
        }
    }
}
