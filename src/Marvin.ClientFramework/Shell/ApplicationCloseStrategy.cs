// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Marvin.ClientFramework.Behavior;

namespace Marvin.ClientFramework.Shell
{
    /// <inheritdoc />
    /// <summary>
    /// Closing strategy when application closes
    /// </summary>
    public class ApplicationCloseStrategy : ICloseStrategy<IClientModule>
    {
        private IEnumerator<IClientModule> _enumerator;
        private bool _finalResult;
        private Action<bool, IEnumerable<IClientModule>> _callback;

        /// <inheritdoc />
        public void Execute(IEnumerable<IClientModule> toClose, Action<bool, IEnumerable<IClientModule>> callback)
        {
            _enumerator = toClose.GetEnumerator();
            _callback = callback;
            _finalResult = true;

            Evaluate(_finalResult);
        }

        void Evaluate(bool result)
        {
            _finalResult = _finalResult && result;

            if (!_enumerator.MoveNext() || !result)
                _callback(_finalResult, new List<IClientModule>());
            else
            {
                var current = _enumerator.Current;
                // ReSharper disable once SuspiciousTypeConversion.Global
                var conductor = current as IConductor;
                if (conductor != null)
                {
                    var tasks = conductor.GetChildren()
                        .OfType<IHaveShutdownTask>()
                        .Select(x => x.GetShutdownTask())
                        .Where(x => x != null);

                    var sequential = new SequentialResult(tasks.GetEnumerator());
                    sequential.Completed += (s, e) =>
                    {
                        if (!e.WasCancelled)
                            Evaluate(!e.WasCancelled);
                    };
                    sequential.Execute(new CoroutineExecutionContext());
                }
                else Evaluate(true);
            }
        }
    }
}
