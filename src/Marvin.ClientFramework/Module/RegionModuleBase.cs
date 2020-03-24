// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Abstract base class for a region module
    /// </summary>
    public abstract class RegionModuleBase<TConf> : ClientModuleBase<TConf>, IRegionModule 
        where TConf : class, IClientModuleConfig, new()
    {
        internal sealed override void AdditionalInitialize()
        {
        }
    }
}
