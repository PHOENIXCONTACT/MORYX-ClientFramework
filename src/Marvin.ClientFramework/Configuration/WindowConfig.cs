// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Runtime.Serialization;
using System.Windows;
using Marvin.Configuration;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Configuration for the main window of this application
    /// </summary>
    [DataContract]
    public class WindowConfig : ConfigBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowConfig"/> class.
        /// </summary>
        public WindowConfig()
        {
            Reset();
        }

        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        [DataMember]
        public double Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        [DataMember]
        public double Height { get; set; }

        /// <summary>
        /// Gets or sets the left position of the window.
        /// </summary>
        [DataMember]
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets the top position of the window.
        /// </summary>
        [DataMember]
        public double Top { get; set; }

        /// <summary>
        /// Gets or sets the state of the window.
        /// </summary>
        [DataMember]
        public WindowState State { get; set; }

        /// <summary>
        /// Gets or sets the startup location of the window.
        /// </summary>
        [DataMember]
        public WindowStartupLocation StartupLocation { get; set; }

        /// <summary>
        /// Resets this the window to framework defaults.
        /// </summary>
        public void Reset()
        {
            Top = 0;
            Left = 0;
            Width = SystemParameters.PrimaryScreenWidth - 100;
            Height = SystemParameters.PrimaryScreenHeight - 150;
            State = WindowState.Normal;
            StartupLocation = WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// Will adjust this configuration to the current window size
        /// </summary>
        public void SetCurrent(Window window)
        {
            Top = window.Top;
            Left = window.Left;
            Width = window.Width;
            Height = window.Height;
            State = window.WindowState;
            StartupLocation = WindowStartupLocation.Manual;
        }
    }
}
