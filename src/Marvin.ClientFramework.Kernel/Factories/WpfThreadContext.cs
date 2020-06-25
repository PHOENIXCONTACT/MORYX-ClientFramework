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