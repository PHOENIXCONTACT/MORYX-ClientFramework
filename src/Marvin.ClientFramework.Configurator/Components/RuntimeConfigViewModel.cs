using Marvin.ClientFramework.Configurator.Properties;

namespace Marvin.ClientFramework.Configurator
{
    [ConfigViewModelPlugin]
    internal class RuntimeConfigViewModel : ConfigViewModelBase<RuntimeConfig>
    {
        public override string DisplayName => strings.RuntimeConfig_ShortTitle;

        public override string ImageSource => "/Controls4Industry;component/Images/stairs.png";
    }
}