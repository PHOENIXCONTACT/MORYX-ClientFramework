using Marvin.ClientFramework.Configurator.Properties;

namespace Marvin.ClientFramework.Configurator
{
    [ConfigViewModelPlugin]
    internal class ProxyConfigViewModel : ConfigViewModelBase<ProxyConfig>
    {
        public override string DisplayName => strings.ProxyConfig_ShortTitle;

        public override string ImageSource => "/Controls4Industry;component/Images/gear.png";
    }
}