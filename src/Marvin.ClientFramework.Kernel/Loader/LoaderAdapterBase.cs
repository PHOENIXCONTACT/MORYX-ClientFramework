// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Loader adapter base
    /// </summary>
    public class LoaderAdapterBase
    {
        /// <summary>
        /// Is risen when message shall change
        /// </summary>
        public event EventHandler<string> ChangeMessage;

        /// <summary>
        /// Raises <see cref="ChangeMessage"/> event
        /// </summary>
        protected virtual void RaiseChangeMessage(string e)
        {
            ChangeMessage?.Invoke(this, e);
        }

        /// <summary>
        /// Add to max
        /// </summary>
        public event EventHandler<int> AddToMax;

        /// <summary>
        /// Raises <see cref="AddToMax"/> event
        /// </summary>
        protected virtual void RaiseAddToMax(int e)
        {
            AddToMax?.Invoke(this, e);
        }

        /// <summary>
        /// Change value with message
        /// </summary>
        public event EventHandler<string> ChangeValueWithMessage;

        /// <summary>
        /// Raises <see cref="RaiseChangeValueWithMessage"/> event
        /// </summary>
        protected virtual void RaiseChangeValueWithMessage(string e)
        {
            ChangeValueWithMessage?.Invoke(this, e);
        }

        /// <summary>
        /// Is risen to indicate an error
        /// </summary>
        public event EventHandler<ClientException> IndicateError;

        /// <summary>
        /// Raises <see cref="IndicateError"/> event
        /// </summary>
        protected virtual void RaiseIndicateError(ClientException e)
        {
            IndicateError?.Invoke(this, e);
        }
    }
}
