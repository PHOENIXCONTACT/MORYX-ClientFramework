// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.ObjectModel;
using Caliburn.Micro;
using Moryx.Controls.Demo.Models;

namespace Moryx.Controls.Demo.ViewModels
{
    public class ItemSelectionViewModel : Screen
    {
        private TestComboBoxEntry _selectedComboBoxEntry;

        public override string DisplayName => "ItemSelection";

        public ObservableCollection<TestComboBoxEntry> ComboBoxEntries { get; }

        public TestComboBoxEntry SelectedComboBoxEntry
        {
            get { return _selectedComboBoxEntry; }
            set
            {
                if (_selectedComboBoxEntry != value)
                {
                    _selectedComboBoxEntry = value;
                    NotifyOfPropertyChange();
                }
            }
        }

        public ItemSelectionViewModel()
        {
            ComboBoxEntries = new ObservableCollection<TestComboBoxEntry>()
            {
                new TestComboBoxEntry("Entry1"),
                new TestComboBoxEntry("Entry2"),
                new TestComboBoxEntry("Entry3"),
                new TestComboBoxEntry("Entry4"),
                new TestComboBoxEntry("Entry5"),
            };

            SelectedComboBoxEntry = ComboBoxEntries[1];
        }
    }
}
