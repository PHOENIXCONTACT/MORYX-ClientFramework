// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Classification of the current user interaction with the screen - determines wether the screen may be hidden or
    /// a new screen must wait for its turn
    /// </summary>
    public enum WorkspaceInteraction
    {
        /// <summary>
        /// Screen idles - content is displayed but no active user interaction. Should be the default state for correct push behaviour
        /// </summary>
        Idle,
        /// <summary>
        /// User actively interacts with the screen by editing data
        /// </summary>
        Editing,
        /// <summary>
        /// New content was recently displayed and the user is probably still reading it
        /// </summary>
        Viewing,
    }
}
