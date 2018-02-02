using System;
using System.Collections;
using Caliburn.Micro;
using Marvin.ClientFramework.Shell;

namespace Marvin.ClientFramework.Kernel
{
    internal class FallbackShellViewModel : Screen, IModuleShell
    {
        public const string ShellName = "FallbackShell";

        public string ConfiguredShell { get; set; }

        public void Initialize()
        {
        }

        public void ActivateItem(object item)
        {
        }

        public void DeactivateItem(object item, bool close)
        {
        }

        public IEnumerable GetChildren()
        {
            return new ArrayList();
        }

        public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;
    }
}
