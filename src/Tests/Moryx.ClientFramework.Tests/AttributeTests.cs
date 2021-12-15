// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Reflection;
using Moryx.Container;
using NUnit.Framework;
using LifeCycle = Moryx.Container.LifeCycle;

namespace Moryx.ClientFramework.Tests
{
    [TestFixture]
    public class AttributeTests
    {
        [Test]
        [TestCase(typeof(ClientModuleAttribute), LifeCycle.Singleton, typeof(IClientModule), AttributeTargets.Class)]
        [TestCase(typeof(ModuleShellAttribute), LifeCycle.Singleton, typeof(IClientModule), AttributeTargets.Class)]
        public void ClientModuleAttributeTest(Type attribute, LifeCycle lifeCycle, Type service, AttributeTargets target)
        {
            const string name = "HelloWorld";

            var att = (RegistrationAttribute)Activator.CreateInstance(attribute, name);

            Assert.AreEqual(att.Name, name, "Name was not equal");
            Assert.AreEqual(att.LifeStyle, lifeCycle, "Component will not be registerd as " + lifeCycle);

            var decorator = typeof(ClientModuleAttribute).GetCustomAttribute<AttributeUsageAttribute>();

            Assert.AreEqual(decorator.ValidOn, target, "Attribute is not valid on: " + target);
        }
    }
}
