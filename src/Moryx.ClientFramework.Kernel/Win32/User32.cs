using System;
using System.Runtime.InteropServices;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// DllImports for User32.dll
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// Dll import to bring a window to the front
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Bring the window to front
        /// </summary>
        /// <param name="windowHandle"></param>
        public static bool BringToFront(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
            {
                return false;
            }

            return SetForegroundWindow(windowHandle);
        }
    }
}
