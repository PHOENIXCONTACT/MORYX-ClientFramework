using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Marvin.ClientFramework.Kernel.Tests
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
            _groups = new List<string>
            {
                @"europe\pbbd82"
            };
            _userName = "pbbd28";
            _firstName = "Dennis";
            _lastName = "Beuchler";
        }

        [Test]
        public void InitializeTest()
        {
            _userInfoProvider.InitializeOnce(_userName, _groups, _firstName, _lastName);

            Assert.AreEqual(_userName, _userInfoProvider.UserName);
            Assert.AreEqual(_firstName, _userInfoProvider.FirstName);
            Assert.AreEqual(_lastName, _userInfoProvider.LastName);
            Assert.AreEqual(_groups.Count, _userInfoProvider.Groups.Count);

            Assert.AreEqual(string.Format("{0}, {1}", _lastName, _firstName), _userInfoProvider.FullName);
        }

        [Test]
        public void DoubleInitiliazetest()
        {
            _userInfoProvider.InitializeOnce(_userName, _groups, _firstName, _lastName);
            Assert.Throws(typeof (TypeInitializationException), () => _userInfoProvider.InitializeOnce(_userName, _groups, _firstName, _lastName));
        }
    }
}