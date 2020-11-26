// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Moryx.Serialization;
using Button = System.Windows.Controls.Button;

namespace Moryx.Controls
{
    /// <summary>
    /// Interaction logic for EntryEditor.xaml
    /// </summary>
    public partial class EntryEditor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntryEditor"/> class.
        /// </summary>
        public EntryEditor()
        {
            InitializeComponent();

            Path = new ObservableCollection<EntryViewModel>();
        }

        #region Dependency properties

        /// <summary>
        /// Root entry property
        /// </summary>
        public static readonly DependencyProperty RootEntryProperty = DependencyProperty.Register(
            "RootEntry", typeof(EntryViewModel), typeof(EntryEditor), new PropertyMetadata(null, RootEntryChanged));

        /// <summary>
        /// Root entry
        /// </summary>
        public EntryViewModel RootEntry
        {
            get => (EntryViewModel)GetValue(RootEntryProperty);
            set => SetValue(RootEntryProperty, value);
        }

        private static void RootEntryChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = (EntryEditor)sender;
            var root = (EntryViewModel) args.NewValue;

            control.Path.Clear();
            control.Path.Add(root);

            control.CurrentEntry = root;
        }

        /// <summary>
        /// Breadcrumb visibility
        /// </summary>
        public static readonly DependencyProperty BreadcrumbVisibilityProperty = DependencyProperty.Register(
            "BreadcrumbVisibility", typeof (Visibility), typeof (EntryEditor), new PropertyMetadata(Visibility.Visible));

        /// <summary>
        /// Indicates whether the breadcrumb is visible or not
        /// </summary>
        public Visibility BreadcrumbVisibility
        {
            get => (Visibility) GetValue(BreadcrumbVisibilityProperty);
            set => SetValue(BreadcrumbVisibilityProperty, value);
        }

        /// <summary>
        /// Edit mode property
        /// </summary>
        public static readonly DependencyProperty IsEditModeProperty = DependencyProperty.Register(
            "IsEditMode", typeof (bool), typeof (EntryEditor), new PropertyMetadata(default(bool)));

        /// <summary>
        /// Indicates whether the editor is in edit mode or not
        /// </summary>
        public bool IsEditMode
        {
            get => (bool) GetValue(IsEditModeProperty);
            set => SetValue(IsEditModeProperty, value);
        }

        /// <summary>
        /// Current entry property
        /// </summary>
        public static readonly DependencyProperty CurrentEntryProperty = DependencyProperty.Register(
            "CurrentEntry", typeof(EntryViewModel), typeof(EntryEditor), new PropertyMetadata(null, CurrentEntryChanged));

        private static void CurrentEntryChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var control = sender as EntryEditor;
            control?.OnCurrentEntryChanged();
        }

        private void OnCurrentEntryChanged()
        {
            if (CurrentEntry?.PossibleValues != null)
            {
                var desiredType = CurrentEntry.PossibleValues.FirstOrDefault(p => p == CurrentEntry.Value) ??
                                  CurrentEntry.PossibleValues.FirstOrDefault();
                DesiredType = desiredType;
            }

            CalculateCanForward();
            CalculateCanBack();
        }

        /// <summary>
        /// Current entry
        /// </summary>
        public EntryViewModel CurrentEntry
        {
            get => (EntryViewModel)GetValue(CurrentEntryProperty);
            set => SetValue(CurrentEntryProperty, value);
        }

        /// <summary>
        /// ViewExceptionDetailsCommand property
        /// </summary>
        public static readonly DependencyProperty ShowExceptionCommandProperty = DependencyProperty.Register(
            "ShowExceptionCommand", typeof(ICommand), typeof(EntryEditor), new PropertyMetadata(null));

        /// <summary>
        /// Command that is called when user clicks on details button on an exception entry
        /// </summary>
        public ICommand ShowExceptionCommand
        {
            get => (ICommand)GetValue(ShowExceptionCommandProperty);
            set => SetValue(ShowExceptionCommandProperty, value);
        }

        /// <summary>
        /// Desired type property
        /// </summary>
        public static readonly DependencyProperty DesiredTypeProperty = DependencyProperty.Register(
            "DesiredType", typeof(string), typeof(EntryEditor), new PropertyMetadata(null));

        /// <summary>
        /// Desired type
        /// </summary>
        public string DesiredType
        {
            get => (string)GetValue(DesiredTypeProperty);
            set => SetValue(DesiredTypeProperty, value);
        }

        private static readonly DependencyPropertyKey PathPropertyKey = DependencyProperty.RegisterReadOnly("Path", typeof(ObservableCollection<EntryViewModel>), typeof(EntryEditor),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// Path property
        /// </summary>
        public static readonly DependencyProperty PathProperty = PathPropertyKey.DependencyProperty;

        /// <summary>
        /// Path
        /// </summary>
        public ObservableCollection<EntryViewModel> Path
        {
            get => (ObservableCollection<EntryViewModel>)GetValue(PathProperty);
            protected set => SetValue(PathPropertyKey, value);
        }

        private static readonly DependencyPropertyKey CanForwardPropertyKey = DependencyProperty.RegisterReadOnly("CanForward", typeof(bool), typeof(EntryEditor),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// CanForward property
        /// </summary>
        public static readonly DependencyProperty CanForwardProperty = CanForwardPropertyKey.DependencyProperty;

        /// <summary>
        /// Indicates whether the user can go forward
        /// </summary>
        public bool CanForward
        {
            get => (bool)GetValue(CanForwardProperty);
            protected set => SetValue(CanForwardPropertyKey, value);
        }

        private static readonly DependencyPropertyKey CanBackwardPropertyKey = DependencyProperty.RegisterReadOnly("CanBackward", typeof(bool), typeof(EntryEditor),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.None));

        /// <summary>
        /// CanBackward property
        /// </summary>
        public static readonly DependencyProperty CanBackwardProperty = CanBackwardPropertyKey.DependencyProperty;

        /// <summary>
        /// Indicates whether the user can go backward
        /// </summary>
        public bool CanBackward
        {
            get => (bool)GetValue(CanBackwardProperty);
            protected set => SetValue(CanBackwardPropertyKey, value);
        }

        #endregion

        private void CalculateCanForward()
        {
            CanForward = Path.IndexOf(CurrentEntry) < Path.Count - 1;
        }

        private void CalculateCanBack()
        {
            CanBackward = Path.IndexOf(CurrentEntry) > 0;
        }

        private void DelveIntoSubEntry(object sender, RoutedEventArgs e)
        {
            var entry = GetEntry(sender);
            // If already in there just move on
            if (Path.Contains(entry))
            {
                CurrentEntry = entry;
                return;
            }

            // Remove everything from current to end
            var index = Path.IndexOf(CurrentEntry);
            while (index < Path.Count - 1)
                Path.RemoveAt(index + 1);
            Path.Add(entry);
            CurrentEntry = entry;
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog {Filter = "All Files (*.*)|*.*"};

            if (dlg.ShowDialog() == true)
            {
                var targetEntry = GetEntry(sender);
                targetEntry.Value = dlg.FileName;
            }
        }

        private void SelectDirectory(object sender, RoutedEventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var targetEntry = GetEntry(sender);
                targetEntry.Value = dialog.SelectedPath;
            }
        }

        private void SelectStreamContent(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { Filter = "All Files (*.*)|*.*" };

            if (dlg.ShowDialog() == true)
            {
                var targetEntry = GetEntry(sender);
                targetEntry.Value = Convert.ToBase64String(File.ReadAllBytes(dlg.FileName));
            }
        }

        private void SaveStreamContent(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog { Filter = "All Files (*.*)|*.*" };

            if (dlg.ShowDialog() == true)
            {
                var targetEntry = GetEntry(sender);
                var base64String = targetEntry.Value ?? "";

                File.WriteAllBytes(dlg.FileName, Convert.FromBase64String(base64String));
            }
        }

        private void RemoveCollectionEntry(object sender, RoutedEventArgs e)
        {
            CurrentEntry.SubEntries.Remove(GetEntry(sender));
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            var index = Path.IndexOf(CurrentEntry);
            CurrentEntry = Path.ElementAt(--index);
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            var index = Path.IndexOf(CurrentEntry);
            CurrentEntry = Path.ElementAt(++index);
        }

        private void ItemRequested(object sender, RoutedEventArgs e)
        {
            if (CurrentEntry.ValueType == EntryValueType.Collection)
            {
                CurrentEntry.AddPrototype(DesiredType);
            }
            else
            {
                CurrentEntry.ReplaceWithPrototype(DesiredType);
            }
        }

        private static EntryViewModel GetEntry(object eventSender)
        {
            // Find sending entry
            var button = (Button)eventSender;
            return (EntryViewModel)button.DataContext;
        }
    }
}
