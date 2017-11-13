using System;

namespace Marvin.ClientFramework.Kernel
{
    public class LoaderAdapterBase
    {
        public event EventHandler<string> ChangeMessage;
        protected virtual void RaiseChangeMessage(string e)
        {
            ChangeMessage?.Invoke(this, e);
        }

        public event EventHandler<int> AddToMax;
        protected virtual void RaiseAddToMax(int e)
        {
            AddToMax?.Invoke(this, e);
        }

        public event EventHandler<string> ChangeValueWithMessage;
        protected virtual void RaiseChangeValueWithMessage(string e)
        {
            ChangeValueWithMessage?.Invoke(this, e);
        }

        public event EventHandler<ClientException> IndicateError;
        protected virtual void RaiseIndicateError(ClientException e)
        {
            IndicateError?.Invoke(this, e);
        }
    }
}