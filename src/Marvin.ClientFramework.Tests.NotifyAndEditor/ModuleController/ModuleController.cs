using System;
using System.Threading;
using Caliburn.Micro;
using Marvin.ClientFramework.Base;

namespace Marvin.ClientFramework.Tests.NotifyAndEditor
{
    [ClientModule("NotifyAndEditor")]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        protected override void OnInitialize()
        {
            Config.DisplayName = "Module Notify";
        }

        protected override void OnActivate()
        {

        }

        protected override void OnDeactivate(bool close)
        {

        }

        protected override IModuleWorkspace OnCreateWorkspace()
        {
            if (_timerRunning)
                ResetNotifications();

            var workspace = Container.Resolve<IModuleWorkspace>(NotifyAndEditorWorkspaceViewModel.ScreenName);

            ((NotifyAndEditorWorkspaceViewModel)workspace).StartTimer = StartTimer;
            ((NotifyAndEditorWorkspaceViewModel)workspace).ResetNotifications = ResetNotifications;
            ((NotifyAndEditorWorkspaceViewModel)workspace).StopTimer = StopTimer;
            ((NotifyAndEditorWorkspaceViewModel)workspace).StartTimer = StartTimer;

            return workspace;
        }

        protected override void OnDestroyWorkspace(IModuleWorkspace workspace)
        {

        }

        #region Timer
        private Timer _tim;
        private bool _timerRunning = false;
        internal void StopTimer()
        {
            if (_tim != null)
                _tim.Change(new TimeSpan(0, 0, 0, 0,-1), new TimeSpan(0, 0, 0, 0, -1));

            _timerRunning = false;
        }

        internal void StartTimer()
        {
            if (_tim == null)
            {
                _tim = new Timer(state =>
                {
                    try
                    {
                        Execute.OnUIThread(() => Notifications.Add(new PendingWorkspaceNotification()));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }

            _tim.Change(new TimeSpan(0, 0, 0, 0, 100), new TimeSpan(0, 0, 0, 0, 100));
            _timerRunning = true;
        }


        internal void ResetNotifications()
        {
            bool t = _timerRunning;

            StopTimer();
            
            Notifications.Clear();

            if(t)
                StartTimer();
        }
        #endregion
    }
}