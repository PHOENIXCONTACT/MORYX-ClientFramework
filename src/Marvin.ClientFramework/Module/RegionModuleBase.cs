namespace Marvin.ClientFramework
{
    /// <summary>
    /// Abstract base class for a region module
    /// </summary>
    /// <typeparam name="TConf"></typeparam>
    public abstract class RegionModuleBase<TConf> : ClientModuleBase<TConf>, IRegionModule 
        where TConf : class, IClientModuleConfig, new()
    {
        internal sealed override void AdditionalInitialize()
        {
        }
    }
}