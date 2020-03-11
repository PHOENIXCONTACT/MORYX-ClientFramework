using System.Windows.Input;
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
                    box.Icon = CommonShapeFactory.GetShapeGeometry(CommonShapeType.AttentionTriangle);
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
                case 4:
                    box.Text = "Changed Path";
                    box.Icon = CommonShapeFactory.GetShapeGeometry(CommonShapeType.SpeakBubble);
                    box.IsReadOnly = false;
                    box.IsEnabled = true;
                    _toggleState++;
                    break;
            }
        }
    }
}
