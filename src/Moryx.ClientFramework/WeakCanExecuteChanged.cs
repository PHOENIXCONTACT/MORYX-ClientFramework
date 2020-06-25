// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using Moryx.Collections;

namespace Moryx.ClientFramework
{
    /// <summary>
    /// Class that implements <c>ICommand.CanExecuteChanged</c> as a weak event.
    /// </summary>
    internal sealed class WeakCanExecuteChanged
    {
        /// <summary>
        /// The sender of the <c>ICommand.CanExecuteChanged</c> event.
        /// </summary>
        private readonly object _sender;

        /// <summary>
        /// The weak collection of delegates for <see cref="CanExecuteChanged"/>.
        /// </summary>
        private readonly WeakCollection<EventHandler> _canExecuteChanged = new WeakCollection<EventHandler>();

        /// <summary>
        /// Creates a new weak-event implementation of <c>ICommand.CanExecuteChanged</c>.
        /// </summary>
        public WeakCanExecuteChanged(object sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// This is a weak event. Provides notification that the 
        /// result of <c>ICommand.CanExecute</c> may be different.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { _canExecuteChanged.Add(value); }
            remove { _canExecuteChanged.Remove(value); }
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event for any listeners still alive, 
        /// and removes any references to garbage collected listeners.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            foreach (var canExecuteChanged in _canExecuteChanged.GetLiveItems())
            {
                canExecuteChanged(_sender, EventArgs.Empty);
            }
        }
    }
}
