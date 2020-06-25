// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;

namespace Moryx.ClientFramework.Tests.Playground
{
    /// <summary>
    /// Interaction logic for NotifyAndEditorWorkspaceView.xaml
    /// </summary>
    public partial class PlaygroundWorkspaceView
    {
        public PlaygroundWorkspaceView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            //InfoBox.BaseBrush = new SolidColorBrush(Colors.Red);
        }
    }
}
