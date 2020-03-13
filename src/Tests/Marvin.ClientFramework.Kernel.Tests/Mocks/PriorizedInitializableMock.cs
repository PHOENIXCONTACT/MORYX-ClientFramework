// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

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
