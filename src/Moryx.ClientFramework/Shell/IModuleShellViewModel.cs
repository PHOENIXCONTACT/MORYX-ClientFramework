// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Moryx.Modules.Client;

namespace Moryx.ClientFramework.Shell
{
    public interface IModuleShellViewModel : IConductor, IScreen
    {
        void Initialize(IModuleShell shell);
    }
}
