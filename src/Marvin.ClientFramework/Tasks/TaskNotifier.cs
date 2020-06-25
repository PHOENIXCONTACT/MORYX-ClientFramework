using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;

namespace Marvin.ClientFramework.Tasks
{
    /// <summary>
    /// Base class for the notifying task notifier
    /// </summary>
    /// <typeparam name="TTaskType">The type of the task</typeparam>
    public abstract class TaskNotifierBase<TTaskType> : INotifyPropertyChanged
        where TTaskType : Task
    {
        /// <summary>
        /// Initializes a task notifier watching the specified task.
        /// </summary>
        /// <param name="task">The task to watch.</param>
        protected TaskNotifierBase(TTaskType task)
        {
            Task = task;
            TaskCompleted = task.IsCompleted ? TaskConstants.CompletedTask() : MonitorTaskAsync(task);
        }

        /// <summary>
        /// Gets the task being watched. This property never changes and is never <c>null</c>.
        /// </summary>
        public TTaskType Task { get; protected set; }

        /// <summary>
        /// Gets a task that completes successfully when <see cref="Task"/> completes (successfully, faulted, or canceled). 
        /// This property never changes and is never <c>null</c>.
        /// </summary>
        public Task TaskCompleted { get; protected set; }

        /// <summary>
        /// Awaits the task asynchronous and notifies all properties if the task is failed or success.
        /// </summary>
        protected async Task MonitorTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            // ReSharper disable once EmptyGeneralCatchClause
            catch (Exception)
            {
                //dont throw anything here, we will catch the 
                //exception in the task
            }
            finally
            {
                NotifyProperties(task);
            }
        }

        /// <summary>
        /// Gets the current task status. This property raises a notification when the task completes.
        /// </summary>
        public TaskStatus Status => Task.Status;

        /// <summary>
        /// Gets whether the task has completed. 
        /// This property raises a notification when the value changes to <c>true</c>.
        /// </summary>
        public bool IsCompleted => Task.IsCompleted;

        /// <summary>
        /// Gets whether the task is busy (not completed). 
        /// This property raises a notification when the value changes to <c>false</c>.
        /// </summary>
        public bool IsNotCompleted => !Task.IsCompleted;

        /// <summary>
        /// Gets the visibility whether the task is busy (not completed). 
        /// This property raises a notification when the value changed
        /// </summary>
        [Obsolete("This view property will be removed in the future versions. Use IsCompleted.")]
        public Visibility BusyVisibility => !Task.IsCompleted ? Visibility.Visible : Visibility.Collapsed;

        /// <summary>
        /// Gets whether the task has been canceled. 
        /// This property raises a notification only if the task is canceled (i.e., if the value changes to <c>true</c>).
        /// </summary>
        public bool IsCanceled => Task.IsCanceled;

        /// <summary>
        /// Gets whether the task has faulted. 
        /// This property raises a notification only if the task faults (i.e., if the value changes to <c>true</c>).
        /// </summary>
        public bool IsFaulted => Task.IsFaulted;

        /// <summary>
        /// Gets whether the task has completed successfully. 
        /// This property raises a notification when the value changes to <c>true</c>.
        /// </summary>
        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;


        /// <summary>
        /// Gets the wrapped faulting exception for the task. Returns <c>null</c> if the task is not faulted. 
        /// This property raises a notification only if the task faults (i.e., if the value changes to non-<c>null</c>).
        /// </summary>
        public AggregateException Exception => Task.Exception;

        /// <summary>
        /// Gets the original faulting exception for the task. Returns <c>null</c> if the task is not faulted. 
        /// This property raises a notification only if the task faults (i.e., if the value changes to non-<c>null</c>).
        /// </summary>
        public Exception InnerException => Exception?.InnerException;

        /// <summary>
        /// Gets the error message for the original faulting exception for the task. 
        /// Returns <c>null</c> if the task is not faulted. 
        /// This property raises a notification only if the task faults (i.e., if the value changes to non-<c>null</c>).
        /// </summary>
        public string ErrorMessage => InnerException?.Message;

