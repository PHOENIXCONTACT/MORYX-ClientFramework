using System.Windows.Media;
using Caliburn.Micro;
using Marvin.Tools.Wcf;

namespace Marvin.Tools.WcfClient.UI.Viewer
{
    internal class WcfClientInfoViewModel : PropertyChangedBase
    {
        public WcfClientInfoViewModel(WcfClientInfo source)
        {
            Source = source;
        }

        public WcfClientInfo Source { get; }

        public string Service => Source.Service;

        public string Uri => Source.Uri;

        public string ServerVersion => Source.ServerVersion;

        public string ClientVersion => Source.ClientVersion;

        public string MinServerVersion => Source.MinServerVersion;

        public string MinClientVersion => Source.MinClientVersion;

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