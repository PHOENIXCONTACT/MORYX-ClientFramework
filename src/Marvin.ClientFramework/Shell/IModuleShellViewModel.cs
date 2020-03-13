// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using Caliburn.Micro;
using Marvin.Modules.Client;

namespace Marvin.ClientFramework.Shell
{
    public interface IModuleShellViewModel : IConductor, IScreen
    {
        void Initialize(IModuleShell shell);
    }
}
