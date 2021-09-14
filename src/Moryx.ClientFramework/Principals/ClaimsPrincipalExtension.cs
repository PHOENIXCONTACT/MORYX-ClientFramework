// Copyright (c) 2021, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System;

namespace Moryx.ClientFramework.Principals
{
    /// <summary>
    /// Helper to inform the UI about an update of the ClaimsPrincipal
    /// </summary>
    public class ClaimsPrincipalExtension
    {
        /// <summary>
        /// Event to get informed about an update of the ClaimsPrincipal
        /// </summary>
        public static event EventHandler ClaimsPrincipalChanged;

        /// <summary>
        /// Method to invoke an event after an update of the ClaimsPrincipal
        /// </summary>
        public static void OnClaimsPrincipalChanged()
        {
            ClaimsPrincipalChanged?.Invoke(typeof(ClaimsPrincipalExtension), EventArgs.Empty);
        }
    }
}
