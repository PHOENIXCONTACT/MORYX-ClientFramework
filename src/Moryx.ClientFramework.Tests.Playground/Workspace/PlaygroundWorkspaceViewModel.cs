// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Threading;
using System.Threading.Tasks;
using Moryx.ClientFramework.Commands;
using Moryx.ClientFramework.Dialog;
using Moryx.Container;

namespace Moryx.ClientFramework.Tests.Playground
{
    public interface ITaskRunner
    {
        Task LongMethod();

        Task<string> LongMethodWithReturn();

        Task ErrorMethod();
    }

    [Plugin(LifeCycle.Singleton, typeof (ITaskRunner))]
    internal class TaskRunner : ITaskRunner
    {
        public async Task LongMethod()
        {
            var task = Task.Run(delegate
            {
                Thread.Sleep(5000);
            });

            await task;
        }

        public async Task<string> LongMethodWithReturn()
        {
            await LongMethod();

            return "Hello Task";
        }

        public async Task ErrorMethod()
        {
            await Task.Run(delegate
            {
                Thread.Sleep(3000);

                throw new MethodAccessException("dont do that!");
            });
        }
    }


    [Plugin(LifeCycle.Singleton, typeof(IModuleWorkspace), Name = WorkspaceName)]
    public class PlaygroundWorkspaceViewModel : ModuleWorkspace
    {
        internal const string WorkspaceName = "PlaygroundWorkspaceViewModel";

        private bool _longActivated;

        public ITaskRunner TaskRunner { get; set; }
        public IDialogManager DialogManager { get; set; }

        public AsyncCommand LongCmd { get; private set; }

        public AsyncCommand LongReturnCmd { get; private set; }

        public AsyncCommand ExceptionCmd { get; private set; }


        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            LongCmd = new AsyncCommand(LongMethod, CanExecuteLongMethod);

            LongReturnCmd = new AsyncCommand(LongReturnMethod);

            ExceptionCmd = new AsyncCommand(ExceptionMethod);

            return Task.CompletedTask;
        }

        #region ExceptionMethod

        private async Task ExceptionMethod(object parameters)
        {
            CurrentCommand = ExceptionCmd;

            await TaskRunner.ErrorMethod();
        }

        #endregion

        #region Long Method

        private bool CanExecuteLongMethod(object parameters)
        {
            return LongActivated;
        }

        private async Task LongMethod(object parameters)
        {
            CurrentCommand = LongCmd;

            var task = TaskRunner.LongMethod();
            await task;
        }

        public bool LongActivated
        {
            get { return _longActivated; }
            set
            {
                _longActivated = value;
                LongCmd.RaiseCanExecuteChanged();
            }
        }

        #endregion

        #region Long Return method

        private async Task LongReturnMethod(object parameters)
        {
            CurrentCommand = LongReturnCmd;

            var task = TaskRunner.LongMethodWithReturn();
            var value = await task;

            ReturnValue = value;
        }

        #endregion

        private AsyncCommand _currentCommand;
        public AsyncCommand CurrentCommand
        {
            get { return _currentCommand; }
            set
            {
                _currentCommand = value;
                NotifyOfPropertyChange(() => CurrentCommand);
            }
        }

        private string _returnValue;
        public string ReturnValue
        {
            get { return _returnValue; }
            set
            {
                _returnValue = value;
                NotifyOfPropertyChange(() => ReturnValue);
            }
        }
    }
}
