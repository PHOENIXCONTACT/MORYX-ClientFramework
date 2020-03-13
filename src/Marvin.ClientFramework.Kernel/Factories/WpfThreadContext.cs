// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using Marvin.Tools.Wcf;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// The thread context to be used by WPF applications for the <see cref="BaseWcfClientFactory"/>
    /// </summary>
    internal class WpfThreadContext : IThreadContext
    {
        /// 
        public void Invoke(Action action)
        {
            ThreadContext.BeginInvoke(action);
        }
    }
}
