using System;
using System.Collections.Generic;
using Moryx.Users;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Static class to store the current operation accesses
    /// </summary>
    public static class ApplicationAccesses
    {
        private static Dictionary<string, OperationAccess> _acesses;

        /// <summary>
        /// Sets the accesses.
        /// </summary>
        /// <exception cref="System.ArgumentException">Accesses are allready set!</exception>
        public static void SetAccesses(Dictionary<string, OperationAccess> accesses)
        {
            if (_acesses != null)
                throw new ArgumentException("Accesses are allready set!", nameof(accesses));

            _acesses = accesses;
        }

        /// <summary>
        /// Gets the acesses.
        /// </summary>
        public static Dictionary<string, OperationAccess> Acesses => _acesses ?? new Dictionary<string, OperationAccess>();
    }
}