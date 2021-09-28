// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Moryx.ClientFramework.History;
using Moryx.Container;

namespace Moryx.ClientFramework.Tests.HistoryWriter
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

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            if (ShowLifecycleMessages)
                MessageBox.Show($"Workspace {_level}: Initialized!");

            return Task.CompletedTask;
        }



        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            NotifyOfPropertyChange(() => ShowViewMessages);
            NotifyOfPropertyChange(() => ShowLifecycleMessages);

            if (ShowLifecycleMessages)
                MessageBox.Show($"Workspace {_level}: Activated!");

            return Task.CompletedTask;
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            if (ShowLifecycleMessages)
                MessageBox.Show($"Workspace {_level}: Deactivated!");

            return Task.CompletedTask;
        }


        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            if (ShowViewMessages)
                MessageBox.Show($"Workspace {_level}: View Loaded!");
        }

        protected override void OnViewAttached(object view, object context)
        {
            base.OnViewAttached(view, context);
            if (ShowViewMessages)
                MessageBox.Show($"Workspace {_level}: View Attached!");
        }

        protected override void OnViewReady(object view)
        {
            base.OnViewReady(view);
            if (ShowViewMessages)
                MessageBox.Show($"Workspace {_level}: View Ready!");
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
