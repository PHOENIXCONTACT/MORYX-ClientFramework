// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace C4I
{
    /// <inheritdoc />
    /// <summary>
    /// ListView implementation with additional features like sorting
    /// </summary>
    public class EddieListView : ListView
    {
        static EddieListView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EddieListView), new FrameworkPropertyMetadata(typeof(EddieListView)));
        }

        /// <inheritdoc />
        public EddieListView()
        {
            AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(GridViewColumnHeaderClickedHandler));
        }

        /// <inheritdoc />
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is EddieListViewItem;
        }

        /// <inheritdoc />
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new EddieListViewItem();
        }

        /// <inheritdoc />
        protected override void OnManipulationBoundaryFeedback(ManipulationBoundaryFeedbackEventArgs e)
        {
            e.Handled = true;
            base.OnManipulationBoundaryFeedback(e);
        }

        #region sorting

        private readonly CustomSorter _customSorter = new CustomSorter();

        private void GridViewColumnHeaderClickedHandler(object sender, RoutedEventArgs e)
        {
            var columnHeader = e.OriginalSource as GridViewColumnHeader;
            if (columnHeader == null)
                return;

            Sort(columnHeader);
        }

        private void Sort(GridViewColumnHeader columnHeader)
        {
            var binding = columnHeader.Column.DisplayMemberBinding as Binding;
            if (binding == null || ItemsSource == null)
                return;

            var dataView = CollectionViewSource.GetDefaultView(ItemsSource);
            var view = (ListCollectionView)dataView;
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

        internal class CustomSorter : IComparer
        {
            private readonly Dictionary<string, ListSortDirection> _dictOfSortDirections =
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
                        _dictOfSortDirections[_sortPropertyName] == ListSortDirection.Ascending ?
                            ListSortDirection.Descending : ListSortDirection.Ascending;
                }
            }

            public int Compare(object x, object y)
            {
                var pi = x?.GetType().GetProperty(_sortPropertyName);
                if (pi == null)
                    return 0;

                var value1 = pi.GetValue(x);
                var value2 = pi.GetValue(y);

                var valuesAreNotSortable = (!(value1 is IComparable) || !(value2 is IComparable));
                if (valuesAreNotSortable)
                    return 0;

                var dir = _dictOfSortDirections[_sortPropertyName];

                return dir == ListSortDirection.Ascending
                    ? ((IComparable) value1).CompareTo(value2)
                    : ((IComparable) value2).CompareTo(value1);
            }
        }

        #endregion
    }
}
