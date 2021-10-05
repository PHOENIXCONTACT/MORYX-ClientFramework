using Moryx.Identity;

namespace Moryx.ClientFramework.Kernel
{
    /// <summary>
    /// Extensions for the <see cref="HeartOfLead"/>
    /// </summary>
    public static class HeartOfLeadExtension
    {
        /// <summary>
        /// Method to register a custom ClaimsAuthorizationManager
        /// </summary>
        public static void EnableAuthorization(this HeartOfLead hol, IAuthorizationContext authorizationContext)
        {
            IdentityConfiguration.CurrentContext = authorizationContext;
        }
    }
}
