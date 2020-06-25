using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Provides functions to capture the entire screen, 
    /// or a particular window, and save it to a file.
    /// </summary>
    public interface IScreenCapture
    {
        /// <summary>
        /// Creates an Image object containing a screen shot of the entire desktop
        /// </summary>
        /// <returns></returns>
        Image CaptureScreen();

        /// <summary>
        /// Creates an Image object containing a screen shot of a specific window
        /// </summary>
        /// <param name="handle">The handle to the window. 
        /// (In windows forms, this is obtained by the Handle property)</param>
        /// <returns></returns>
        Image CaptureWindow(IntPtr handle);

        /// <summary>
        /// Captures a screen shot of a specific window, and saves it to a file
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        void CaptureWindowToFile(IntPtr handle, string filename, ImageFormat format);

        /// <summary>
        /// Captures a screen shot of the entire desktop, and saves it to a file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="format"></param>
        void CaptureScreenToFile(string filename, ImageFormat format);
    }
}
