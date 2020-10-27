// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows.Media;
using Caliburn.Micro;
using Moryx.Tools.Wcf;

namespace Moryx.Tools.WcfClient.UI.Viewer
{
    internal class WcfClientInfoViewModel : PropertyChangedBase
    {
        public WcfClientInfoViewModel(WcfClientInfo source)
        {
            Source = source;

            if (Version.TryParse(Source.ServerVersion, out var serverVersion))
                MinClientVersion = $"{serverVersion.Major}.0.0";
        }

        public WcfClientInfo Source { get; }

        public string Service => Source.Service;

        public string Uri => Source.Uri;

        public string ServerVersion => Source.ServerVersion;

        public string ClientVersion => Source.ClientVersion;

        public string MinServerVersion => Source.MinServerVersion;

        public string MinClientVersion { get; }

        public ConnectionState State => Source.State;

        public int Tries => Source.Tries;

        public SolidColorBrush StateBrush
        {
            get
            {
                switch (Source.State)
                {
                    case ConnectionState.Success:
                        return Brushes.LightGreen;

                    case ConnectionState.FailedTry:
                    case ConnectionState.VersionMissmatch:
                    case ConnectionState.ConnectionLost:
                        return Brushes.LightCoral;

                    case ConnectionState.Closing:
                    case ConnectionState.Closed:
                        return Brushes.LightYellow;

                    default:
                        return Brushes.White;
                }
            }
        }
    }
}
