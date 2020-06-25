namespace Marvin.ClientFramework.Kernel.Tests
{
    internal class PriorizedInitializableMock : InitializableMock, IPriorizedInitialize
    {
        public RunLevel RunLevel { get; private set; }

        public PriorizedInitializableMock(RunLevel runlevel) : base(false)
        {
            RunLevel = runlevel;
        }
    }
}