using System;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Operation access extension for bool
    /// </summary>
    public class AccessBoolExtension : OperationAccessExtensionBase
    {
        /// <inheritdoc />
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CheckAccess();
        }
    }
}
