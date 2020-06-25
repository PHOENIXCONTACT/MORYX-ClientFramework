// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows.Threading;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Interface for error catcher and handler
    /// </summary>
    public interface IErrorCatcher
    {
        /// <summary>
        /// Handles the unhandled dispatcher exceptions
        /// </summary>
        void HandleDispatcherException(object sender, DispatcherUnhandledExceptionEventArgs args);
    }
}
