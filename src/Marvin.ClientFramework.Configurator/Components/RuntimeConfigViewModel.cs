namespace Marvin.ClientFramework.Configurator
{
    [ConfigViewModelPlugin]
    internal class RuntimeConfigViewModel : ConfigViewModelBase<RuntimeConfig>
    {
        public override string DisplayName
        {
            get { return "Runtime Config"; }
        }

        public override string ImageSource
        {
            get { return "/Controls4Industry;component/Images/stairs.png"; }
        }
    }
}