using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// Dialog panel
    /// </summary>
    public class DialogPanel : ContentControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DialogPanel"/> class.
        /// </summary>
        public DialogPanel()
        {
            Buttons = new List<EddieButton>();
        }

        /// <summary>
        /// Buttons shown on the dialog
        /// </summary>
        public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register(
            "Buttons", typeof(IList), typeof(DialogPanel), new PropertyMetadata(default(IList)));

        /// <summary>
        /// Gets or sets the buttons
        /// </summary>
        public IList Buttons
        {
            get { return (IList)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        /// <summary>
        /// Header of the dialog
        /// </summary>
        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof (object), typeof (DialogPanel), new PropertyMetadata(default(object)));

        /// <summary>
        /// Gets or sets the header
        /// </summary>
        public object Header
        {
            get { return GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        /// <summary>
        /// Busy indicator visibility
        /// </summary>
        public static readonly DependencyProperty BusyVisibilityProperty = DependencyProperty.Register(
            "BusyVisibility", typeof(Visibility), typeof(DialogPanel), new PropertyMetadata(Visibility.Collapsed));

        /// <summary>
        /// Gets or sets the busy indicator visibility
        /// </summary>
        public Visibility BusyVisibility
        {
            get { return (Visibility)GetValue(BusyVisibilityProperty); }
            set { SetValue(BusyVisibilityProperty, value); }
        }
    }
}