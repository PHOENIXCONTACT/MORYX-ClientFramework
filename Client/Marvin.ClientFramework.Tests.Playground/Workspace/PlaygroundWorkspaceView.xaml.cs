using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Marvin.ClientFramework.Tests.Playground
{
    /// <summary>
    /// Interaction logic for NotifyAndEditorWorkspaceView.xaml
    /// </summary>
    public partial class PlaygroundWorkspaceView : UserControl
    {
        public PlaygroundWorkspaceView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //InfoBox.BaseBrush = new SolidColorBrush(Colors.Red);
        }
    }
}
