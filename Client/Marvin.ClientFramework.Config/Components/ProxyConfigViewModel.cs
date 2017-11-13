namespace Marvin.ClientFramework.Config
{
    [ConfigViewModelPlugin]
    internal class ProxyConfigViewModel : ConfigViewModelBase<ProxyConfig>
    {
        public override string DisplayName
        {
            get { return "Proxy"; }
        }

        public override string ImageSource
        {
            get { return "/Controls4Industry;component/Images/gear.png"; }
        }
    }
}