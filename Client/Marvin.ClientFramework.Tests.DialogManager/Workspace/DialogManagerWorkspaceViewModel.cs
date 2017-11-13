using System;
using System.Windows;
using Marvin.ClientFramework.Base;
using Marvin.ClientFramework.Dialog;
using Marvin.ClientFramework.Tests.DialogManager.Dialogs;
using Marvin.Container;
using MessageBoxImage = Marvin.ClientFramework.Dialog.MessageBoxImage;
using MessageBoxOptions = Marvin.ClientFramework.Dialog.MessageBoxOptions;

namespace Marvin.ClientFramework.Tests.DialogManager
{
    [Plugin(LifeCycle.Transient, typeof(IModuleWorkspace), Name = WorkspaceName)]
    public class DialogManagerWorkspaceViewModel : ModuleWorkspace
    {
        internal const string WorkspaceName = "DialogManagerWorkspaceViewModel";

        #region Dependency 

        public IDialogManager DialogManager { get; set; }

        #endregion

        private readonly SmallDialogViewModel _dialogViewModel = new SmallDialogViewModel();

        public string SystemRunningAs
        {
            get
            {
                if (Environment.Is64BitOperatingSystem)
                    return "x64";
                else
                    return "x86";
            }
        }

        public string ProcessRunningAs
        {
            get
            {
                if (Environment.Is64BitProcess)
                    return "x64";
                else
                    return "x86";
            }
        }


        public void ShowSmallDialog()
        {
            DialogManager.ShowDialog(_dialogViewModel);
        }

        public void ShowSmallDialogWithCallback()
        {
            // to test if a callback is rised multiple time the dialogmodel must be a singelton!
            DialogManager.ShowDialog(_dialogViewModel, delegate 
            {
                MessageBox.Show("Callback has been rised!", "Callback!");
            });
        }

        public void ShowMessageBox()
        {
            DialogManager.ShowMessageBox("This is a MessageBox!", "Example Title", MessageBoxOptions.YesNoCancel, MessageBoxImage.Attention);
        }

        public void ShowMessageBoxWithCallback()
        {
            DialogManager.ShowMessageBox("This is a MessageBox!", "Example Title", MessageBoxOptions.YesNoCancel, MessageBoxImage.Attention,
                delegate(IMessageBox box)
                {
                    MessageBox.Show("Callback has been rised! Result: " + box.Result, "Callback!");
                });
        }
    }
}