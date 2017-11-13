using System;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// BaseClass for exceptions within Client code. It should add usable information to the System.Exception, 
    /// that may be used by a superior component/module/plugin to handle the exception.
    /// </summary>
    public class ClientException : MarvinException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientException"/> class.
        /// </summary>
        public ClientException(string message, Exception innerException, string displayText)
            : base(message, innerException, true, true, displayText)
        {
            
        }
    }
}
