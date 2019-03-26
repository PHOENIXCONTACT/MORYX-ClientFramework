using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Button for the module bar
    /// </summary>
    public class EddieModuleButton : Control
    {
        static EddieModuleButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieModuleButton), new FrameworkPropertyMetadata(typeof(EddieModuleButton)));
        }

        /// <summary>
        /// Text that is displayed on the button
        /// </summary>
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(EddieModuleButton), new PropertyMetadata(default(string)));

        /// <summary>
        /// Get or sets the text on the button
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// <see cref="EddieButtonStyle"/> for this button
        /// </summary>
        public static readonly DependencyProperty EddieStyleProperty = DependencyProperty.Register(
            "EddieStyle", typeof(EddieButtonStyle), typeof(EddieModuleButton), new PropertyMetadata(EddieButtonStyle.Green));


        /// <summary>
        /// Get or sets the <see cref="EddieButtonStyle"/> for this button
        /// </summary>
        public EddieButtonStyle EddieStyle
        {
            get { return (EddieButtonStyle)GetValue(EddieStyleProperty); }
            set { SetValue(EddieStyleProperty, value); }
        }

        /// <summary>
        /// Notification counter of this button
        /// </summary>
        public static readonly DependencyProperty NotificationsProperty = DependencyProperty.Register(
            "Notifications", typeof (int), typeof (EddieModuleButton), new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets the notification counter
        /// </summary>
        public int Notifications
        {
            get { return (int) GetValue(NotificationsProperty); }
            set { SetValue(NotificationsProperty, value); }
        }

        /// <summary>
        /// Icon on this button
        /// </summary>
        public static readonly DependencyProperty IconGeometryProperty = DependencyProperty.Register(
            "IconGeometry", typeof(Geometry), typeof(EddieModuleButton),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), null);

        /// <summary>
        /// Gets or sets the icon on this button
        /// </summary>
        public Geometry IconGeometry
        {
            get { return (Geometry)GetValue(IconGeometryProperty); }
            set { SetValue(IconGeometryProperty, value); }
        }

        /// <summary>
        /// Command that is executed when the button gets clicked
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            "Command", typeof (ICommand), typeof (EddieModuleButton), new PropertyMetadata(default(ICommand)));

        /// <summary>
        /// Gets or sets the command
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Click event that is raised when the button gets clicked
        /// </summary>
        public event RoutedEventHandler Click;
        /// <summary>
        /// Raises the <see cref="Click"/> event
        /// </summary>
        /// <param name="e"></param>
        protected virtual void RaiseClick(RoutedEventArgs e)
        {
            var handler = Click;
            handler?.Invoke(this, e);
        }

        /// <inheritdoc />
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
            RaiseClick(routedEventArgs);
        }
    }
}