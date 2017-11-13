using System;

namespace Marvin.ClientFramework.UI
{
    public class AccessBoolExtension : OperationAccessExtensionBase
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CheckAccess();
        }
    }
}
