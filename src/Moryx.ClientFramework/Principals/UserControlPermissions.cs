using System.Windows.Controls;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Class for the resource dictionary of a <see cref="UserControl"/>
    /// </summary>
    public class UserControlPermissions
    {
        /// <summary>
        /// Standard key to be found by the markup extension
        /// </summary>
        public const string Key = "Permissions";

        /// <summary>
        /// Resource applied to all instances of <see cref="PermissionExtension"/> unless specified
        /// </summary>
        public string Resource { get; set; }
    }
}