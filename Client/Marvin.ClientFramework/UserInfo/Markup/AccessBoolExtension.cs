using System;

namespace Marvin.ClientFramework
{
    public class AccessBoolExtension : OperationAccessExtensionBase
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CheckAccess();
        }
    }
}
