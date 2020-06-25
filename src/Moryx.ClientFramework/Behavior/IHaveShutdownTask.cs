// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using Caliburn.Micro;

namespace Moryx.ClientFramework.Behavior
{
    /// <summary>
    /// Interface for modules and plugins which are 
    /// indicating that they have a running shutdown task
    /// </summary>
    public interface IHaveShutdownTask
    {
        /// <summary>
        /// Gets the shutdown task.
        /// </summary>
        IResult GetShutdownTask();
    }
}
