// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Marvin.Container;

namespace Marvin.ClientFramework.Tests.HistoryWriter
{
    [PluginFactory]
    public interface IHistoryWorkspaceFactory
    {
        IModuleWorkspace CreateWorkspace(int level);

        void Destroy(IModuleWorkspace workspace);
    }
}
