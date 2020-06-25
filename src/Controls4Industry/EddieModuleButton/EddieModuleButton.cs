using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace C4I
{
    public class EddieModuleButton : Control
    {
        static EddieModuleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieModuleButton), new FrameworkPropertyMetadata(typeof(EddieModuleButton)));
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(EddieModuleButton), new PropertyMetadata(default(string)));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty EddieStyleProperty = DependencyProperty.Register(
            "EddieStyle", typeof(EddieButtonStyle), typeof(EddieModuleButton), new PropertyMetadata(EddieButtonStyle.Green));


        public EddieButtonStyle EddieStyle
        {
            get { return (EddieButtonStyle)GetValue(EddieStyleProperty); }
            set { SetValue(EddieStyleProperty, value); }
        }

        public static readonly DependencyProperty NotificationsProperty = DependencyProperty.Register(
            "Notifications", typeof (int), typeof (EddieModuleButton), new PropertyMetadata(0));

        public int Notifications
        {
            get { return (int) GetValue(NotificationsProperty); }
            set { SetValue(NotificationsProperty, value); }
        }

        public static readonly DependencyProperty IconGeometryProperty = DependencyProperty.Register(
            "IconGeometry", typeof(Geometry), typeof(EddieModuleButton),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), null);

        public Geometry IconGeometry
        {
            get { return (Geometry)GetValue(IconGeometryProperty); }
            set { SetValue(IconGeometryProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof (ICommand), typeof (EddieModuleButton), new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public event RoutedEventHandler Click;
        protected virtual void Raise_Click(RoutedEventArgs e)
        {
            var handler = Click;
            if (handler != null) handler(this, e);
        }
        
        public override void OnApplyTemplate()
        {
            var button = GetTemplateChild("PART_MainButton") as EddieButton;
            if (button != null)
            {
                button.Click += OnMainButtonClick;
                button.Command = Command;
            } 

            base.OnApplyTemplate();
        }

        private void OnMainButtonClick(object sender, RoutedEventArgs routedEventArgs)
        {
            Raise_Click(routedEventArgs);
        }
    }
}