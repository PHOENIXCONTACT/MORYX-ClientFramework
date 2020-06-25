using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Marvin.Container;
using Marvin.Modules;

namespace Marvin.ClientFramework.Kernel
{
    /// <summary>
    /// The component initializer will initialize all given components
    /// It will raises some events when the initialization phase begins and when it ends
    /// It will look for the <see cref="IPriorizedInitialize"/> to order the initialization components by the <see cref="RunLevel"/>
    /// </summary>
    [KernelComponent(typeof(IComponentInitializer))]
    public class ComponentInitializer : IComponentInitializer
    {
        #region Dependency Injection

        /// 
        public IEnumerable<IInitializable> Initializables { get; set; }

        #endregion

        /// 
        public void Initialize()
        {
            Starting?.Invoke(this, Initializables.Count());

            //sort components to have an order
            var normalCmps = Initializables.Where(c => !(c is IPriorizedInitialize)).ToList();
            var prioCmps = Initializables.Except(normalCmps).OrderBy(c => ((IPriorizedInitialize)c).RunLevel);

            var sortedComponents = new List<IInitializable>();
            sortedComponents.AddRange(prioCmps);
            sortedComponents.AddRange(normalCmps);

            ThreadPool.QueueUserWorkItem(state => InitializeComponents(sortedComponents));
        }

        /// <summary>
        /// Will initialize given the components.
        /// </summary>
        private void InitializeComponents(IEnumerable<IInitializable> components)
        {
            try
            {
                foreach (var component in components.ToList())
                {
                    var buffer = component;

                    if (InitializingComponent != null)
                    {
                        ThreadContext.BeginInvoke(() => InitializingComponent(this, buffer));
                    }

                    component.Initialize();
                }

                ThreadContext.BeginInvoke(RaiseCompleted);
            }
            catch (Exception)
            {
                //TODO: error handling
                throw;
            }
        }

        ///
        public event EventHandler<IInitializable> InitializingComponent;

        ///
        public event EventHandler<int> Starting;

        ///
        public event EventHandler Completed;

        /// <summary>
        /// Raises the completed event.
        /// </summary>
        private void RaiseCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}