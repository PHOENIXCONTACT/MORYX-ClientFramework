// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Moryx.ClientFramework.History
{
    /// <summary>
    /// Event args containing all information for the workspace change
    /// </summary>
    public class WorkspacePair : EventArgs
    {
        /// <summary>
        /// Fill event args with workspace change arguments
        /// </summary>
        public WorkspacePair(IWorkspaceModule module, IModuleWorkspace workspace)
        {
            Module = module;
            Workspace = workspace;
        }

        /// <summary>
        /// Module hosting the new workspace
        /// </summary>
        public IWorkspaceModule Module { get; }

        /// <summary>
        /// Workspace instance for visu
        /// </summary>
        public IModuleWorkspace Workspace { get; }
    }
}
