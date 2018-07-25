using System.Windows;
using Marvin.ClientFramework.History;
using Marvin.Container;

namespace Marvin.ClientFramework.Tests.HistoryWriter
{
    [Plugin(LifeCycle.Transient, typeof(IModuleWorkspace), Name = WorkspaceName)]
    public class HistoryWriterWorkspaceViewModel : ModuleWorkspace
    {
        internal const string WorkspaceName = "HistoryWriterWorkspaceViewModel";

        public bool ShowLifecycleMessages
        {
            get { return _showLifecycleMessages; }
            set
            {
                _showLifecycleMessages = value;
                NotifyOfPropertyChange(() => ShowLifecycleMessages);
            }
        }

        public bool ShowViewMessages
        {
            get { return _showViewMessages; }
            set
            {
                _showViewMessages = value;
                NotifyOfPropertyChange(() => ShowViewMessages);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            if (ShowLifecycleMessages)
                MessageBox.Show(string.Format("Workspace {0}: Initialized!", _level));
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            NotifyOfPropertyChange(() => ShowViewMessages);
            NotifyOfPropertyChange(() => ShowLifecycleMessages);

            if (ShowLifecycleMessages)
                MessageBox.Show(string.Format("Workspace {0}: Activated!", _level));
        }

        protected override void OnDeactivate(bool close)
        {
            base.OnDeactivate(close);
            if (ShowLifecycleMessages)
                MessageBox.Show(string.Format("Workspace {0}: Deactivated!", _level));
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (ShowViewMessages)
                MessageBox.Show(string.Format("Workspace {0}: View Loaded!", _level));
        }

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            if (ShowViewMessages)
                MessageBox.Show(string.Format("Workspace {0}: View Attached!", _level));
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            if (ShowViewMessages)
                MessageBox.Show(string.Format("Workspace {0}: View Ready!", _level));
        }

        // Injected
        public IHistoryWriter Writer { get; set; }
        public IHistoryWorkspaceFactory Factory { get; set; }

        private int _level;
        private static bool _showLifecycleMessages = true;
        private static bool _showViewMessages = false;

        public int Level
        {
            get { return _level; }
            set
            {
                if (value == _level) 
                    return;

                _level = value;
                NotifyOfPropertyChange();
            }
        }

        public HistoryWriterWorkspaceViewModel(int level)
        {
            Level = level;
        }

        public void Push()
        {
            Writer.Push(Factory.CreateWorkspace(Level + 1));
        }
    }
}