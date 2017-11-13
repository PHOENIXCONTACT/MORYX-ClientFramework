using System.Windows.Markup;
using Marvin.Users;

namespace Marvin.ClientFramework.UI
{
    /// <summary>
    /// Base class for MarkupExtensions which check the access
    /// for operations
    /// </summary>
    public abstract class OperationAccessExtensionBase : MarkupExtension
    {
        /// <summary>
        /// Gets or sets the operation which should be used to check 
        /// the current access right.
        /// </summary>
        [ConstructorArgument("operation")]
        public string Operation { get; set; }

        /// <summary>
        /// Checks the access. True: Access granted, False: access refused
        /// </summary>
        public bool CheckAccess()
        {
            var accesses = ApplicationAccesses.Acesses;

            if (accesses != null && Operation != null && accesses.ContainsKey(Operation))
            {
                return accesses[Operation] >= OperationAccess.LimitedRead;
            }

            return false;
        }
    }
}