using System.Collections.Generic;
using Marvin.Modules;
using NUnit.Framework;

namespace Marvin.ClientFramework.Kernel.Tests
{
    [TestFixture]
    public class ComponentInitializerTests
    {
        private ComponentInitializer _initializer;

        [SetUp]
        public void SetUp()
        {
            _initializer = new ComponentInitializer
            {
                Initializables = new List<IInitializable>
                {
                    new InitializableMock(false),
                    new PriorizedInitializableMock(RunLevel.R4),
                    new PriorizedInitializableMock(RunLevel.R0),
                    new PriorizedInitializableMock(RunLevel.R3)
                }
            };
        }

        [Test]
        public void InitializeTest()
        {
            //TODO: find a way to test the initializer
            //var resetEvent = new ManualResetEvent(false);

            //_initializer.Completed += delegate(object sender, EventArgs args)
            //{
            //    resetEvent.Set();
            //};

            //ThreadPool.QueueUserWorkItem(delegate(object state)
            //{
            //    _initializer.Initialize();

                
            //});

            
            //resetEvent.WaitOne();

            //foreach (var initializable in _initializer.Initializables)
            //{
            //    Assert.AreEqual(((InitializableMock)initializable).IsInitialized, true);
            //}
        }
    }
}