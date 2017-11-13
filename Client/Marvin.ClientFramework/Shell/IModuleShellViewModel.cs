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