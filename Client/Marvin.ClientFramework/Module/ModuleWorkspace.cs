using System;
using Caliburn.Micro;

namespace Marvin.ClientFramework
{
    //TODO: remove copy and paste!!!

    /// <summary>
    /// The ModuleWorkspace is the main visible part. 
    /// In the default shell the workspace will be loaded into the white big box
    /// </summary>
    public partial class ModuleWorkspace<T> where T : class
    {
        private WorkspaceInteraction _currentInteraction;

        /// <summary>
        /// Current interaction state of the screen
        /// </summary>
        public WorkspaceInteraction CurrentInteraction
        {
            get { return _currentInteraction; }
            protected internal set
            {
                if (value == _currentInteraction) return;
                _currentInteraction = value;
                InteractionChanged(this, value);
            }
        }

        /// <summary>
        /// Event raised when interaction changes
        /// </summary>
        public event EventHandler<WorkspaceInteraction> InteractionChanged;
    }

    public partial class ModuleWorkspace<T>
    {
        /// <summary>
        /// An implementation of <see cref="IConductor"/> that holds on to many items wich are all activated.
        /// </summary>
        public class AllActive : Conductor<T>.Collection.AllActive, IModuleWorkspace
        {
            private WorkspaceInteraction _currentInteraction;

            /// <summary>
            /// Current interaction state of the screen
            /// </summary>
            public WorkspaceInteraction CurrentInteraction
            {
                get { return _currentInteraction; }
                protected internal set
                {
                    if (value == _currentInteraction) return;
                    _currentInteraction = value;
                    InteractionChanged(this, value);
                }
            }

            /// <summary>
            /// Event raised when interaction changes
            /// </summary>
            public event EventHandler<WorkspaceInteraction> InteractionChanged;
        }
    }

    public partial class ModuleWorkspace<T>
    {
        /// <summary>
        /// An implementation of <see cref="IConductor"/> that holds on many items but only activates one at a time.
        /// </summary>
        public class OneActive : Conductor<T>.Collection.OneActive, IModuleWorkspace
        {
            private WorkspaceInteraction _currentInteraction;

            /// <summary>
            /// Current interaction state of the screen
            /// </summary>
            public WorkspaceInteraction CurrentInteraction
            {
                get { return _currentInteraction; }
                protected internal set
                {
                    if (value == _currentInteraction) return;
                    _currentInteraction = value;
                    InteractionChanged(this, value);
                }
            }

            /// <summary>
            /// Event raised when interaction changes
            /// </summary>
            public event EventHandler<WorkspaceInteraction> InteractionChanged;
        }
    }
    
    /// <summary>
    /// The ModuleWorkspace is the main visible part. 
    /// In the default shell the workspace will be loaded into the white big box
    /// </summary>
    public class ModuleWorkspace : Screen, IModuleWorkspace
    {
        private WorkspaceInteraction _currentInteraction;

        /// <summary>
        /// Current interaction state of the screen
        /// </summary>
        public WorkspaceInteraction CurrentInteraction
        {
            get { return _currentInteraction; }
            protected internal set
            {
                if (value == _currentInteraction) return;
                _currentInteraction = value;
                InteractionChanged(this, value);
            }
        }

        /// <summary>
        /// Event raised when interaction changes
        /// </summary>
        public event EventHandler<WorkspaceInteraction> InteractionChanged;
    }
}