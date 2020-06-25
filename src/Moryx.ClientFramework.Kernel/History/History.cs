// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Moryx.ClientFramework.History;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Module history manager
    /// </summary>
    [KernelComponent(typeof(IHistory))]
    public class History : IHistory
    {
        private const int HistoryLength = 10;

        private readonly List<WorkspacePair> _history = new List<WorkspacePair>();

        private readonly List<WorkspacePair> _pushedScreens = new List<WorkspacePair>();

        #region IHistory API

        /// <summary>
        /// Push a module onto the history stack with a default screen
        /// </summary>
        public void Push(IWorkspaceModule module)
        {
            Push(module, module.CreateWorkspace());
        }

        /// <summary>
        /// Push a module onto the history stack with a specific screen
        /// </summary>
        public void Push(IWorkspaceModule module, IModuleWorkspace workspace)
        {
            if (_history.Count >= HistoryLength)
            {
                var pair = _history.ElementAt(0);
                pair.Module.DestroyWorkspace(pair.Workspace);
                _history.Remove(pair);
            }

            Push(new WorkspacePair(module, workspace));
        }

        /// <summary>
        /// Module tries to push itself onto the stack as result of a server side notification
        /// </summary>
        /// <returns>True if screen was pushed - false if enqueued!</returns>
        public bool TryPush(IWorkspaceModule module, IModuleWorkspace workspace)
        {
            var pushedPair = new WorkspacePair(module, workspace);
            if (Current.Workspace.CurrentInteraction != WorkspaceInteraction.Idle)
            {
                _pushedScreens.Add(pushedPair);
                return false;
            }

            // Current screen idles - so just push
            Push(pushedPair);
            return true;
        }

        /// <summary>
        /// Remove a pushed screen from the queue because it is no longer necessary
        /// </summary>
        public void RemovePush(IWorkspaceModule module, IModuleWorkspace workspace)
        {
            _pushedScreens.RemoveAll(pair => pair.Module == module && pair.Workspace == workspace);

            // If the current screen was part of the removed screens go back in history
            if (!_pushedScreens.Contains(Current))
            {
                MovePrevious();
            }
        }

        /// <summary>
        /// Current selected module
        /// </summary>
        public WorkspacePair Current { get; private set; }

        /// <summary>
        /// Index of the current selected module
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <inheritdoc />
        public bool MoveNext()
        {
            var next = CurrentIndex + 1;
            if (next >= _history.Count)
                return false;

            Move(next);
            return true;
        }

        /// <inheritdoc />
        public bool MovePrevious()
        {
            var next = CurrentIndex - 1;
            if (next < 0)
                return false;

            Move(next);
            return true;
        }

        /// <summary>
        /// Is risen when current workspace changes
        /// </summary>
        public event EventHandler<WorkspacePair> WorkspaceChanged;

        #endregion

        private void Push(WorkspacePair pair)
        {
            // If the current page is somewhere in the middle of history we remove all old ones 
            // This creates browser like behaviour
            while (CurrentIndex < _history.Count - 1)
            {
                _history.RemoveAt(CurrentIndex + 1);
            }
            _history.Add(pair);
            Move(_history.Count - 1);
        }

        private void Move(int nextIndex)
        {
            if (Current != null)
                Current.Workspace.InteractionChanged -= WorkspaceInteractionChanged;

            // Check limits due a remove push call
            if (nextIndex > _history.Count - 1)
                nextIndex = _history.Count - 1;

            if (nextIndex < 0)
                nextIndex = 0;

            CurrentIndex = nextIndex;
            Current = _history.ElementAt(CurrentIndex);
            Current.Workspace.InteractionChanged += WorkspaceInteractionChanged;

            WorkspaceChanged?.Invoke(this, Current);
        }

        private void WorkspaceInteractionChanged(object sender, WorkspaceInteraction interaction)
        {
            // Check if we can push a waiting screen
            if (interaction == WorkspaceInteraction.Idle && _pushedScreens.Any())
            {
                var next = _pushedScreens.ElementAt(0);
                _pushedScreens.RemoveAt(0);
                Push(next);
            }
        }
    }
}
