using System.Windows;

namespace C4I
{
    /// <summary>
    /// Default button with additional features of the <see cref="EddieButtonBase"/>
    /// </summary>
    public class EddieButton : EddieButtonBase
    {
        /// <summary>
        /// Initializes the <see cref="EddieButton"/> class.
        /// </summary>
        static EddieButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieButton), new FrameworkPropertyMetadata(typeof(EddieButton)));
        }
    }
}
