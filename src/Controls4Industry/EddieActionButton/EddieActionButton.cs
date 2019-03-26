using System.Windows;

namespace C4I
{
    /// <summary>
    /// Icon only styled button
    /// </summary>
    public class EddieActionButton : EddieButtonBase
    {
        static EddieActionButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieActionButton), new FrameworkPropertyMetadata(typeof(EddieActionButton)));
        }
    }
}