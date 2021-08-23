// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;
using Moryx.ClientFramework.Shell;

namespace Moryx.ClientFramework.Kernel
{
    internal class FallbackShellViewModel : Screen, IModuleShell
    {
        public const string ShellName = "FallbackShell";

        public string RunMode { get; set; }

        public string ConfiguredShell { get; set; }

        public Task InitializeAsync()
        {
            return Task.CompletedTask;
        }

        public Task ActivateItemAsync(object item, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public Task DeactivateItemAsync(object item, bool close, CancellationToken cancellationToken = new CancellationToken())
        {
            return Task.CompletedTask;
        }

        public IEnumerable GetChildren()
        {
            return new ArrayList();
        }

#pragma warning disable 67


        public event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;
#pragma warning restore 67
    }
}
