// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// Loader adapter are to adapt components to the <see cref="ILoaderHandler"/>
    /// They can indicate their state for the <see cref="ILoaderHandler"/>
    /// </summary>
    public interface ILoaderAdapter
    {
        /// <summary>
        /// Determines whether this instance can adapt the specified component.
        /// </summary>
        bool CanAdapt(object component);

        /// <summary>
        /// Adapts the specified component.
        /// </summary>
        void Adapt(object component);

        /// <summary>
        /// changes the message on the loader
        /// </summary>
        event EventHandler<string> ChangeMessage;
        
        /// <summary>
        /// used to add an integer value to the progress maximum
        /// </summary>
        event EventHandler<int> AddToMax;

        /// <summary>
        /// will add 1 to the value of the progress and changes the message
        /// </summary>
        event EventHandler<string> ChangeValueWithMessage;

        /// <summary>
        /// will indicate an error on the ui and presents the error message
        /// </summary>
        event EventHandler<ClientException> IndicateError;        
    }
}
