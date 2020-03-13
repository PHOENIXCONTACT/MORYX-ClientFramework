// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel;
using System.Windows;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Root visual window in which the whole application runs
    /// </summary>
    public class RootVisual : Window
    {
        private readonly WindowConfig _windowConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="RootVisual"/> class.
        /// </summary>
        /// <param name="windowConfig">The window configuration.</param>
        public RootVisual(WindowConfig windowConfig)
        {
            _windowConfig = windowConfig;

            HorizontalAlignment = HorizontalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;

            SetLayout();
            Closing += OnClosing;
        }

        private void OnClosing(object sender, CancelEventArgs cancelEventArgs)
        {
            _windowConfig.SetCurrent(this);
        }

        private void SetLayout()
        {
            if (_windowConfig.Top > SystemParameters.VirtualScreenHeight ||
                _windowConfig.Left > SystemParameters.VirtualScreenWidth ||
                _windowConfig.Height > SystemParameters.VirtualScreenHeight)
                _windowConfig.Reset();

            Top = _windowConfig.Top;
            Left = _windowConfig.Left;
            Width = _windowConfig.Width;
            Height = _windowConfig.Height;
            WindowState = _windowConfig.State;
            WindowStartupLocation = _windowConfig.StartupLocation;
        }
    }
}
