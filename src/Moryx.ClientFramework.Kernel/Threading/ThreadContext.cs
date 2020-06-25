using System;
using System.Windows.Threading;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Static class which holds the current dispatcher thread
    /// </summary>
    public static class ThreadContext
    {
        /// <summary>
        /// Gets or sets the default dispatcher.
        /// </summary>
        public static Dispatcher Dispatcher { get; set; }

        /// <summary>
        /// If the dispatcher is available, the method will be invoked on it
        /// </summary>
        /// <param name="action">The action to call</param>
        /// <exception cref="ClientException">Dispatcher was not set</exception>
        public static void BeginInvoke(Action action)
        {
            if (Dispatcher == null)
            {
                throw new ClientException("Dispatcher was not set", null, "Dispatcher was not set");
            }

            if (Dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                Dispatcher.BeginInvoke(action);
            }
        }
    }
}
