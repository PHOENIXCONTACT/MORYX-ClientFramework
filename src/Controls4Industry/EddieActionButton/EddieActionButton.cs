using System.Windows;

namespace C4I
{
    public class EddieActionButton : EddieButtonBase
    {
        static EddieActionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieActionButton), new FrameworkPropertyMetadata(typeof(EddieActionButton)));
        }
    }
}