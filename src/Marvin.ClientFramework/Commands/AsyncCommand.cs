// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Marvin.ClientFramework.Tasks;

namespace Marvin.ClientFramework.Commands
{
    /// <summary>
    /// An async command that implements <see cref="ICommand"/>, forwarding <see cref="ICommand.Execute(object)"/> 
    /// to <see cref="IAsyncCommand.ExecuteAsync(object)"/>.
    /// A basic asynchronous command, which (by default) is disabled while the command is executing.
    /// </summary>
    public sealed class AsyncCommand : IAsyncCommand, INotifyPropertyChanged
    {
        #region Fields and Properties
        /// <summary>
        /// The local implementation of <see cref="ICommand.CanExecuteChanged"/>.
        /// </summary>
        private readonly WeakCanExecuteChanged _canExecuteChanged;

        /// <summary>
        /// Indicator if the command manager will be used
        /// </summary>
        private readonly bool _useCommandManager;

        /// <summary>
        /// The implementation of <see cref="IAsyncCommand.ExecuteAsync(object)"/>.
        /// </summary>
        private readonly Func<object, Task> _executeAsync;

        /// <summary>
        /// The implementation of <see cref="ICommand.CanExecute(object)"/>. May be <c>null</c>.
        /// </summary>
        private readonly Func<object, bool> _canExecute;

        /// <summary>
        /// Represents the execution of the asynchronous command.
        /// </summary>
        public TaskNotifier Execution { get; private set; }

        /// <summary>
        /// Whether the asynchronous command is currently executing.
        /// </summary>
        public bool IsExecuting => Execution != null && Execution.IsNotCompleted;

        #endregion

        /// <summary>
        /// Creates a new asynchronous command, with the specified asynchronous delegate as its implementation.
        /// </summary>
        public AsyncCommand(Func<object, Task> executeAsync) : this(executeAsync, null, false)
        {
        }

        /// <summary>
        /// Creates a new asynchronous command, with the specified asynchronous delegate as its implementation.
        /// </summary>
        public AsyncCommand(Func<object, Task> executeAsync, Func<object, bool> canExecute) : this(executeAsync, canExecute, false)
        {
        }

        /// <summary>
        /// Creates a new asynchronous command, with the specified asynchronous delegate as its implementation which can use a weak event handler or the command manager.
        /// </summary>
        public AsyncCommand(Func<object, Task> executeAsync, Func<object, bool> canExecute, bool useCommandManager)
        {
            _canExecuteChanged = new WeakCanExecuteChanged(this);
            _executeAsync = executeAsync;
            _canExecute = canExecute;
            _useCommandManager = useCommandManager;
        }

        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/>.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (_useCommandManager)
            {
                CommandManager.InvalidateRequerySuggested();
            }
            else
            {
                _canExecuteChanged.RaiseCanExecuteChanged();
            }
        }

        /// <inheritdoc />
        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                if (_useCommandManager)
                {
                    CommandManager.RequerySuggested += value;
                }
                else
                {
                    _canExecuteChanged.CanExecuteChanged += value;
                }
            }
            remove
            {
                if (_useCommandManager)
                {
                    CommandManager.RequerySuggested -= value;
                }
                else
                {
                    _canExecuteChanged.CanExecuteChanged -= value;
                }
            }
        }

        /// <summary>
        /// The implementation of <see cref="ICommand.CanExecute(object)"/>.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return !IsExecuting;

            return !IsExecuting && _canExecute(parameter);
        }

        /// <inheritdoc />
        async void ICommand.Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        /// <summary>
        /// Executes the asynchronous command. 
        /// Any exceptions from the asynchronous delegate are captured 
        /// and placed on <see cref="Execution"/>; they are not propagated to the UI loop.
        /// </summary>
        /// <param name="parameter">The parameter for the command.</param>
        public async Task ExecuteAsync(object parameter)
        {
            Execution = TaskNotifier.Create(_executeAsync(parameter));

            RaiseCanExecuteChanged();
            RaisePropertyChanged("Execution");
            RaisePropertyChanged("IsExecuting");

            await Execution.TaskCompleted;

            RaiseCanExecuteChanged();
            RaisePropertyChanged("IsExecuting");
        }


        /// <inheritdoc />
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed events on multiple properties.
        /// </summary>
        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, PropertyChangedEventArgsCache.Instance.Get(property));
        }

        /// <summary>
        /// Makes <see cref="AsyncCommand"/> listen on PropertyChanged events of some object,
        /// so that <see cref="AsyncCommand"/> can update its <see cref="CanExecute"/>
        /// </summary>
        public AsyncCommand ListenOn<TObserved, TProperty>(TObserved observedObject, Expression<Func<TObserved, TProperty>> expression)
            where TObserved : INotifyPropertyChanged
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException("Please provide a lambda expression like 'n => n.PropertyName'");

            var memberInfo = memberExpression.Member;
            if (string.IsNullOrEmpty(memberInfo.Name))
                throw new ArgumentException("'expression' did not provide a property name.");

            PropertyChangedEventManager.AddHandler(observedObject, ListenerPropertyChangedHandler, memberInfo.Name);

            return this;
        }

        /// <summary>
        /// Event Handler for all property changes registered with <see cref="ListenOn{TObserved,TProperty}"/>
        /// </summary>
        private void ListenerPropertyChangedHandler(object sender, PropertyChangedEventArgs args)
        {
            RaiseCanExecuteChanged();
        }
    }
}
