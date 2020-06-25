using System;
using Marvin.Logging;
using Marvin.Modules;

namespace Marvin.ClientFramework
{
    internal class LocalModuleErrorReporting : IModuleErrorReporting
    {
        public IModuleLogger Logger { get; set; }

        public void ReportFailure(object sender, Exception exception)
        {
            Logger.LogException(LogLevel.Error, exception, "");
        }

        public void ReportWarning(object sender, Exception exception)
        {
            Logger.LogException(LogLevel.Error, exception, "");
        }
    }
}