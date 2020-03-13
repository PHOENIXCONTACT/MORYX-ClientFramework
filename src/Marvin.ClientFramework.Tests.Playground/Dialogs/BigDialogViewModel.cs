// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Caliburn.Micro;

namespace Marvin.ClientFramework.Tests.Playground
{
    public class BigDialogViewModel : Screen
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            DisplayName = "Hello World";
        }
    }
}
