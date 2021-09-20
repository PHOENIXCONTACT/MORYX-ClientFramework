using System;
using System.Configuration;
using System.IdentityModel.Configuration;
using System.IdentityModel.Services;
using System.Security.Claims;

namespace Moryx.ClientFramework.Kernel
{
    public static class HeartOfLeadExtension
    {
        /// <summary>
        /// Method to register a custom ClaimsAuthorizationManager
        /// </summary>
        public static void EnableAuthorization(ClaimsAuthorizationManager authorizationManager)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var sectionName = "system.identityModel";
            try
            {
                if (config.Sections.Get(sectionName) == null)
                {
                    config.Sections.Add(sectionName, new SystemIdentityModelSection());
                    config.Save();
                    ConfigurationManager.RefreshSection(sectionName);
                }

                FederatedAuthentication.FederationConfiguration.IdentityConfiguration.ClaimsAuthorizationManager = authorizationManager;
            }
            catch (Exception e)
            {
                //Error during authorization preparation
                throw;
            }
        }

        /// <summary>
        /// Method to authorize the current principal to perform every action on any resource
        /// </summary>
        public static void AuhtorizeEverything(this HeartOfLead hol)
        {
            EnableAuthorization(null);
        }
    }
}