        /// <summary>
        /// Notifies the current properties.
        /// </summary>
        protected virtual void NotifyProperties(Task task)
        {
            if (task.IsCanceled)
            {
                RaisePropertyChanged("Status", "IsCanceled");
            }
            else if (task.IsFaulted)
            {
                RaisePropertyChanged("Exception", "InnerException", "ErrorMessage", "Status", "IsFaulted");
            }
            else
            {
                RaisePropertyChanged("Status", "IsSuccessfullyCompleted");
            }
            RaisePropertyChanged("IsCompleted", "IsNotCompleted", "BusyVisibility");
        }


        ///
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the property changed via the event cache
        /// Can be multiple properties
        /// </summary>
        protected void RaisePropertyChanged(params string[] properties)
        {
            if (PropertyChanged == null)
                return;

            foreach (var property in properties)
            {
                PropertyChanged(this, PropertyChangedEventArgsCache.Instance.Get(property));
            }
        }


    }

    /// <summary>
    /// Watches a task and raises property-changed notifications when the task completes.
    /// </summary>
    public sealed class TaskNotifier : TaskNotifierBase<Task>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskNotifier"/> class.
        /// </summary>
        public TaskNotifier(Task task) : base(task)
        {

        }

        /// <summary>
        /// Creates a new task notifier watching the specified task.
        /// </summary>
        /// <param name="task">The task to watch.</param>
        public static TaskNotifier Create(Task task)
        {
            return new TaskNotifier(task);
        }

        /// <summary>
        /// Creates a new task notifier watching the specified task.
        /// </summary>
        public static TaskNotifier<TResult> Create<TResult>(Task<TResult> task)
        {
            return Create(task, default(TResult));
        }

        /// <summary>
        /// Creates a new task notifier watching the specified task.
        /// </summary>
        public static TaskNotifier<TResult> Create<TResult>(Task<TResult> task, TResult defaultResult)
        {
            return new TaskNotifier<TResult>(task, defaultResult);
        }

        /// <summary>
        /// Executes the specified asynchronous code and creates a new task notifier watching the returned task.
        /// </summary>
        /// <param name="asyncAction">The asynchronous code to execute.</param>
        public static TaskNotifier Create(Func<Task> asyncAction)
        {
            return Create(asyncAction());
        }

        /// <summary>
        /// Executes the specified asynchronous code and creates a new task notifier watching the returned task.
        /// </summary>
        public static TaskNotifier<TResult> Create<TResult>(Func<Task<TResult>> asyncAction)
        {
            return Create(asyncAction, default(TResult));
        }

        /// <summary>
        /// Executes the specified asynchronous code and creates a new task notifier watching the returned task.
        /// </summary>
        public static TaskNotifier<TResult> Create<TResult>(Func<Task<TResult>> asyncAction, TResult defaultResult)
        {
            return Create(asyncAction(), defaultResult);
        }
    }

    /// <summary>
    /// Watches a task and raises property-changed notifications when the task completes.
    /// </summary>
    /// <typeparam name="TResult">The type of the task result.</typeparam>
    public sealed class TaskNotifier<TResult> : TaskNotifierBase<Task<TResult>>
    {
        /// <summary>
        /// The "result" of the task when it has not yet completed.
        /// </summary>
        private readonly TResult _defaultResult;

        /// <summary>
        /// Initializes a task notifier watching the specified task.
        /// </summary>
        /// <param name="task">The task to watch.</param>
        /// <param name="defaultResult">The default result.</param>
        internal TaskNotifier(Task<TResult> task, TResult defaultResult) : base(task)
        {
            _defaultResult = defaultResult;
        }

        /// <summary>
        /// Notifies the properties and additionaly the result if the task was success
        /// </summary>
        protected override void NotifyProperties(Task task)
        {
            base.NotifyProperties(task);

            if (!task.IsCanceled && !task.IsFaulted)
                RaisePropertyChanged("Result");
        }

        /// <summary>
        /// Gets the result of the task. Returns the "default result" value specified in the constructor if the task has not yet completed successfully. 
        /// This property raises a notification when the task completes successfully.
        /// </summary>
        public TResult Result => Task.Status == TaskStatus.RanToCompletion ? Task.Result : _defaultResult;
    }
}