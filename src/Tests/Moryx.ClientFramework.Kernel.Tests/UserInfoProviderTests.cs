// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Moryx.ClientFramework.Kernel.Tests
{
    [TestFixture]
    public class UserInfoProviderTests
    {
        private UserInfoProvider _userInfoProvider;
        private List<string> _groups;
        private string _userName;
        private string _firstName;
        private string _lastName;

        [SetUp]
        public void SetUp()
        {
            _userInfoProvider = new UserInfoProvider();
            _userName = "summ01";
            _firstName = "Max";
            _lastName = "Mustermann";
            _groups = new List<string>
            {
                @"someGroup"
            };
            _userName = "someUser";
            _firstName = "Max";
            _lastName = "Mustermann";
        }

        [Test]
        public void InitializeTest()
        {
            _userInfoProvider.InitializeOnce(_userName, _groups, _firstName, _lastName);

            Assert.AreEqual(_userName, _userInfoProvider.UserName);
            Assert.AreEqual(_firstName, _userInfoProvider.FirstName);
            Assert.AreEqual(_lastName, _userInfoProvider.LastName);
            Assert.AreEqual(_groups.Count, _userInfoProvider.Groups.Count);

            Assert.AreEqual($"{_lastName}, {_firstName}", _userInfoProvider.FullName);
        }

        [Test]
        public void DoubleInitializeTest()
        {
            _userInfoProvider.InitializeOnce(_userName, _groups, _firstName, _lastName);
            Assert.Throws(typeof (TypeInitializationException), () => _userInfoProvider.InitializeOnce(_userName, _groups, _firstName, _lastName));
        }
    }
}
