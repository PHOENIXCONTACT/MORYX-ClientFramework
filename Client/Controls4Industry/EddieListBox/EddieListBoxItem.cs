using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <summary> 
    /// A child of a <see cref="EddieListBox" />.
    /// </summary> 
    public class EddieListBoxItem : ListBoxItem
    {
        static EddieListBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieListBoxItem), new FrameworkPropertyMetadata(typeof(EddieListBoxItem)));
        }
    }
}