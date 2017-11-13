using System;
using System.Windows;

namespace Marvin.ClientFramework.UI
{
    public class AccessVisibilityExtension : OperationAccessExtensionBase
    {
        /// <summary>
        /// When implemented in a derived class, returns an object that is provided as the value of the target property for this markup extension.
        /// </summary>
        /// <param name="serviceProvider">A service provider helper that can provide services for the markup extension.</param>
        /// <returns> The object value to set on the property where the extension is applied. </returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return CheckAccess() ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}