using System.Windows.Input;
using System.Windows.Media;
using C4I;
using Caliburn.Micro;

namespace Marvin.Controls.Demo.ViewModels
{
    public class TextBoxesViewModel : Screen
    {
        private int _toggleState;

        public override string DisplayName => "TextBoxes";
        
        public ICommand ToggleStateCommand { get; }

        public TextBoxesViewModel()
        {
            ToggleStateCommand = new RelayCommand(ToggleState);
        }

        private void ToggleState(object parameter)
        {
            ToggleTextBoxState(parameter as EddieTextBox);
        }

        private void ToggleTextBoxState(EddieTextBox box)
        {
            switch (_toggleState)
            {
                default:
                    box.Text = "Normal";
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState = 1;
                    break;
                case 1:
                    box.Text = "Readonly";
                    box.IsReadOnly = true;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
                case 2:
                    box.Text = "Disabled";
                    box.IsReadOnly = false;
                    box.IsEnabled = false;
                    _toggleState++;
                    break;
                case 3:
                    box.Text = "Changed Icon";
                    box.Icon = CommonShapeType.AttentionTriangle;
                    box.IconPath = null;
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
                case 4:
                    box.Text = "Changed Path";
                    box.IconPath = Geometry.Parse("F1M40.71,0.183C15.667,0.183 0.404,14.806 0.404,32.845 0.404,50.885 15.667,65.312 40.71,65.312 43.761,65.312 46.742,64.429 49.625,64.015 61.284,78.753 79.67,75.854 79.67,75.854 66.683,69.707 66.946,59.526 69.255,58.17 79.449,52.181 85.578,43.061 85.578,32.845 85.578,14.806 65.753,0.183 40.71,0.183z");
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
            }
        }
    }
}
