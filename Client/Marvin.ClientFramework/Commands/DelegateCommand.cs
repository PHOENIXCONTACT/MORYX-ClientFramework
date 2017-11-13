using System;
using System.Windows.Input;

namespace Marvin.ClientFramework.Commands
{
    /// <summary>
    /// A command whose sole purpose is to relay its functionality 
    /// to other objects by invoking delegates. The default return value for the CanExecute method is 'true'.
    /// </summary>
    public class DelegateCommand : ICommand
    {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        private readonly WeakCanExecuteChanged _canExecuteChanged;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            
            _execute = execute;
            _canExecute = canExecute;
            _canExecuteChanged = new WeakCanExecuteChanged(this);
        }

        #endregion 

        /// <summary>
        /// Raises <see cref="ICommand.CanExecuteChanged"/>.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            _canExecuteChanged.RaiseCanExecuteChanged();
        }

        ///
        event EventHandler ICommand.CanExecuteChanged
        {
            add { _canExecuteChanged.CanExecuteChanged += value; }
            remove { _canExecuteChanged.CanExecuteChanged -= value; }
        }

        ///
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        ///
        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}