// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.ObjectModel;
using Caliburn.Micro;
using Moryx.Controls.Demo.Models;

namespace Moryx.Controls.Demo.ViewModels
{
    public class ListViewViewModel : Screen
    {
        private ObservableCollection<TestListViewEntry> _listViewItemsList = new ObservableCollection<TestListViewEntry>();

        public override string DisplayName => "ListView";

        public ObservableCollection<TestListViewEntry> ListViewItemsList
        {
            get { return _listViewItemsList; }
            set
            {
                _listViewItemsList = value;
                NotifyOfPropertyChange();
            }
        }

        public ListViewViewModel()
        {
            for (var i = 0; i < 1000; i++)
            {
                ListViewItemsList.Add(new TestListViewEntry(i, "Hello" + i, "World" + i, "Hello" + i, "Moryx" + i, "Team" + i));
            }
        }
    }
}
