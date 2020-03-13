// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.ObjectModel;
using Marvin.Modules;

namespace Marvin.ClientFramework
{
    internal class ClientNotificationCollection : ObservableCollection<IModuleNotification>, INotificationCollection
    {
    }
}
