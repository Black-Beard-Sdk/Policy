// Ignore Spelling: username firstname lastname Adfs Api

using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Bb.Helpers;
using Microsoft.Extensions.Logging;

namespace Bb.Adfs
{
    /// <summary>
    /// Manages connections to Active Directory Federation Services (ADFS).
    /// </summary>
    /// <remarks>
    /// This class provides functionality for establishing and managing connections to ADFS,
    /// as well as performing common operations such as creating users and managing their properties.
    /// </remarks>
    /// <example>
    /// <code lang="C#">
    /// // Create and use a connection
    /// using (var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123"))
    /// {
    ///     // Use the connection...
    ///     AdfsUser user = connection.CreateUser(...);
    ///     
    ///     // The connection will be automatically disposed when the using block exits
    /// }
    /// </code>
    /// </example>

    [SupportedOSPlatform("windows")]
    public class AdfsConnection : IDisposable
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AdfsConnection"/> class.
        /// </summary>
        /// <param name="logger">Optional logger for logging operations. If null, no logging will be performed.</param>
        /// <remarks>
        /// This constructor initializes the connection properties but does not establish the connection.
        /// The connection is established when the <see cref="Connect"/> method is called or when the
        /// <see cref="PrincipalContext"/> property is accessed for the first time.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when serverUrl, domain, username, or password is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a logger
        /// ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger&lt;AdfsConnection&gt;();
        /// 
        /// // Create a new connection to ADFS with logging
        /// var connection = new AdfsConnection(logger);
        /// </code>
        /// </example>
        public AdfsConnection(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Establishes a connection to the ADFS server.
        /// </summary>
        /// <param name="serverUrl">URL of the ADFS server. Cannot be null.</param>
        /// <param name="domain">Domain name for organization unit. Cannot be null.</param>
        /// <param name="username">Username for authentication. Cannot be null.</param>
        /// <param name="password">Password for authentication. Cannot be null.</param>
        /// <returns>True if the connection was successfully established; otherwise, false.</returns>
        /// <remarks>
        /// This method attempts to create a new PrincipalContext using the connection parameters
        /// provided in the constructor. If an exception occurs during the connection attempt,
        /// it is logged, and the method returns false.
        /// </remarks>
        /// <exception cref="PrincipalServerDownException">Thrown when the ADFS server cannot be reached.</exception>
        /// <see cref="bool"/>
        /// <example>
        /// <code lang="C#">
        /// 
        /// // Create a logger
        /// ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger&lt;AdfsConnection&gt;();
        /// 
        /// // Create a new connection to ADFS with logging
        /// var connection = new AdfsConnection(logger);
        /// 
        /// if (connection.Connect("adfs.company.com", "DC=company,DC=com", "administrator", "password123"))
        /// {
        ///     Console.WriteLine("Successfully connected to ADFS server.");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("Failed to connect to ADFS server.");
        /// }
        /// </code>
        /// </example>
        public bool Connect(string serverUrl, string domain, string username, string password)
        {

            try
            {
                _logger?.LogInformation("Attempting to connect to ADFS server {ServerUrl} with domain {Domain}", serverUrl, domain);

                _principalContext = new PrincipalContext(ContextType.Domain, serverUrl, domain, username, password);

                if (_principalContext != null)
                {
                    _logger?.LogInformation("Successfully connected to ADFS server {ServerUrl}", serverUrl);
                    return true;
                }

                _logger?.LogWarning("Failed to create PrincipalContext for ADFS server {ServerUrl}", serverUrl);
                return false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error connecting to ADFS server {ServerUrl}: {ErrorMessage}", serverUrl, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets a DirectoryEntry object for the ADFS connection.
        /// </summary>
        /// <returns>A new DirectoryEntry object for the ADFS server.</returns>
        /// <remarks>
        /// This method creates a new DirectoryEntry using the LDAP path constructed from the
        /// server URL and domain provided in the constructor. The DirectoryEntry provides
        /// lower-level access to the directory than the PrincipalContext.
        /// </remarks>
        /// <exception cref="DirectoryServicesCOMException">Thrown when there is an error connecting to the directory service.</exception>
        /// <see cref="DirectoryEntry"/>
        /// <example>
        /// <code lang="C#">
        /// var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123");
        /// 
        /// // Get a DirectoryEntry for direct LDAP operations
        /// using (DirectoryEntry entry = connection.GetDirectoryEntry())
        /// {
        ///     // Use the DirectoryEntry for LDAP operations
        ///     DirectorySearcher searcher = new DirectorySearcher(entry);
        ///     searcher.Filter = "(objectClass=user)";
        ///     SearchResultCollection results = searcher.FindAll();
        /// }
        /// </code>
        /// </example>
        public DirectoryEntry GetDirectoryEntry(string serverUrl, string domain, string username, string password)
        {
            string ldapPath = $"LDAP://{serverUrl}/{domain}";
            _logger?.LogDebug("Creating DirectoryEntry with LDAP path: {LdapPath}", ldapPath);
            return new DirectoryEntry(ldapPath, username, password);
        }

        /// <summary>
        /// Gets the PrincipalContext for the ADFS connection.
        /// </summary>
        /// <remarks>
        /// If the connection has not been established yet, this property attempts to establish it
        /// by calling the <see cref="Connect"/> method. This property provides access to the underlying
        /// PrincipalContext object, which can be used for advanced directory operations.
        /// </remarks>
        /// <exception cref="PrincipalServerDownException">Thrown when the ADFS server cannot be reached during connection.</exception>
        /// <see cref="PrincipalContext"/>
        /// <example>
        /// <code lang="C#">
        /// var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123");
        /// 
        /// // Access the PrincipalContext (will establish connection if not already connected)
        /// PrincipalContext context = connection.PrincipalContext;
        /// 
        /// // Use the context for directory operations
        /// UserPrincipal user = UserPrincipal.FindByIdentity(context, "johndoe");
        /// </code>
        /// </example>
        public PrincipalContext? PrincipalContext
        {
            get
            {
                if (_principalContext == null)
                    _logger?.LogDebug("PrincipalContext requested but not yet established. Connecting now...");
                return _principalContext;
            }
        }

        /// <summary>
        /// Gets length of the API key.
        /// </summary>
        public int ApiKeyLength { get; set; } = 100;

        /// <summary>
        /// Gets length of the login.
        /// </summary>
        public int LoginLength { get; set; } = 25;

        /// <summary>
        /// Gets length of the password.
        /// </summary>
        public int PasswordLength { get; set; } = 35;

        public string[] RenewApiKey(string salt, string payloadApiKey)
        {

            if (string.IsNullOrEmpty(payloadApiKey))
                throw new ArgumentException($"'{nameof(payloadApiKey)}' cannot be null or empty", nameof(payloadApiKey));

            CheckIsConnected();

            _logger?.LogInformation("Renewing API key for payload: {PayloadLength} characters", payloadApiKey.Length);

            try
            {

                var username = ApiKeyGenerator.ResolveLogin(payloadApiKey, this.LoginLength, salt);
                _logger?.LogDebug("Resolved username: {Username}", username);

                var user = GetUserByUsername(username);
                if (user == null)
                {
                    _logger?.LogWarning("User not found for username: {Username}", username);
                    throw new InvalidDataException($"User {nameof(username)}={username} not found");
                }

                _logger?.LogInformation("Generating new API key for user: {Username}", username);
                var apiKeys = ApiKeyGenerator
                    .GenerateApiKey(this.ApiKeyLength)
                    .GenerateIdentifiers(this.LoginLength, this.PasswordLength, salt);

                user.Login = apiKeys[1];
                user.SetPassword(apiKeys[2]);

                user.Save();

                _logger?.LogInformation("Successfully renewed API key for user: {Username}", username);
                return [apiKeys[0], apiKeys[1]];
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error renewing API key: {ErrorMessage}", ex.Message);
                throw;
            }

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void CheckIsConnected()
        {
            if (_principalContext == null)
            {
                _logger?.LogWarning("PrincipalContext is not established. Cannot renew API key.");
                throw new InvalidOperationException("PrincipalContext is not established. Cannot renew API key.");
            }
        }

        /// <summary>
        /// Finds a user in the Active Directory by username.
        /// </summary>
        /// <param name="username">The username (SamAccountName) to search for. Cannot be null or empty.</param>
        /// <returns>An AdfsUser object if the user is found; otherwise, null.</returns>
        /// <remarks>
        /// This method searches for a user in Active Directory using the specified username (SamAccountName).
        /// It wraps the found UserPrincipal in an AdfsUser object for easier manipulation.
        /// If no user with the specified username exists, the method returns null.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown when username is null or empty.</exception>
        /// <exception cref="PrincipalServerDownException">Thrown when the connection to the Active Directory server fails.</exception>
        /// <see cref="AdfsUser"/>
        /// <example>
        /// <code lang="C#">
        /// var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123");
        /// 
        /// // Find a user by username
        /// AdfsUser user = connection.GetUserByUsername("johndoe");
        /// 
        /// if (user != null)
        /// {
        ///     Console.WriteLine($"Found user: {user.DisplayName}");
        ///     
        ///     // Update user properties
        ///     user.Email = "john.new@example.com";
        ///     user.Save();
        /// }
        /// else
        /// {
        ///     Console.WriteLine("User not found");
        /// }
        /// </code>
        /// </example>
        public AdfsUser? GetUserByUsername(string username)
        {
            return GetUserBy(IdentityType.SamAccountName, username);
        }

        /// <summary>
        /// Finds a user in the Active Directory by specified criteria.
        /// </summary>
        /// <param name="identity">The type of identity to search for.</param>
        /// <param name="value">The value to search for. Cannot be null or empty.</param>
        /// <returns>An AdfsUser object if the user is found; otherwise, null.</returns>
        /// <remarks>
        /// This method searches for a user in Active Directory using the specified identity type and value.
        /// It wraps the found UserPrincipal in an AdfsUser object for easier manipulation.
        /// If no user matching the criteria exists, the method returns null.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown when value is null or empty.</exception>
        /// <exception cref="PrincipalServerDownException">Thrown when the connection to the Active Directory server fails.</exception>
        /// <see cref="AdfsUser"/>
        /// <example>
        /// <code lang="C#">
        /// var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123");
        /// 
        /// // Find a user by username
        /// AdfsUser user = connection.GetUserBy(IdentityType.SamAccountName, "johndoe");
        /// 
        /// if (user != null)
        /// {
        ///     Console.WriteLine($"Found user: {user.DisplayName}");
        ///     
        ///     // Update user properties
        ///     user.Email = "john.new@example.com";
        ///     user.Save();
        /// }
        /// else
        /// {
        ///     Console.WriteLine("User not found");
        /// }
        /// </code>
        /// </example>
        public AdfsUser? GetUserBy(IdentityType identity, string value)
        {

            CheckIsConnected();

            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"'{nameof(value)}' cannot be null or empty", nameof(value));

            try
            {
                _logger?.LogDebug("Searching for user with {Identity}: {Value}", identity, value);

                // Ensure we have a connection to the directory
                var context = this.PrincipalContext;

                // Search for the user by the specified identity type and value
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, identity, value);

                if (userPrincipal != null)
                {
                    _logger?.LogInformation("Found user {DisplayName} with {Identity}: {Value}",
                        userPrincipal.DisplayName, identity, value);
                    return new AdfsUser(userPrincipal);
                }

                _logger?.LogWarning("No user found with {Identity}: {Value}", identity, value);
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error finding user with {Identity}: {Value}. Error: {ErrorMessage}",
                    identity, value, ex.Message);
                throw; // Re-throw the exception after logging it
            }
        }

        /// <summary>
        /// Creates an API key, generates credentials, and creates a user in Active Directory.
        /// </summary>
        /// <param name="salt">Salt value for generating secure identifiers. Can be null or empty.</param>
        /// <param name="username">Username for display purposes.</param>
        /// <param name="firstname">First name of the user. Cannot be null or empty.</param>
        /// <param name="lastname">Last name of the user. Cannot be null or empty.</param>
        /// <param name="email">Email address of the user. Cannot be null or empty.</param>
        /// <param name="organization">Organization the user belongs to. Cannot be null or empty.</param>
        /// <param name="groupNames">Names of AD groups to add the user to. Can be empty but not null.</param>
        /// <returns>An array containing the API key and login for the created user.</returns>
        /// <remarks>
        /// This method generates a random API key, creates secure login credentials using the provided salt,
        /// and creates a new user in Active Directory with the generated credentials. The user is then
        /// added to the specified groups. The method returns both the API key and the login name.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown when firstname, lastname, or email is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when groupNames is null.</exception>
        /// <see cref="System.String[]"/>
        /// <example>
        /// <code lang="C#">
        /// var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123");
        /// 
        /// // Create a user with an API key and add them to two groups
        /// string[] credentials = connection.CreateApikey(
        ///     "mysalt123",
        ///     "John",
        ///     "Doe",
        ///     "john.doe@company.com",
        ///     "Marketing",
        ///     "MarketingUsers",
        ///     "ApiUsers");
        /// 
        /// string apiKey = credentials[0];
        /// string login = credentials[1];
        /// 
        /// Console.WriteLine($"Created user with API key: {apiKey} and login: {login}");
        /// </code>
        /// </example>
        public string[] CreateApiKey(string salt, string username, string firstname, string lastname, string email, string organization, params string[] groupNames)
        {

            CheckIsConnected();

            try
            {
                _logger?.LogInformation("Creating API key for user: {Username}, {FirstName} {LastName}", username, firstname, lastname);

                var apiKeys = ApiKeyGenerator
                    .GenerateApiKey(this.ApiKeyLength)
                    .GenerateIdentifiers(this.LoginLength, this.PasswordLength, salt);

                string apiKey = apiKeys[0];
                string login = apiKeys[1];
                string pwd = apiKeys[2];

                _logger?.LogDebug("Generated login for API key: {Login}", login);

                CreateUser(login, pwd, username, firstname, lastname, email, organization, 0, c =>
                {
                    bool changed = false;

                    if (groupNames != null && groupNames.Length > 0)
                    {
                        _logger?.LogDebug("Adding user {Login} to {GroupCount} groups", login, groupNames.Length);
                        c.AddGroups(groupNames);
                        changed = true;
                    }

                    return changed;
                });

                _logger?.LogInformation("Successfully created API key for user: {Username}", username);
                return [apiKey, login];
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating API key for user {Username}: {ErrorMessage}",
                    username, ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates a new user in Active Directory with the specified properties.
        /// </summary>
        /// <param name="login">Username for the new account. Cannot be null or empty.</param>
        /// <param name="password">Password for the new account. Cannot be null.</param>
        /// <param name="username">Display username for the account.</param>
        /// <param name="firstName">First name of the user. Cannot be null or empty.</param>
        /// <param name="lastName">Last name of the user. Cannot be null or empty.</param>
        /// <param name="email">Email address of the user. Cannot be null or empty.</param>
        /// <param name="organization">Organization the user belongs to. Cannot be null or empty.</param>
        /// <param name="passwordExpiryDays">Number of days until the password expires. If 0 or negative, password never expires.</param>
        /// <param name="actionAdfsUser">Optional action to perform on the user after creation. Can be null.</param>
        /// <returns>An AdfsUser object representing the newly created user.</returns>
        /// <remarks>
        /// This method creates a new user in Active Directory with the specified properties. If passwordExpiryDays is greater than 0,
        /// the password will be set to expire after the specified number of days. Otherwise, the password will never expire.
        /// After the user is created, the optional actionAdfsUser delegate is called, allowing additional operations to be performed.
        /// </remarks>
        /// <exception cref="ArgumentException">Thrown when username, firstName, lastName, or email is null or empty.</exception>
        /// <exception cref="ArgumentNullException">Thrown when password is null.</exception>
        /// <exception cref="PrincipalExistsException">Thrown when a user with the specified username already exists.</exception>
        /// <see cref="AdfsUser"/>
        /// <example>
        /// <code lang="C#">
        /// var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123");
        /// 
        /// // Create a new user with password expiring after 90 days and add them to a group
        /// AdfsUser user = connection.CreateUser(
        ///     "johndoe", 
        ///     "P@ssw0rd123", 
        ///     "John", 
        ///     "Doe", 
        ///     "john.doe@company.com", 
        ///     "Marketing", 
        ///     90,
        ///     u => {
        ///         u.AddGroups("MarketingUsers");
        ///         return true; // Indicate that changes were made
        ///     });
        /// 
        /// Console.WriteLine($"Created user: {user.DisplayName}");
        /// </code>
        /// </example>
        public AdfsUser CreateUser(string login, string password, string username, string firstName, string lastName,
            string email, string organization, int passwordExpiryDays = 0, Func<AdfsUser, bool>? actionAdfsUser = null)
        {

            CheckIsConnected();

            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("Login cannot be null or empty", nameof(login));

            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("First name cannot be null or empty", nameof(firstName));

            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            if (_principalContext == null || string.IsNullOrEmpty(_principalContext.ConnectedServer))
                throw new InvalidOperationException("PrincipalContext is not connected. Cannot create user.");

            try
            {
                _logger?.LogInformation("Creating user {Login} ({FirstName} {LastName}) from organization {Organization}",
                    login, firstName, lastName, organization);

                string fullName = $"{firstName} {lastName}";
                UserPrincipal newUser = new UserPrincipal(this._principalContext)
                {
                    SamAccountName = login,
                    Name = fullName,
                    GivenName = firstName,
                    Surname = lastName,
                    DisplayName = $"{organization}:{firstName}",
                    Description = $"User from {organization}",
                    EmailAddress = email,
                    UserPrincipalName = $"{username}@{_principalContext.ConnectedServer}",
                    Enabled = true,
                };

                newUser.SetPassword(password);

                // Configure password expiration
                if (passwordExpiryDays > 0)
                {
                    _logger?.LogDebug("Setting password to expire in {ExpiryDays} days for user {Login}",
                        passwordExpiryDays, login);

                    // Set the password expiration date
                    DateTime expirationDate = DateTime.Now.AddDays(passwordExpiryDays);
                    newUser.PasswordNotRequired = false;
                    newUser.PasswordNeverExpires = false;

                    // We can't directly set the password expiration date with UserPrincipal
                    // We need to use DirectoryEntry for that
                    DirectoryEntry? userEntry = newUser.GetUnderlyingObject() as DirectoryEntry;
                    if (userEntry != null)
                    {
                        // Convert the date to FILETIME format (100-nanosecond intervals since January 1, 1601)
                        long fileTime = expirationDate.ToFileTime();
                        userEntry.Properties["pwdLastSet"].Value = fileTime;
                    }
                }
                else
                {
                    _logger?.LogDebug("Setting password to never expire for user {Login}", login);
                    // If passwordExpiryDays is 0 or negative, the password never expires
                    newUser.PasswordNeverExpires = true;
                }

                // Save the user to the directory
                _logger?.LogDebug("Saving new user {Login} to directory", login);
                newUser.Save();

                var result = new AdfsUser(newUser);

                // Execute additional actions if provided
                if (actionAdfsUser != null)
                {
                    _logger?.LogDebug("Executing additional actions on user {Login}", login);
                    if (actionAdfsUser(result))
                    {
                        _logger?.LogDebug("Saving user {Login} after additional actions", login);
                        result.Save();
                    }
                }

                _logger?.LogInformation("Successfully created user {Login} ({FullName})", login, fullName);
                return result;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error creating user {Login}: {ErrorMessage}", login, ex.Message);
                throw;
            }
        }

        private readonly ILogger _logger;
        private PrincipalContext? _principalContext;
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing && _principalContext != null)
                {
                    _logger?.LogDebug("Disposing AdfsConnection");
                    _principalContext.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
