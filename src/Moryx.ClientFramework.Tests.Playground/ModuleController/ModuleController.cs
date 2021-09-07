// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading.Tasks;
using System.Windows.Media;
using Moryx.ClientFramework.Tests.Playground.Properties;
using Moryx.WpfToolkit;

namespace Moryx.ClientFramework.Tests.Playground
{
    [ClientModule("Playground")]
    [ClassDisplay(Name = nameof(Strings.ModuleController_Playground), Description = "", ResourceType = typeof(Strings))]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        public override Geometry Icon => MdiShapeFactory.GetShapeGeometry(MdiShapeType.Weather_Cloudy);

        protected override Task OnInitializeAsync()
        {
            return Task.CompletedTask;
        }

        protected override Task OnActivateAsync()
        {
            return Task.CompletedTask;
        }

        protected override Task OnDeactivateAsync(bool close)
        {
            return Task.CompletedTask;
        }

        protected override IModuleWorkspace OnCreateWorkspace()
        {
            return Container.Resolve<IModuleWorkspace>(PlaygroundWorkspaceViewModel.WorkspaceName);
        }

        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {
        }
    }
}
