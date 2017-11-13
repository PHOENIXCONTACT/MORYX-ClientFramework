namespace Marvin.ClientFramework
{
    public abstract class RegionModuleBase<TConf> : ClientModuleBase<TConf>, IRegionModule 
        where TConf : class, IClientModuleConfig, new()
    {
        internal sealed override void AdditionalInitialize()
        {
        }
    }
}