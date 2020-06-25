using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    /// <summary> 
    /// EddieSlider control lets the user select from a range of values by moving a slider. 
    /// EddieSlider is used to enable to user to gradually modify a value (range selection).
    /// </summary>
    public class EddieSlider : Slider
    {
        /// <summary>
        /// Initializes the <see cref="EddieSlider"/> class.
        /// </summary>
        static EddieSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieSlider), new FrameworkPropertyMetadata(typeof(EddieSlider)));
        }
    }
}