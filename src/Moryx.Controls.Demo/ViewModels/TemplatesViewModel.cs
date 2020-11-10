// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Moryx.WpfToolkit;

namespace Moryx.Controls.Demo.ViewModels
{
    public class TemplatesViewModel : Screen
    {
        private Visibility _busyVisibility;
        public override string DisplayName => "Templates";

        public ICommand BusyCmd { get; set; }

        public ICommand NotBusyCmd { get; set; }

        public TemplatesViewModel()
        {
            BusyCmd = new RelayCommand(Busy);
            NotBusyCmd = new RelayCommand(NotBusy);
        }

        public Visibility BusyVisibility
        {
            get => _busyVisibility;
            set
            {
                _busyVisibility = value;
                NotifyOfPropertyChange();
            }
        }

        private void Busy(object obj)
        {
            BusyVisibility = Visibility.Visible;
        }

        private void NotBusy(object obj)
        {
            BusyVisibility = Visibility.Collapsed;
        }
    }
}
