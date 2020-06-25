using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace Marvin.ClientFramework.Configurator
{
    /// <summary>
    /// Interaction logic for AppConfigView.xaml
    /// </summary>
    public partial class AppConfigView : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppConfigView"/> class.
        /// </summary>
        public AppConfigView()
        {
            InitializeComponent();
        }

        private void OpenPath(object sender, MouseButtonEventArgs e)
        {
            var textblock = (TextBlock) sender;

            try
            {
                if (textblock != null && !string.IsNullOrEmpty(textblock.Text))
                    Process.Start(textblock.Text);
            }
            catch (Exception)
            {
            }
        }
    }
}
