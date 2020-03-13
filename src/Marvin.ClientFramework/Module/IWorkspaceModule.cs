// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Windows.Media;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Client modules that display content on the workspace
    /// </summary>
    public interface IWorkspaceModule : IClientModule
    {
        /// <summary>
        /// Current display name of this module
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Icon for the module
        /// </summary>
        Geometry Icon { get; }

        /// <summary>
        /// Gets a value indicating whether the module is visible in the shell navigation.
        /// </summary>
        bool HasButton { get; }

        /// <summary>
        /// Creates a Workspace.
        /// </summary>
        IModuleWorkspace CreateWorkspace();

        /// <summary>
        /// Destroys a Workspace.
        /// </summary>
        void DestroyWorkspace(IModuleWorkspace workspace);
    }
}
