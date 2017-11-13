using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    public class EddieNavigationBar : TabControl
    {
        static EddieNavigationBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieNavigationBar), new FrameworkPropertyMetadata(typeof(EddieNavigationBar)));
        }
    }
}