// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using System.Windows;
using Moryx.Users;
using Moq;
using NUnit.Framework;

namespace Moryx.ClientFramework.Tests
{
    [TestFixture]
    public class MarkupExtensionTests
    {
        internal const string FullAccessOperation = "Full";
        internal const string DeniedAccessOperation = "Denied";
        internal const string LimitedReadAccessOperation = "LimitedRead";
        internal const string ReadOnlyAccessOperation = "ReadOnly";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            ApplicationAccesses.SetAccesses(new Dictionary<string, OperationAccess>
            {
                {DeniedAccessOperation, OperationAccess.Denied},
                {LimitedReadAccessOperation, OperationAccess.LimitedRead},
                {ReadOnlyAccessOperation, OperationAccess.ReadOnly},
                {FullAccessOperation, OperationAccess.Full},
            });
        }

        [Test]
        [TestCase(DeniedAccessOperation, false)]
        [TestCase(LimitedReadAccessOperation, true)]
        [TestCase(ReadOnlyAccessOperation, true)]
        [TestCase(FullAccessOperation, true)]
        public void AccessBoolExtensionTest(string access, bool result)
        {
            Assert.AreEqual(result, GetProvidedValue<bool>(typeof(AccessBoolExtension), access));
        }


        [Test]
        [TestCase(DeniedAccessOperation, GridUnitType.Pixel, 0)]
        [TestCase(LimitedReadAccessOperation, GridUnitType.Star, 1)]
        [TestCase(ReadOnlyAccessOperation, GridUnitType.Star, 1)]
        [TestCase(FullAccessOperation, GridUnitType.Star, 1)]
        public void AccessGridLenghtExtensionTest(string access, GridUnitType resultUnitType, double resultLength)
        {
            var value = GetProvidedValue<GridLength>(typeof(AccessGridLenghtExtension), access);

            Assert.AreEqual(value.Value, resultLength);
            Assert.AreEqual(value.GridUnitType, resultUnitType);
        }

        [Test]
        [TestCase(DeniedAccessOperation, Visibility.Collapsed)]
        [TestCase(LimitedReadAccessOperation, Visibility.Visible)]
        [TestCase(ReadOnlyAccessOperation, Visibility.Visible)]
        [TestCase(FullAccessOperation, Visibility.Visible)]
        public void AccessVisibilityExtensionTest(string access, Visibility result)
        {
            Assert.AreEqual(result, GetProvidedValue<Visibility>(typeof(AccessVisibilityExtension), access));
        }

        [Test]
        [TestCase(DeniedAccessOperation, Visibility.Visible)]
        [TestCase(LimitedReadAccessOperation, Visibility.Collapsed)]
        [TestCase(ReadOnlyAccessOperation, Visibility.Collapsed)]
        [TestCase(FullAccessOperation, Visibility.Collapsed)]
        public void AccessVisibilitySwapExtensionTest(string access, Visibility result)
        {
            Assert.AreEqual(result, GetProvidedValue<Visibility>(typeof(AccessVisibilitySwapExtension), access));
        }

        private static T GetProvidedValue<T>(Type extension, string operation)
        {
            var ext = (OperationAccessExtensionBase) Activator.CreateInstance(extension);

            ext.Operation = operation;

            var serviceProviderMock = new Mock<IServiceProvider>();
            var result = (T)ext.ProvideValue(serviceProviderMock.Object);

            return result;
        }
    }
}
