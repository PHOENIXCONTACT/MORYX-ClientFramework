// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows;
using Caliburn.Micro;
using Moryx.Controls.Demo.Shell;

namespace Moryx.Controls.Demo
{
    public class DemoBootstrapper : BootstrapperBase
    {
        public DemoBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
