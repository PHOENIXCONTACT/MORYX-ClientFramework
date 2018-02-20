﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using C4I;

namespace Marvin.Controls.Demo
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
                    LabelMinWidth = 120.0,
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
