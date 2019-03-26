using System.Windows;

namespace Marvin.ClientFramework.Tests.Playground
{
    /// <summary>
    /// Interaction logic for NotifyAndEditorWorkspaceView.xaml
    /// </summary>
    public partial class PlaygroundWorkspaceView
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
