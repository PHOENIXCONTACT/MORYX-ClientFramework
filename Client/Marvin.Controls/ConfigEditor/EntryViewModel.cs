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
        public Entry Entry { get; }

        public EntryViewModel(IList<Entry> entries)
        {
            Key = "Root";
            ValueType = EntryValueType.Class;
            SubEntries = new ObservableWrapperCollection(entries);
        }

        public EntryViewModel(Entry entry)
        {
            Entry = entry;
            Key = Entry.Key.Name;
            ValueType = Entry.Value.Type;
            SubEntries = new ObservableWrapperCollection(entry.SubEntries);
        }

        ///
        public string Key { get; }

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