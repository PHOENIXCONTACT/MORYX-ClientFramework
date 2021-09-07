// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Moryx.ClientFramework.Tests.Playground
{
    public class BigDialogViewModel : Screen
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            DisplayName = "Hello World";
            return Task.CompletedTask;
        }
    }
}
