using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <summary>
    /// Compo box implementation with addtional features like clear button or text selection
    /// </summary>
    public class EddieComboBox : ComboBox
    {
        /// <summary>
        /// Dependency property for the <see cref="ClearButtonVisibility"/>
        /// </summary>
        public static readonly DependencyProperty ClearButtonVisibilityProperty =
            DependencyProperty.Register("ClearButtonVisibility", typeof (Visibility), typeof (EddieComboBox),
                new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Dependency property for the <see cref="AllowClearButton"/>
        /// </summary>
        public static readonly DependencyProperty AllowClearButtonProperty = DependencyProperty.Register(
            "AllowClearButton", typeof (bool), typeof (EddieComboBox), new PropertyMetadata(false));

        /// <summary>
        /// Initializes the <see cref="EddieComboBox"/> class.
        /// </summary>
        static EddieComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (EddieComboBox),
                new FrameworkPropertyMetadata(typeof (EddieComboBox)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EddieComboBox"/> class.
        /// </summary>
        public EddieComboBox()
        {
            SelectionChanged += SelectedItemChanged;
        }

        /// <summary>
        /// The clear button visibility
        /// </summary>
        public Visibility ClearButtonVisibility
        {
            get { return (Visibility) GetValue(ClearButtonVisibilityProperty); }
            set { SetValue(ClearButtonVisibilityProperty, value); }
        }

        /// <summary>
        /// Indicates weather the clear button is visible or not
        /// </summary>
        public bool AllowClearButton
        {
            get { return (bool) GetValue(AllowClearButtonProperty); }
            set { SetValue(AllowClearButtonProperty, value); }
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