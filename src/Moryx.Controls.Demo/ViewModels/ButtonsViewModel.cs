// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Moryx.WpfToolkit;

namespace Moryx.Controls.Demo.ViewModels
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
            });
        }
    }
}
