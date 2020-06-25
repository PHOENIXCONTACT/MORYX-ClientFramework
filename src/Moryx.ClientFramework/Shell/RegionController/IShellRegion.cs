// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.ComponentModel;

namespace Moryx.ClientFramework.Shell
{
    /// <summary>
    /// Interface for all plugins displayed in the shell
    /// </summary>
    public interface IShellRegion : INotifyPropertyChanged
    {
        /// <summary>
        /// Connect this plugin with the shell
        /// </summary>
        /// <param name="shell">Parent instance</param>
        void ConnectToShell(ShellViewModelBase shell);
    }
}
