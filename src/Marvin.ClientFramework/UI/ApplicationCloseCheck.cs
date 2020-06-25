using System;
using Caliburn.Micro;
using Marvin.ClientFramework.Dialog;
using Marvin.ClientFramework.Shell;

namespace Marvin.ClientFramework.UI
{
    public class ApplicationCloseCheck : IResult
    {
        private readonly Action<IDialogManager, Action<bool>> _closeCheck;

        private readonly IChild _childScreen;

        public ApplicationCloseCheck(IChild screen, Action<IDialogManager, Action<bool>> closeCheck)
        {
            _childScreen = screen;
            _closeCheck = closeCheck;
        }

        public IModuleShell ModuleShell { get; set; }

        public void Execute(CoroutineExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public event EventHandler<ResultCompletionEventArgs> Completed;

        protected virtual void OnCompleted(ResultCompletionEventArgs e)
        {
            EventHandler<ResultCompletionEventArgs> handler = Completed;
            if (handler != null) handler(this, e);
        }
    }
}
