using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    public class DialogPanel : ContentControl
    {
        public DialogPanel()
        {
            Buttons = new List<EddieButton>();
        }

        public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register(
            "Buttons", typeof(IList), typeof(DialogPanel), new PropertyMetadata(default(IList)));

        public IList Buttons
        {
            get { return (IList)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            "Header", typeof (object), typeof (DialogPanel), new PropertyMetadata(default(object)));

        public object Header
        {
            get { return (object) GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty BusyVisibilityProperty = DependencyProperty.Register(
            "BusyVisibility", typeof(Visibility), typeof(DialogPanel), new PropertyMetadata(Visibility.Collapsed));

        public Visibility BusyVisibility
        {
            get { return (Visibility)GetValue(BusyVisibilityProperty); }
            set { SetValue(BusyVisibilityProperty, value); }
        }
    }
}