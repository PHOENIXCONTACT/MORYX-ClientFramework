using System.Runtime.InteropServices;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// DllImports for Kernel32.dll
    /// </summary>
    public static class Kernel32
    {
        /// <summary>
        /// Dll import to attach this application to the console stream
        /// </summary>
        [DllImport("Kernel32.dll")]
        public static extern bool AttachConsole(int processId);
    }
}
