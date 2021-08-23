// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Moryx.ClientFramework;
using Moryx.Container;
using Moryx.Tools.Wcf;

namespace Moryx.Tools.WcfClient.UI.Viewer
{
    [Plugin(LifeCycle.Singleton, typeof(IModuleWorkspace), Name = nameof(WcfClientViewerWorkspaceViewModel))]
    internal class WcfClientViewerWorkspaceViewModel : ModuleWorkspace
    {
        #region Dependency Injection

        public IWcfClientFactory ClientFactory { get; set; }

        #endregion

        private ObservableCollection<WcfClientInfoViewModel> _clients;
        public ObservableCollection<WcfClientInfoViewModel> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                NotifyOfPropertyChange(() => Clients);
            }
        }

        /// <inheritdoc />
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            ClientFactory.ClientInfoChanged += OnClientInfoChanged;
            Clients = new ObservableCollection<WcfClientInfoViewModel>();

            foreach (var client in ClientFactory.ClientInfos)
                Clients.Add(new WcfClientInfoViewModel(client));

            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when [client information changed].
        /// </summary>
        private void OnClientInfoChanged(object sender, WcfClientInfo client)
        {
            Execute.OnUIThread(() => UpdateClientInfo(client));
        }

        /// <summary>
        /// Updates the client information.
        /// </summary>
        private void UpdateClientInfo(WcfClientInfo clientInfo)
        {
            var clientVm = Clients.FirstOrDefault(c => c.Model == clientInfo);
            if (clientVm != null)
                clientVm.UpdateModel(clientInfo);
            else
                Clients.Add(new WcfClientInfoViewModel(clientInfo));
        }
    }
}
