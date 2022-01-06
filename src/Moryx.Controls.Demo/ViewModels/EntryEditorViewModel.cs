// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Moryx.Controls.Demo.Models;
using Moryx.Serialization;
using Moryx.WpfToolkit;

namespace Moryx.Controls.Demo.ViewModels
{
    public class EntryEditorViewModel : Screen
    {
        private bool _isEditMode;

        public override string DisplayName => "EntryEditor";

        public EntryViewModel EntryViewModels { get; }

        public ICommand ShowExceptionCommand { get; }

        public ICommand BeginEditCmd { get; }

        public ICommand EndEditCmd { get; }

        public ICommand CancelEditCmd { get; }

        public bool IsEditMode
        {
            get { return _isEditMode; }
            private set
            {
                _isEditMode = value;
                NotifyOfPropertyChange();
            }
        }

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
            BeginEditCmd = new RelayCommand(BeginEdit);
            EndEditCmd = new RelayCommand(EndEdit);
            CancelEditCmd = new RelayCommand(CancelEdit);
        }

        private void EndEdit(object obj)
        {
            IsEditMode = false;
            EntryViewModels.EndEdit();
        }

        private void CancelEdit(object obj)
        {
            IsEditMode = false;
            EntryViewModels.CancelEdit();
        }

        private void BeginEdit(object obj)
        {
            IsEditMode = true;
            EntryViewModels.BeginEdit();
        }

        public void ShowException(object parameter)
        {
            MessageBox.Show("Use this command to show additional information about the exception.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);

        }
    }
}
