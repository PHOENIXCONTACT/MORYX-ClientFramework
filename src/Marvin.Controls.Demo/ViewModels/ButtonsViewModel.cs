using System.Windows;
using System.Windows.Input;
using C4I;
using Caliburn.Micro;

namespace Marvin.Controls.Demo.ViewModels
{
    public class ButtonsViewModel : Screen
    {
        public override string DisplayName => "Buttons";

        public ICommand PopupTestCommand { get; }

        public ICommand SpeedTestWindowCommand { get; }

        public ButtonsViewModel()
        {
            PopupTestCommand = new RelayCommand(obj => MessageBox.Show("Works"));

            SpeedTestWindowCommand = new RelayCommand(o =>
            {
                if (!(o is EddieButton speedTestButton))
                    return;

                speedTestButton.Icon = MdiShapeFactory.GetShapeGeometry(MdiShapeType.Reload);

                var speedTest = new SpeedTestWindow();
                speedTest.ShowDialog();

                speedTestButton.Icon = CommonShapeFactory.GetShapeGeometry(CommonShapeType.Unset);
            });
        }
    }
}
