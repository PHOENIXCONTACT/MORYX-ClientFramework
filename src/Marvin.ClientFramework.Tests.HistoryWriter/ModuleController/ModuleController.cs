// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows.Media;

namespace Marvin.ClientFramework.Tests.HistoryWriter
{
    [ClientModule("HistoryWriter")]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        public override Geometry Icon => Geometry.Parse("M353.985,203.905c35.145-20.995,47.049-66.379,26.508-101.954c-20.54-35.575-65.794-47.957-101.547-28.017" +
                                                        "C278.336,33.001,244.985,0,203.907,0c-41.079,0-74.431,33.001-75.039,73.937c-35.754-19.94-81.01-7.557-101.549,28.02" +
                                                        "C6.78,137.531,18.683,182.915,53.83,203.908c-35.146,20.995-47.05,66.379-26.51,101.954c20.54,35.576,65.796,47.958,101.55,28.017" +
                                                        "c0.607,40.936,33.96,73.938,75.039,73.936c41.08,0,74.43-33.002,75.038-73.936c35.754,19.939,81.011,7.557,101.548-28.021" +
                                                        "C401.035,270.282,389.13,224.898,353.985,203.905z M203.907,272.65c-37.966,0-68.744-30.777-68.744-68.744" +
                                                        "s30.778-68.744,68.744-68.744c37.965,0,68.744,30.777,68.744,68.744S241.873,272.65,203.907,272.65z");

        protected override void OnInitialize()
        {
            //Config.DisplayName = "History";
        }

        protected override void OnActivate()
        {
        }

        protected override void OnDeactivate(bool close)
        {
        }

        protected override IModuleWorkspace OnCreateWorkspace()
        {
            var fac = Container.Resolve<IHistoryWorkspaceFactory>();
            return fac.CreateWorkspace(1);
        }

        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {
        }
    }
}
