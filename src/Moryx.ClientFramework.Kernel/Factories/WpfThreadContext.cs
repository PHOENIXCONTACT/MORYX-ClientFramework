// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using Moryx.Tools.Wcf;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// The thread context to be used by WPF applications for the <see cref="BaseWcfClientFactory"/>
    /// </summary>
    internal class WpfThreadContext : IThreadContext
    {
        /// <inheritdoc />
        public void Invoke(Action action)
        {
            ThreadContext.BeginInvoke(action);
        }
    }
}
