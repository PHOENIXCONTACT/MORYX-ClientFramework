using Marvin.Container;

namespace Marvin.ClientFramework.Dialog
{
    /// <summary>
    /// Factory to create MessageBoxes
    /// </summary>
    [PluginFactory]
    public interface IMessageBoxFactory
    {
        /// <summary>
        /// Will create a messagebox
        /// </summary>
        IMessageBox Create();

        /// <summary>
        /// Destroys the specified message box.
        /// </summary>
        void Destroy(IMessageBox messageBox);
    }
}