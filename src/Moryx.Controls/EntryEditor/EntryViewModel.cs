// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using Moryx.Serialization;
using Moryx.Tools;

namespace Moryx.Controls
{
    /// <summary>
    /// View model that represents a page in the config editor
    /// </summary>
    public class EntryViewModel : INotifyPropertyChanged, IEditableObject
    {

        #region Fields and Properties
        private ObservableCollection<EntryViewModel> _subEntries;
        private string _preEditValue;
        private ObservableCollection<EntryViewModel> _preEditSubEntries;

        /// <summary>
        /// The entry of this view model
        /// </summary>
        public Entry Entry { get; private set; }

        /// <summary>
        /// Parent view model
        /// </summary>
        public EntryViewModel Parent { get; set; }

        /// <summary>
        /// Displayed name of the <see cref="Entry"/>
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Type of the value of the <see cref="Entry"/>
        /// </summary>
        public EntryValueType ValueType { get; private set; }

        /// <summary>
        /// Type of the unit of the value of the <see cref="Entry"/>
        /// </summary>
        public EntryUnitType UnitType { get; private set; }

        /// <summary>
        /// Default value of the <see cref="Entry"/>
        /// </summary>
        public string DefaultValue => Entry.Value.Default;

        /// <summary>
        /// Description of the <see cref="Entry"/>
        /// </summary>
        public string Description => Entry.Description;

        /// <summary>
        /// Flag if this entry is readonly and text boxes shall be disabled
        /// </summary>
        public bool IsReadOnly => Entry.Value.IsReadOnly;

        // TODO: AL6 Remove direct access to Model (insert in CopyFrom/ToModel) and the helper variable _preEditValue from BeginEdit, CancelEdit and EndEdit methods
        /// <summary>
        /// Current value of <see cref="Entry"/>
        /// </summary>
        public string Value
        {
            get => Entry.Value.Current;
            set
            {
                if (Entry.Value.Current is not null && Entry.Value.Current.Equals(value))
                    return;
                Entry.Value.Current = value;
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    OnPropertyChanged();
                });
            }
        }

        /// <summary>
        /// Assignable values to the current <see cref="Entry"/>
        /// </summary>
        public ObservableCollection<string> PossibleValues
        {
            get
            {
                var possibleValues = Entry?.Value.Possible;
                return possibleValues != null ? new ObservableCollection<string>(possibleValues) : null;
            }
        }

        // TODO: AL6 Remove direct sync to Model (insert in CopyFrom/ToModel) and the helper variable _preEditSubEntries from BeginEdit, CancelEdit and EndEdit methods
        /// <summary>
        /// Child instances of this <see cref="EntryViewModel"/>
        /// </summary>
        public ObservableCollection<EntryViewModel> SubEntries
        {
            get => _subEntries;
            set
            {
                if (_subEntries is not null)
                    _subEntries.CollectionChanged -= SyncSubEntries;
                _subEntries = value;
                _subEntries.CollectionChanged += SyncSubEntries;
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    OnPropertyChanged();
                });
            }
        }

        private void SyncSubEntries(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action is NotifyCollectionChangedAction.Move)
                return;
            Entry.SubEntries = SubEntries.Select(vm => vm.Entry).ToList();
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryViewModel"/> class.
        /// </summary>
        public EntryViewModel() : this(new Entry() { DisplayName = "Root", 
            Value = new EntryValue() { Type = EntryValueType.Class }, SubEntries = new List<Entry>() })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryViewModel"/> class.
        /// </summary>
        [Obsolete("Use Empty constructor instead")]
        public EntryViewModel(IList<Entry> entries) : this()
        {
            UpdateModel(entries);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntryViewModel"/> class.
        /// </summary>
        public EntryViewModel(Entry entry)
        {
            Entry = entry;
            DisplayName = Entry.DisplayName;
            Value = Entry.Value.Current;
            ValueType = Entry.Value.Type;
            UnitType = Entry.Value.UnitType;
            SubEntries = new ObservableCollection<EntryViewModel>(Entry.SubEntries.Select(e => new EntryViewModel(e)));
            
            _preEditSubEntries = new ObservableCollection<EntryViewModel>(SubEntries);
            _preEditValue = Entry.Value.Current;

            UpdateParent();
        }
        #endregion

        private void UpdateParent()
        {
            foreach (var entryViewModel in SubEntries)
                UpdateParent(entryViewModel);
        }

        private void UpdateParent(EntryViewModel entry)
        {
            if (entry.Parent is null || !entry.Parent.Equals(this))
                entry.Parent = this;
        }

        /// <summary>
        /// Add prototype of this name to the subEntries
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
            Entry.Value.Type = prototype.Value.Type;
            Entry.Value.UnitType = prototype.Value.UnitType;
            // Update our observable collection
            SubEntries = new ObservableCollection<EntryViewModel>(Entry.SubEntries.Select(e => new EntryViewModel(e)));

            UpdateParent();
        }

        /// <inheritdoc />
        public void BeginEdit()
        {
            SubEntries.BeginEdit();
            _preEditValue = Entry.Value.Current;
            _preEditSubEntries = new ObservableCollection<EntryViewModel>(SubEntries);
        }

        /// <inheritdoc />
        public void EndEdit()
        {
            SubEntries.EndEdit();
            CopyToModel();
        }

        private void CopyToModel()
        {
            Entry.DisplayName = DisplayName;
            Entry.Value.Current = Value;
            Entry.Value.Type = ValueType;
            Entry.Value.UnitType = UnitType;

            UpdateParent();
        }

        /// <inheritdoc />
        public void CancelEdit()
        {
            Entry.SubEntries = _preEditSubEntries.Select(vm => vm.Entry).ToList();
            Value = _preEditValue;
            CopyFromModel();
            SubEntries.CancelEdit();
        }

        private void CopyFromModel()
        {
            DisplayName = Entry.DisplayName;
            UnitType = Entry.Value.UnitType;
            ValueType = Entry.Value.Type;
            MergeIntoViewModelCollection(SubEntries, Entry.SubEntries);

            UpdateParent();
        }

        private void MergeIntoViewModelCollection(ObservableCollection<EntryViewModel> entryViewModels, IList<Entry> updated)
        {
            // Remove those without identifiert or not existing in the updated collection
            var removed = entryViewModels.Where(vm => vm.Entry.Identifier == "" || updated.All(e => e != vm.Entry)).ToList();
            foreach (var obj in removed)
                entryViewModels.Remove(obj);

            foreach (var updatedEntry in updated)
            {
                var match = entryViewModels.FirstOrDefault(vm => vm.Entry == updatedEntry);
                if (match is null) // Add new Entry 
                    entryViewModels.Add(new EntryViewModel(updatedEntry));
                else // Update Entry
                    match.UpdateModel(updatedEntry);
            }
        }

        /// <summary>
        /// Updates the internal model
        /// </summary>
        /// <param name="entry">The updated entry instance</param>
        public void UpdateModel(Entry entry)
        {
            Entry = entry;
            CopyFromModel();
        }

        /// <summary>
        /// Updates the internal model
        /// </summary>
        /// <param name="entries">The updated subentry instances</param>
        public void UpdateModel(IList<Entry> entries)
        {
            MergeIntoViewModelCollection(SubEntries, entries);
            CopyToModel();
            UpdateParent();
        }


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
