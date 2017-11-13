using Caliburn.Micro;

namespace Marvin.ClientFramework.Dialog
{
    /// <summary>
    /// Screen extension whith an result
    /// </summary>
    public interface IDialogScreen : IScreen
    {
        /// <summary>
        /// Result of the dialog
        /// </summary>
        bool Result { get; }
    }
}