using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Marvin.Container;

namespace Marvin.ClientFramework.Kernel
{
    [KernelComponent(typeof(ILoaderHandler))]
    public class LoaderHandler : ILoaderHandler
    {
        public ILoaderView View { get; private set; }
        public IKernelConfigManager ConfigManager { get; set; }
        public IEnumerable<ILoaderAdapter> LoaderAdapter { get; set; }

        public void Initialize()
        {
            var appConfig = ConfigManager.GetConfiguration<AppConfig>();

            View = new LoaderView
            {
                Maximum = 0,
                Value = 0,
                AppnName = appConfig.Name
            };

            foreach (var adapter in LoaderAdapter)
            {
                adapter.AddToMax += OnAddToMax;
                adapter.ChangeMessage += OnChangeMessage;
                adapter.ChangeValueWithMessage += OnChangeValueWithMessage;
                adapter.IndicateError += OnIndicateError;
            }
        }

        public void CheckForAdapter(object sender, object instance)
        {
            var adapter = LoaderAdapter.FirstOrDefault(a => a.CanAdapt(instance));

            adapter?.Adapt(instance);
        }

        private void OnIndicateError(object sender, ClientException clientException)
        {
            Execute.OnUIThread(() => View.IndicateError());
        }

        private void OnChangeValueWithMessage(object sender, string message)
        {
            Execute.OnUIThread(delegate
            {
                View.StatusMessage = message;
                View.Value += 1;
            });
        }

        private void OnChangeMessage(object sender, string message)
        {
            Execute.OnUIThread(delegate
            {
                View.StatusMessage = message;
            });
        }

        private void OnAddToMax(object sender, int message)
        {
            Execute.OnUIThread(delegate
            {
                View.Maximum += message;
            });
        }
    }
}