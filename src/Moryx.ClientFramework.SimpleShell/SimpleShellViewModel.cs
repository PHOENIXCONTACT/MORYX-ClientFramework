// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moryx.ClientFramework.Shell;

namespace Moryx.ClientFramework.SimpleShell
{
    /// <summary>
    /// Simple shell for hosting client modules
    /// </summary>
    [ModuleShell("SimpleShell")]
    public class SimpleShellViewModel : ShellViewModelBase
    {
        /// <inheritdoc />
        protected override async Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            await base.OnInitializeAsync(cancellationToken);
            if (Items.Any())
                SelectModule(Items.First());
        }

        /// <summary>
        /// Creates the controller.
        /// </summary>
        protected override IShellRegionController CreateController()
        {
            return new NullRegionController();
        }
    }
}
