using System.Collections.Generic;

namespace Marvin.ClientFramework
{
    /// <summary>
    /// Provider for the modules to get the most general user information
    /// </summary>
    public interface IUserInfoProvider
    {
        /// <summary>
        /// Initializes this component just one times.
        /// </summary>
        /// <param name="userName">The current user name</param>
        /// <param name="groups">Current setted groups</param>
        /// <param name="firstName">The first name of the user</param>
        /// <param name="lastName">The last name of the user</param>
        /// <exception cref="System.TypeInitializationException">UserInfoProvider is already initialized.</exception>
        void InitializeOnce(string userName, List<string> groups, string firstName, string lastName);

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        List<string> Groups { get; }

        /// <summary>
        /// Gets the firstname of the user
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// Gets the lastname of the user
        /// </summary>
        string LastName { get; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        string FullName { get; }
    }
}