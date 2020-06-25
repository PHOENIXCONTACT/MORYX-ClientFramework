// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;
using System.Collections.Generic;
using Moryx.Container;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Base implementation of the user info provider. 
    /// The provider stores general user based information
    /// </summary>
    [KernelComponent(typeof(IUserInfoProvider))]
    public class UserInfoProvider : IUserInfoProvider
    {
        #region Fields and Properties

        private bool _isInitialized;

        ///
        public string UserName { get; private set; } = "Unknown";

        ///
        public List<string> Groups { get; private set; } = new List<string>();

        ///
        public string FirstName { get; private set; } = "Unknown";

        ///
        public string LastName { get; private set; } = "Unknown";

        ///
        public string FullName => $"{LastName}, {FirstName}";

        #endregion

        ///
        public void InitializeOnce(string userName, List<string> groups, string firstName, string lastName)
        {
            if (_isInitialized)
            {
                throw new TypeInitializationException(GetType().Name, new Exception("UserInfoProvider is already initialized."));
            }

            UserName = userName;
            Groups = groups;
            FirstName = firstName;
            LastName = lastName;
           
            _isInitialized = true;
        }
    }
}
