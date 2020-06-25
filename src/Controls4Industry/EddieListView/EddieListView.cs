using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace C4I
{

    public class EddieListView : ListView
    {

        static EddieListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieListView), new FrameworkPropertyMetadata(typeof(EddieListView)));


        }

        public EddieListView()
        {

            AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(GridViewColumnHeaderClickedHandler));
        }


        ///
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is EddieListViewItem;
        }

        ///
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new EddieListViewItem();
        }

        /// 
        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
            base.OnManipulationBoundaryFeedback(e);
        }


        #region sorting
        private CustomSorter _customSorter = new CustomSorter();
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader columnHeader = e.OriginalSource as GridViewColumnHeader;
            if (columnHeader == null)
                return;
            Sort(columnHeader);
        }

        private GridViewColumnHeader sortedColumnHeader;
        private void Sort(GridViewColumnHeader columnHeader)
        {
            Binding binding = columnHeader.Column.DisplayMemberBinding as Binding;
            if (binding != null && ItemsSource != null)
            {
                ICollectionView dataView = CollectionViewSource.GetDefaultView(ItemsSource);
                ListCollectionView view = dataView as ListCollectionView;
                _customSorter.SortPropertyName = binding.Path.Path;
                view.CustomSort = _customSorter;



                var direction = ListSortDirection.Ascending;
                if (Items.SortDescriptions.Count > 0)
                {
                    var currentSort = Items.SortDescriptions[0];
                    if (currentSort.PropertyName == _customSorter.SortPropertyName)
                    {
                        direction = currentSort.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                    }
                    Items.SortDescriptions.Clear();

                    RemoveSortGlyph(columnHeader);

                }


                Items.SortDescriptions.Add(new SortDescription(_customSorter.SortPropertyName, direction));


                AddSortGlyph(columnHeader, direction, null);
                sortedColumnHeader = columnHeader;
            }


        }


        private static void AddSortGlyph(GridViewColumnHeader columnHeader, ListSortDirection direction, ImageSource sortGlyph)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            adornerLayer.Add(new SortGlyphAdorner(columnHeader, direction, sortGlyph));
        }

        private static void RemoveSortGlyph(GridViewColumnHeader columnHeader)
        {
            var adornerLayer = AdornerLayer.GetAdornerLayer(columnHeader);
            var adorners = adornerLayer.GetAdorners(columnHeader);
            if (adorners == null)
                return;

            foreach (var adorner in adorners.OfType<SortGlyphAdorner>())
            {
                adornerLayer.Remove(adorner);
            }
        }




        public class CustomSorter : IComparer
        {
            private Dictionary<string, ListSortDirection> _dictOfSortDirections =
                new Dictionary<string, ListSortDirection>();

            private string _sortPropertyName;
            public string SortPropertyName
            {
                get { return _sortPropertyName; }
                set
                {
                    _sortPropertyName = value;
                    if (!_dictOfSortDirections.ContainsKey(_sortPropertyName))
                    {
                        _dictOfSortDirections.Add(_sortPropertyName, ListSortDirection.Ascending);
                    }
                    // Alternate sort directions inside the dictionary
                    _dictOfSortDirections[_sortPropertyName] =
                        (_dictOfSortDirections[_sortPropertyName] == ListSortDirection.Ascending) ?
                            ListSortDirection.Descending : ListSortDirection.Ascending;
                }
            }

            public int Compare(object x, object y)
            {
                PropertyInfo pi = x.GetType().GetProperty(_sortPropertyName);
                if (pi != null)
                {
                    object value1 = pi.GetValue(x);
                    object value2 = pi.GetValue(y);

                    bool valuesAreNotSortable = (!(value1 is IComparable) || !(value2 is IComparable));
                    if (valuesAreNotSortable)
                        return 0;

                    ListSortDirection dir = _dictOfSortDirections[_sortPropertyName];

                    if (dir == ListSortDirection.Ascending)
                        return ((IComparable)value1).CompareTo(value2);
                    else
                        return ((IComparable)value2).CompareTo(value1);
                }
                return 0;
            }
        }
        #endregion


    }
}
