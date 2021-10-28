// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Windows.Media;
using Caliburn.Micro;
using Moryx.Communication.Endpoints;
using Moryx.Tools.Wcf;

namespace Moryx.Tools.WcfClient.UI.Viewer
{
    internal class WcfClientInfoViewModel : PropertyChangedBase
    {
        public WcfClientInfo Model { get; private set; }

        public WcfClientInfoViewModel(WcfClientInfo source)
        {
            UpdateModel(source);
        }

        public void UpdateModel(WcfClientInfo clientInfo)
        {
            Model = clientInfo;

            if (Version.TryParse(Model.ServerVersion, out var serverVersion))
                MinClientVersion = $"{serverVersion.Major}.0.0";


            NotifyOfPropertyChange(nameof(Service));
            NotifyOfPropertyChange(nameof(Uri));
            NotifyOfPropertyChange(nameof(ServerVersion));
            NotifyOfPropertyChange(nameof(ClientVersion));
            NotifyOfPropertyChange(nameof(MinServerVersion));
            NotifyOfPropertyChange(nameof(MinClientVersion));
            NotifyOfPropertyChange(nameof(State));
            NotifyOfPropertyChange(nameof(Tries));
            NotifyOfPropertyChange(nameof(StateBrush));
        }

        public string Service => Model.Service;

        public string Uri => Model.Uri;

        public string ServerVersion => Model.ServerVersion;

        public string ClientVersion => Model.ClientVersion;

        public string MinServerVersion => Model.MinServerVersion;

        public string MinClientVersion { get; private set; }

        public ConnectionState State => Model.State;

        public int Tries => Model.Tries;

        public SolidColorBrush StateBrush
        {
            get
            {
                switch (Model.State)
                {
                    case ConnectionState.Success:
                        return Brushes.LightGreen;

                    case ConnectionState.FailedTry:
                    case ConnectionState.VersionMismatch:
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
