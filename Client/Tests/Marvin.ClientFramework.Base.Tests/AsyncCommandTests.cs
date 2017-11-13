using System;
using System.Threading;
using System.Threading.Tasks;
using Marvin.ClientFramework.Commands;
using NUnit.Framework;

namespace Marvin.ClientFramework.Base.Tests
{
    [TestFixture]
    public class AsyncCommandTests
    {
        private async Task SuccessFunc(object parameters)
        {
            await Task.Run(() => Thread.Sleep(2));
        }

        private async Task LongSuccessFunc(object parameters)
        {
            await Task.Run(() => Thread.Sleep(200));
        }

        private async Task ExeptionFunc(object parameters)
        {
            await Task.Run(delegate
            {
                throw new TimeoutException("Timeout");
            });
        }

        [Test]
        public void InstanciationTest()
        {
            var command = new AsyncCommand(SuccessFunc);

            //Execution should be stopped
            Assert.IsFalse(command.IsExecuting);

            //Execution context should be null
            Assert.IsNull(command.Execution);

            var task = command.ExecuteAsync(null);
            var execution = command.Execution;

            //Check other execution parameters
            Assert.IsFalse(execution.IsCanceled);
            Assert.IsFalse(execution.IsCompleted);
            Assert.IsFalse(execution.IsFaulted);
            Assert.IsFalse(execution.IsSuccessfullyCompleted);
            Assert.IsTrue(execution.IsNotCompleted);
            Assert.AreEqual(TaskStatus.WaitingForActivation, execution.Status);
        }

        [Test]
        public async Task SuccessTaskTest()
        {
            var command = new AsyncCommand(SuccessFunc);
            
            await command.ExecuteAsync(null);

            Assert.AreEqual(TaskStatus.RanToCompletion, command.Execution.Status);
        }

        [Test]
        public async Task FaultedTastTest()
        {
            var command = new AsyncCommand(ExeptionFunc);

            var task = command.ExecuteAsync(null);

            // catch execption that the method will continue after fault
            try
            {
                await task;
            }
            catch (Exception)
            {
            }
            
            Assert.AreEqual(TaskStatus.Faulted, command.Execution.Status);
        }

        [Test]
        public async Task CanExecuteSimpleTest()
        {
            var command = new AsyncCommand(LongSuccessFunc);
            var task = command.ExecuteAsync(null);
            var execution = command.Execution;

            ThreadPool.QueueUserWorkItem(delegate
            {
                Thread.Sleep(50);

                Console.WriteLine("task ended");

                Assert.True(execution.IsNotCompleted);
            });

            await task;
        }
    }
}