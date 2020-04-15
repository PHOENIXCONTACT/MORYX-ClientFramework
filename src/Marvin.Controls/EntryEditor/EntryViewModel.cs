// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Marvin.Serialization;

namespace Marvin.Controls
{
    /// <summary>
    /// View model that represents a page in the config editor
    /// </summary>
    public class EntryViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The entry of this view model
        /// </summary>
        public Entry Entry { get; }

        /// <summary>
        /// Parent view model
        /// </summary>
        public EntryViewModel Parent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryViewModel"/> class.
        /// </summary>
        public EntryViewModel(IList<Entry> entries)
        {
            DisplayName = "Root";
            ValueType = EntryValueType.Class;
            SubEntries = new ObservableWrapperCollection(entries);

            UpdateParent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryViewModel"/> class.
        /// </summary>
        public EntryViewModel(Entry entry)
        {
            Entry = entry;
            DisplayName = Entry.DisplayName;
            ValueType = Entry.Value.Type;
            UnitType = Entry.Value.UnitType;
            SubEntries = new ObservableWrapperCollection(entry.SubEntries);

            UpdateParent();
        }

        private void UpdateParent(EntryViewModel entry)
        {
            entry.Parent = this;
        }

        private void UpdateParent()
        {
            foreach (var entryViewModel in SubEntries)
            {
                UpdateParent(entryViewModel);
            }
        }

        ///
        public string DisplayName { get; }

        ///
        public string Value
        {
            get { return Entry.Value.Current; }
            set
            {
                Entry.Value.Current = value;
                OnPropertyChanged();
            }
        }

        ///
        public EntryValueType ValueType { get; }

        ///
        public EntryUnitType UnitType { get; }

        ///
        public string DefaultValue => Entry.Value.Default;

        ///
        public string Description => Entry.Description;

        /// <summary>
        /// Flag if this entry is readonly and text boxes shall be disabled
        /// </summary>
        public bool IsReadOnly => Entry.Value.IsReadOnly;

        /// <summary>
        /// Add prototype of this name to the subentries
        /// </summary>
        public void AddPrototype(string prototypeName)
        {
            var prototype = Entry.GetPrototype(prototypeName).Instantiate();
            var prototypeVm = new EntryViewModel(prototype);
            SubEntries.Add(prototypeVm);

            UpdateParent(prototypeVm);
        }

        /// <summary>
        /// Replace the current entry structure with a prototype instance
        /// </summary>
        public void ReplaceWithPrototype(string prototypeName)
        {
            // Create new instance
            var prototype = Entry.GetPrototype(prototypeName).Instantiate();
            // Update entry
            Entry.Value = prototype.Value;
            Entry.SubEntries = prototype.SubEntries;
            // Update our observable collection
            SubEntries = new ObservableWrapperCollection(Entry.SubEntries);

            UpdateParent();
        }

        ///
        public ObservableCollection<string> PossibleValues
        {
            get
            {
                var possibleValues = Entry?.Value.Possible;
                return possibleValues != null ? new ObservableCollection<string>(possibleValues) : null;
            }
        }

        private ObservableCollection<EntryViewModel> _subEntries;

        ///
        public ObservableCollection<EntryViewModel> SubEntries
        {
            get { return _subEntries; }
            set
            {
                _subEntries = value;
                OnPropertyChanged();
            }
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    internal class ObservableWrapperCollection : ObservableCollection<EntryViewModel>
    {
        private readonly IList<Entry> _entries;

        public ObservableWrapperCollection(IList<Entry> entries)
        {
            _entries = entries;
            // Copy current state to our collection
            for (var i = 0; i < entries.Count; i++)
            {
                base.InsertItem(i, new EntryViewModel(entries[i]));
            }
        }

        ///
        protected override void InsertItem(int index, EntryViewModel item)
        {
            _entries.Add(item.Entry);
            base.InsertItem(index, item);
        }

        ///
        protected override void RemoveItem(int index)
        {
            _entries.Remove(this.ElementAt(index).Entry);
            base.RemoveItem(index);
        }
    }
}
