// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Caliburn.Micro;

namespace Marvin.ClientFramework.Tests.DialogManager
{
    public class SmallDialogViewModel : Screen
    {
        protected override void OnInitialize()
        {
            DisplayName = "Small Dialog Title";

            base.OnInitialize();
        }

        public void CloseCommand()
        {
            TryClose(true);
        }
    }
}
