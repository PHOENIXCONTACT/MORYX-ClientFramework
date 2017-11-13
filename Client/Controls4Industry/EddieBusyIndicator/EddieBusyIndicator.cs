using System.Windows;
using System.Windows.Controls;

namespace C4I
{
    public class EddieBusyIndicator : Control
    {
        static EddieBusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieBusyIndicator), new FrameworkPropertyMetadata(typeof(EddieBusyIndicator)));
        }
    }
}
