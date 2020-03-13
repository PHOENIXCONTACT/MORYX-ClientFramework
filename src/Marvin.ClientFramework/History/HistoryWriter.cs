// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

namespace Marvin.ClientFramework.History
{
    internal class HistoryWriter : IHistoryWriter
    {
        private readonly IWorkspaceModule _module;
        private readonly IHistory _history;

        public HistoryWriter(IWorkspaceModule module, IHistory history)
        {
            _module = module;
            _history = history;
        }

        /// <summary>
        /// Push a module onto the history stack with a specific workspace
        /// </summary>
        public void Push(IModuleWorkspace workspace)
        {
            _history.Push(_module, workspace);
        }

        /// <summary>
        /// Module tries to push itself onto the stack as result of a server side notification
        /// </summary>
        /// <returns>True if screen was pushed - false if enqueued!</returns>
        public bool TryPush(IModuleWorkspace workspace)
        {
            return _history.TryPush(_module, workspace);
        }

        /// <summary>
        /// Remove a pushed screen from the queue because it is no longer necessary
        /// </summary>
        public void RemovePush(IModuleWorkspace workspace)
        {
            _history.RemovePush(_module, workspace);
        }

        /// <summary>
        /// Go back in time to
        /// </summary>
        public void Reverse()
        {
            _history.MovePrevious();
        }
    }
}
