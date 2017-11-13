using System.Collections.ObjectModel;
using Marvin.Modules;

namespace Marvin.ClientFramework
{
    internal class ClientNotificationCollection : ObservableCollection<IModuleNotification>, INotificationCollection
    {
    }
}