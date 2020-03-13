// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Threading;
using System.Windows.Media;
using Caliburn.Micro;

namespace Marvin.ClientFramework.Tests.NotifyAndEditor
{
    [ClientModule("NotifyAndEditor")]
    public class ModuleController : WorkspaceModuleBase<ModuleConfig>
    {
        public override Geometry Icon => Geometry.Parse("F1M48.611,27.742C48.611,27.742 42.804,27.742 42.804,27.742 42.804,27.742 42.804,16.822 42.804,16.822 " +
                                                        "42.804,16.822 25.861,16.822 25.861,16.822 25.861,16.822 25.861,8.258 25.861,8.258 25.861,8.258 31.668,8.258 " +
                                                        "31.668,8.258 32.027,8.258 32.318,7.967 32.318,7.608 32.318,7.608 32.318,0.778 32.318,0.778 32.318,0.418 " +
                                                        "32.027,0.127 31.668,0.127 31.668,0.127 18.332,0.127 18.332,0.127 17.973,0.127 17.682,0.418 17.682,0.778 " +
                                                        "17.682,0.778 17.682,7.608 17.682,7.608 17.682,7.967 17.973,8.258 18.332,8.258 18.332,8.258 24.139,8.258 " +
                                                        "24.139,8.258 24.139,8.258 24.139,16.822 24.139,16.822 24.139,16.822 7.196,16.822 7.196,16.822 7.196,16.822 " +
                                                        "7.196,27.742 7.196,27.742 7.196,27.742 1.389,27.742 1.389,27.742 1.029,27.742 0.738,28.033 0.738,28.392 " +
                                                        "0.738,28.392 0.738,35.222 0.738,35.222 0.738,35.582 1.029,35.873 1.389,35.873 1.389,35.873 14.724,35.873 " +
                                                        "14.724,35.873 15.083,35.873 15.375,35.582 15.375,35.222 15.375,35.222 15.375,28.392 15.375,28.392 15.375,28.033 " +
                                                        "15.083,27.742 14.724,27.742 14.724,27.742 8.917,27.742 8.917,27.742 8.917,27.742 8.917,18.543 8.917,18.543 " +
                                                        "8.917,18.543 24.139,18.543 24.139,18.543 24.139,18.543 24.139,27.742 24.139,27.742 24.139,27.742 18.332,27.742 " +
                                                        "18.332,27.742 17.973,27.742 17.682,28.033 17.682,28.392 17.682,28.392 17.682,35.222 17.682,35.222 17.682,35.582" +
                                                        " 17.973,35.873 18.332,35.873 18.332,35.873 31.668,35.873 31.668,35.873 32.027,35.873 32.318,35.582 32.318,35.222 " +
                                                        "32.318,35.222 32.318,28.392 32.318,28.392 32.318,28.033 32.027,27.742 31.668,27.742 31.668,27.742 25.861,27.742 " +
                                                        "25.861,27.742 25.861,27.742 25.861,18.543 25.861,18.543 25.861,18.543 41.083,18.543 41.083,18.543 41.083,18.543 " +
                                                        "41.083,27.742 41.083,27.742 41.083,27.742 35.276,27.742 35.276,27.742 34.917,27.742 34.625,28.033 34.625,28.392 " +
                                                        "34.625,28.392 34.625,35.222 34.625,35.222 34.625,35.582 34.917,35.873 35.276,35.873 35.276,35.873 48.611,35.873 " +
                                                        "48.611,35.873 48.971,35.873 49.262,35.582 49.262,35.222 49.262,35.222 49.262,28.392 49.262,28.392 49.262,28.033 " +
                                                        "48.971,27.742 48.611,27.742z");


        protected override void OnInitialize()
        {
            //Config.DisplayName = "Module Notify";
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
        private bool _timerRunning;

        internal void StopTimer()
        {
            _tim?.Change(new TimeSpan(0, 0, 0, 0,-1), new TimeSpan(0, 0, 0, 0, -1));
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
            var t = _timerRunning;

            StopTimer();
            
            Notifications.Clear();

            if(t)
                StartTimer();
        }
        #endregion
    }
}
