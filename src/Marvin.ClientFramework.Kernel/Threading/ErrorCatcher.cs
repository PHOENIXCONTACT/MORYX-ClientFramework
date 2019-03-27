using System;
using System.Windows.Threading;
using Marvin.Container;
using Marvin.Logging;
using Marvin.Tools;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// The error catcher of the client framework. will write the crash to the crashlog
    /// </summary>
    [KernelComponent(typeof(IErrorCatcher))]
    public class ErrorCatcher : IErrorCatcher, ILoggingComponent
    {
        /// <summary>
        /// Logger to log exceptions from the framework
        /// </summary>
        public IModuleLogger Logger { get; set; }

        /// <inheritdoc />
        public void HandleDispatcherException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            Logger.LogException(LogLevel.Error, args.Exception, "There is an error on the UI thread! Check it!");
            CrashHandler.HandleCrash(sender, new UnhandledExceptionEventArgs(args.Exception, false));
        }
    }
}