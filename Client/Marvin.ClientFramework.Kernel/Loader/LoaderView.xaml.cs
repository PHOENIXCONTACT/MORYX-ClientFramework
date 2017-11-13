using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Marvin.ClientFramework.Kernel
{
    public partial class LoaderView : UserControl, ILoaderView
    {
        public LoaderView()
        {
            InitializeComponent();

            StatusMessage = "One moment please ...";
        }

        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum", typeof (int), typeof (LoaderView), new PropertyMetadata(default(int)));

        public int Maximum
        {
            get { return (int) GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof (int), typeof (LoaderView), new PropertyMetadata(default(int)));

        public int Value
        {
            get { return (int) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        
        public static readonly DependencyProperty StatusMessageProperty = DependencyProperty.Register("StatusMessage", typeof (string), typeof (LoaderView), new PropertyMetadata(default(string)));
        public string StatusMessage
        {
            get { return (string) GetValue(StatusMessageProperty); }
            set { SetValue(StatusMessageProperty, value); }
        }

        public static readonly DependencyProperty AppnNameProperty = DependencyProperty.Register(
            "AppnName", typeof (string), typeof (LoaderView), new PropertyMetadata(default(string)));

        public string AppnName
        {
            get { return (string) GetValue(AppnNameProperty); }
            set { SetValue(AppnNameProperty, value); }
        }

        public void IndicateError()
        {
            ProgressBar.Foreground = new SolidColorBrush(Colors.Red);
        }

        public void Connect(int connectionId, object target)
        {
            throw new System.NotImplementedException();
        }
    }
}
