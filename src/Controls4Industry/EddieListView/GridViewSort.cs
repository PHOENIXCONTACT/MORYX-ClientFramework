// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace C4I
{
    /// <summary>
    /// Implements a grid view sort option in the listview
    /// </summary>
    public class GridViewSort
    {
        #region Public attached properties

        /// <summary>
        /// Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command",
            typeof (ICommand), typeof (GridViewSort), new UIPropertyMetadata(null, CommandPropertyChanged));

        /// <summary>
        /// Gets command
        /// </summary>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// Sets command
        /// </summary>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        private static void CommandPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var listView = o as ItemsControl;
            if (listView == null) 
                return;

            if (GetAutoSort(listView)) 
                return;

            if (e.OldValue != null && e.NewValue == null)
            {
                listView.RemoveHandler(ButtonBase.ClickEvent,
                    new RoutedEventHandler(ColumnHeader_Click));
            }

            if (e.OldValue == null && e.NewValue != null)
            {
                listView.AddHandler(ButtonBase.ClickEvent,
                    new RoutedEventHandler(ColumnHeader_Click));
            }
        }

        /// <summary>
        /// Gets auto sort
        /// </summary>
        public static bool GetAutoSort(DependencyObject obj)
        {
            return (bool) obj.GetValue(AutoSortProperty);
        }

        /// <summary>
        /// Sets auto sort
        /// </summary>
        public static void SetAutoSort(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoSortProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for AutoSort.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty AutoSortProperty =
            DependencyProperty.RegisterAttached("AutoSort", typeof (bool), typeof (GridViewSort), new UIPropertyMetadata(false, AutoSortPropertyChanged));

        private static void AutoSortPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var listView = o as ListView;
            if (listView == null) 
                return;

            listView.IsVisibleChanged += listView_IsVisibleChanged;

            // Don't change click handler if a command is set
            if (GetCommand(listView) != null) 
                return;

            var oldValue = (bool)e.OldValue;
            var newValue = (bool)e.NewValue;
            if (oldValue && !newValue)
            {
                listView.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }
            
            if (!oldValue && newValue)
            {
                listView.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnHeader_Click));
            }
        }

        /// <summary>
        /// Whenever the visibility of the ListView changes, we need to add / remove the adorner. Otherwise the adorner would still be
        /// visible when the view changes. 
        /// </summary>
        private static void listView_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var listView = sender as ListView;
            var currentSortedColumnHeader = GetSortedColumnHeader(listView);

            if (currentSortedColumnHeader == null || listView == null) 
                return;
            
            if (listView.IsVisible)
            {
                var view = listView.Items;
                var sort = view.SortDescriptions[0].Direction == ListSortDirection.Ascending
                    ? GetSortGlyphAscending(listView)
                    : GetSortGlyphDescending(listView);

                AddSortGlyph(currentSortedColumnHeader, view.SortDescriptions[0].Direction, sort);
            }
            else
            {
                RemoveSortGlyph(currentSortedColumnHeader);
            }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for PropertyName.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty PropertyNameProperty =
            DependencyProperty.RegisterAttached("PropertyName", typeof (string), typeof (GridViewSort), new UIPropertyMetadata(null));

        /// <summary>
        /// Gets property name
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetPropertyName(DependencyObject obj)
        {
            return (string)obj.GetValue(PropertyNameProperty);
        }

        /// <summary>
        /// Sets property name
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetPropertyName(DependencyObject obj, string value)
        {
            obj.SetValue(PropertyNameProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for ShowSortGlyph.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty ShowSortGlyphProperty =
            DependencyProperty.RegisterAttached("ShowSortGlyph", typeof (bool), typeof (GridViewSort),
                new UIPropertyMetadata(true));

        /// <summary>
        /// Gets show sort glyph
        /// </summary>
        public static bool GetShowSortGlyph(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowSortGlyphProperty);
        }

        /// <summary>
        /// Sets show sort glyph
        /// </summary>
        public static void SetShowSortGlyph(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowSortGlyphProperty, value);
        }
        
        /// <summary>
        /// Using a DependencyProperty as the backing store for SortGlyphAscending.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SortGlyphAscendingProperty =
            DependencyProperty.RegisterAttached("SortGlyphAscending", typeof (ImageSource), typeof (GridViewSort),
                new UIPropertyMetadata(null));

        /// <summary>
        /// Gets sort glyph ascending
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ImageSource GetSortGlyphAscending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphAscendingProperty);
        }

        /// <summary>
        /// Sets sort glyph ascending
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSortGlyphAscending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphAscendingProperty, value);
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for SortGlyphDescending.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SortGlyphDescendingProperty =
            DependencyProperty.RegisterAttached("SortGlyphDescending", typeof (ImageSource), typeof (GridViewSort),
                new UIPropertyMetadata(null));

        /// <summary>
        /// Gets sort glyph descending
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ImageSource GetSortGlyphDescending(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(SortGlyphDescendingProperty);
        }

        /// <summary>
        /// Sets sort glyph descending
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetSortGlyphDescending(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(SortGlyphDescendingProperty, value);
        }

        #endregion

        #region Private attached properties

        /// <summary>
        /// Using a DependencyProperty as the backing store for SortedColumn.  This enables animation, styling, binding, etc...
        /// </summary>
        private static readonly DependencyProperty SortedColumnHeaderProperty =
            DependencyProperty.RegisterAttached("SortedColumnHeader", typeof (GridViewColumnHeader), typeof (GridViewSort), new UIPropertyMetadata(null));

        private static GridViewColumnHeader GetSortedColumnHeader(DependencyObject obj)
        {
            return (GridViewColumnHeader)obj.GetValue(SortedColumnHeaderProperty);
        }

        private static void SetSortedColumnHeader(DependencyObject obj, GridViewColumnHeader value)
        {
            obj.SetValue(SortedColumnHeaderProperty, value);
        }

        #endregion

        #region Column header click event handler

        private static void ColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked?.Column == null) 
                return;
            
            var propertyName = GetPropertyName(headerClicked.Column);
            if (string.IsNullOrEmpty(propertyName)) 
                return;
                
            var listView = GetAncestor<ListView>(headerClicked);
            if (listView == null) 
                return;
                    
            var command = GetCommand(listView);
            if (command != null)
            {
                if (command.CanExecute(propertyName))
                {
                    command.Execute(propertyName);
                }
            }
            else if (GetAutoSort(listView))
            {
                ApplySort(listView.Items, propertyName, listView, headerClicked);
            }
        }

        #endregion

        #region Helper methods

        private static T GetAncestor<T>(DependencyObject reference) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(reference);
            while (parent != null && !(parent is T))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (T) parent;
        }

        private static void ApplySort(ICollectionView view, string propertyName, ListView listView, GridViewColumnHeader sortedColumnHeader)
        {
            var direction = ListSortDirection.Ascending;
            if (view.SortDescriptions.Count > 0)
            {
                var currentSort = view.SortDescriptions[0];
                if (currentSort.PropertyName == propertyName)
                {
                    direction = currentSort.Direction == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
                }
                view.SortDescriptions.Clear();

                var currentSortedColumnHeader = GetSortedColumnHeader(listView);
                if (currentSortedColumnHeader != null)
                {
                    RemoveSortGlyph(currentSortedColumnHeader);
                }
            }
            
            if (string.IsNullOrEmpty(propertyName)) 
                return;

            view.SortDescriptions.Add(new SortDescription(propertyName, direction));
            if (GetShowSortGlyph(listView))
            {
                var sort = direction == ListSortDirection.Ascending
                    ? GetSortGlyphAscending(listView)
                    : GetSortGlyphDescending(listView);

                AddSortGlyph(sortedColumnHeader, direction, sort);
            }
            SetSortedColumnHeader(listView, sortedColumnHeader);
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

        #endregion
    }
}
