using System.Windows.Media;
using Caliburn.Micro;
using Marvin.Tools.Wcf;

namespace Marvin.Tools.WcfClient.UI.Viewer
{
    internal class WcfClientInfoViewModel : PropertyChangedBase
    {
        private readonly WcfClientInfo _source;

        public WcfClientInfoViewModel(WcfClientInfo source)
        {
            _source = source;
        }

        public WcfClientInfo Source
        {
            get { return _source; }
        }

        public string Service
        {
            get { return _source.Service; }
        }

        public string Uri
        {
            get { return _source.Uri; }
        }

        public string ServerVersion
        {
            get { return _source.ServerVersion; }
        }

        public string ClientVersion
        {
            get { return _source.ClientVersion; }
        }

        public string MinServerVersion
        {
            get { return _source.MinServerVersion; }
        }

        public string MinClientVersion
        {
            get { return _source.MinClientVersion; }
        }

        public ConnectionState State
        {
            get { return _source.State; }
        }

        public SolidColorBrush StateBrush
        {
            get
            {
                switch (_source.State)
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

        public int Tries
        {
            get { return _source.Tries; }
        }
    }
}