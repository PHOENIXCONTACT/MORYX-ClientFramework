// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using C4I;
using Caliburn.Micro;
using Marvin.Controls.Demo.Models;
using Marvin.Serialization;

namespace Marvin.Controls.Demo.ViewModels
{
    public class EntryEditorViewModel : Screen
    {
        public override string DisplayName => "EntryEditor";

        public EntryViewModel EntryViewModels { get; }

        public ICommand ShowExceptionCommand { get; }

        public EntryEditorViewModel()
        {
            var entryModel = new EntryClass
            {
                ArrayOfByte = new byte[] { 0x01, 0xF3 },
                ChainOfChars = "HelloWorld",
                ListSubClass = new List<EntrySubClass>
                {
                    new EntrySubClass
                    {
                        AByte = 0x33
                    }
                },
                SubClass = new EntrySubClass
                {
                    AByte = 0xE2
                },
                ListEnum = new List<Visibility>
                {
                    Visibility.Hidden,
                    Visibility.Visible,
                    Visibility.Collapsed
                },
                ListBool = new List<bool>
                {
                    true,
                    false,
                    false
                },
                ListString = new List<string>
                {
                    "Hello",
                    "World"
                },
                File = "",
                Password = "secret"
            };

            var entry = EntryConvert.EncodeObject(entryModel);
            entry.DisplayName = "Root";
            EntryViewModels = new EntryViewModel(entry);

            ShowExceptionCommand = new RelayCommand(ShowException);
        }

        public void ShowException(object parameter)
        {
            MessageBox.Show("Use this command to show additional information about the exception.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
