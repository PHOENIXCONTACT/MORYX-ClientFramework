using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace C4I
{
    /// <summary>
    /// Compo box implementation with addtional features like clear button or text selection
    /// </summary>
    public class EddieComboBox : ComboBox
    {
        /// <summary>
        /// Initializes the <see cref="EddieComboBox"/> class.
        /// </summary>
        static EddieComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieComboBox),
                new FrameworkPropertyMetadata(typeof(EddieComboBox)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EddieComboBox"/> class.
        /// </summary>
        public EddieComboBox()
        {
            SelectionChanged += SelectedItemChanged;
        }

        /// <summary>
        /// Dependency property for the <see cref="ClearButtonVisibility"/>
        /// </summary>
        public static readonly DependencyProperty ClearButtonVisibilityProperty =
            DependencyProperty.Register(nameof(ClearButtonVisibility), typeof (Visibility), typeof (EddieComboBox),
                new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// The clear button visibility
        /// </summary>
        public Visibility ClearButtonVisibility
        {
            get => (Visibility)GetValue(ClearButtonVisibilityProperty);
            set => SetValue(ClearButtonVisibilityProperty, value);
        }

        /// <summary>
        /// Dependency property for the <see cref="AllowClearButton"/>
        /// </summary>
        public static readonly DependencyProperty AllowClearButtonProperty = DependencyProperty.Register(
            nameof(AllowClearButton), typeof (bool), typeof (EddieComboBox), new PropertyMetadata(false));

        /// <summary>
        /// Indicates weather the clear button is visible or not
        /// </summary>
        public bool AllowClearButton
        {
            get => (bool)GetValue(AllowClearButtonProperty);
            set => SetValue(AllowClearButtonProperty, value);
        }

        /// <summary>
        /// DependencyProperty LockIcon type of <see cref="Geometry"/> which sets the icon on the combo box
        /// </summary>
        public static readonly DependencyProperty LockIconProperty = DependencyProperty.Register(
            nameof(LockIcon), typeof(Geometry), typeof(EddieComboBox), new FrameworkPropertyMetadata(CommonShapeFactory.GetShapeGeometry(CommonShapeType.Lock),
                FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender), null);

        /// <summary>
        /// Visible icon geometry of the combo box
        /// </summary>
        public Geometry LockIcon
        {
            get => (Geometry)GetValue(LockIconProperty);
            set => SetValue(LockIconProperty, value);
        }

        private void ClearSelectedItem(object sender, RoutedEventArgs e)
        {
            SelectedItem = null;
            ClearButtonVisibility = Visibility.Collapsed;
        }

        private void SelectedItemChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AllowClearButton)
                ClearButtonVisibility = Visibility.Visible;
        }

        ///
        public override void OnApplyTemplate()
        {
            var clearButton = GetTemplateChild("PART_ClearSelectedItemButton") as Button;
            if (clearButton != null)
            {
                clearButton.Click += ClearSelectedItem;
            }

            base.OnApplyTemplate();
        }
    }
}