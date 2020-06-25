using System;
using System.Windows;

namespace Marvin.ClientFramework
{
    public class AccessGridLenghtExtension : OperationAccessExtensionBase
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CheckAccess() ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }
    }
}
