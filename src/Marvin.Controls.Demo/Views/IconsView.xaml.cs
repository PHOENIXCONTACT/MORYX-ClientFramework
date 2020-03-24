// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Marvin.Controls.Demo.ViewModels;

namespace Marvin.Controls.Demo.Views
{
    /// <summary>
    /// Interaction logic for IconsView.xaml
    /// </summary>
    public partial class IconsView
    {
        private CollectionViewSource _commonViewSource;
        private CollectionViewSource _mdiViewSource;

        public IconsView()
        {
            InitializeComponent();

            _commonViewSource = (CollectionViewSource)Resources["CommonItems"];
            _commonViewSource.Filter += OnCommonFilter;

            _mdiViewSource = (CollectionViewSource)Resources["MdiItems"];
            _mdiViewSource.Filter += OnMdiFilter;

            CommonShapeSearch.KeyDown += CommonShapeSearchOnKeyDown;
            MdiShapeSearch.KeyDown += MdiShapeSearchOnKeyDown;
        }

        private void CommonShapeSearchOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                _commonViewSource.View.Refresh();
        }

        private void MdiShapeSearchOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                _mdiViewSource.View.Refresh();
        }

        private void OnMdiFilter(object sender, FilterEventArgs filterEventArgs)
        {
            var shapeWrapper = (IconWrapper)filterEventArgs.Item;

            // Filter by TextBox
            if (!string.IsNullOrEmpty(MdiShapeSearch.Text) && !shapeWrapper.Name.ToLower().Contains(MdiShapeSearch.Text.ToLower()))
            {
                filterEventArgs.Accepted = false;
                return;
            }

            // Allow
            filterEventArgs.Accepted = true;
        }

        private void OnCommonFilter(object sender, FilterEventArgs filterEventArgs)
        {
            var shapeWrapper = (IconWrapper)filterEventArgs.Item;

            // Filter by TextBox
            if (!string.IsNullOrEmpty(CommonShapeSearch.Text) && !shapeWrapper.Name.ToLower().Contains(CommonShapeSearch.Text.ToLower()))
            {
                filterEventArgs.Accepted = false;
                return;
            }

            // Allow
            filterEventArgs.Accepted = true;
        }
    }
}
