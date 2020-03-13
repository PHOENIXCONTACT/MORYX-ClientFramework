// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Screen instance for the viewport
    /// </summary>
    public interface IModuleWorkspace
    {
        /// <summary>
        /// Current interaction state of the screen
        /// </summary>
        WorkspaceInteraction CurrentInteraction { get; }

        /// <summary>
        /// Event raised when the current interaction type changes
        /// </summary>
        event EventHandler<WorkspaceInteraction> InteractionChanged;
    }
}
