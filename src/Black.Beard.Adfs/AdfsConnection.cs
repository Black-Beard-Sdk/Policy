using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Black.Beard.Adfs
{
    /// <summary>
    /// Manages connections to Active Directory Federation Services (ADFS).
    /// </summary>
    /// <remarks>
    /// This class provides functionality for establishing and managing connections to ADFS,
    /// as well as performing common operations such as creating users and managing their properties.
    /// </remarks>
    public class AdfsConnection : IDisposable
    {
        private readonly string _serverUrl;
        private readonly string _domain;
        private readonly string _username;
        private readonly string _password;
        private PrincipalContext _principalContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdfsConnection"/> class.
        /// </summary>
        /// <param name="serverUrl">URL of the ADFS server. Cannot be null.</param>
        /// <param name="domain">Domain name for authentication. Cannot be null.</param>
        /// <param name="username">Username for authentication. Cannot be null.</param>
        /// <param name="password">Password for authentication. Cannot be null.</param>
        /// <remarks>
        /// This constructor initializes the connection properties but does not establish the connection.
        /// The connection is established when the <see cref="Connect"/> method is called or when the
        /// <see cref="PrincipalContext"/> property is accessed for the first time.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when serverUrl, domain, username, or password is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a new connection to ADFS
        /// var connection = new AdfsConnection(
        ///     "adfs.company.com", 
        ///     "DC=company,DC=com", 
        ///     "administrator", 
        ///     "password123");
        /// </code>
        /// </example>
        public AdfsConnection(string serverUrl, string domain, string username, string password)
        {
            _serverUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
            _domain = domain ?? throw new ArgumentNullException(nameof(domain));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

        /// <summary>
        /// Establishes a connection to the ADFS server.
        /// </summary>
        /// <returns>True if the connection was successfully established; otherwise, false.</returns>
        /// <remarks>
        /// This method attempts to create a new PrincipalContext using the connection parameters
        /// provided in the constructor. If an exception occurs during the connection attempt,
        /// it is caught, logged to the console, and the method returns false.
        /// </remarks>
        /// <exception cref="PrincipalServerDownException">Thrown when the ADFS server cannot be reached.</exception>
        /// <see cref="bool"/>
        /// <example>
        /// <code lang="C#">
        /// var connection = new AdfsConnection("adfs.company.com", "DC=company,DC=com", "administrator", "password123");
        /// 
        /// if (connection.Connect())
        /// {
        ///     Console.WriteLine("Successfully connected to ADFS server.");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("Failed to connect to ADFS server.");
        /// }
        /// </code>
        /// </example>
        public bool Connect()
        {
            try
            {
                _principalContext = new PrincipalContext(
                    ContextType.Domain,
                    _serverUrl,
                    _domain,
                    _username,
                    _password);

                return _principalContext != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de connexion à ADFS: {ex.Message}");
                return false;
            }
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
        public PrincipalContext PrincipalContext
        {
            get
            {
                if (_principalContext == null)
                {
                    Connect();
                }
                return _principalContext;
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
        public DirectoryEntry GetDirectoryEntry()
        {
            string ldapPath = $"LDAP://{_serverUrl}/{_domain}";
            return new DirectoryEntry(ldapPath, _username, _password);
        }

        /// <summary>
        /// Creates an API key, generates credentials, and creates a user in Active Directory.
        /// </summary>
        /// <param name="salt">Salt value for generating secure identifiers. Can be null or empty.</param>
        /// <param name="firstname">First name of the user. Cannot be null or empty.</param>
        /// <param name="lastname">Last name of the user. Cannot be null or empty.</param>
        /// <param name="email">Email address of the user. Cannot be null or empty.</param>
        /// <param name="organisation">Organisation the user belongs to. Cannot be null or empty.</param>
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
        public string[] CreateApikey(string salt, string firstname, string lastname, string email, string organisation, params string[] groupNames)
        {
            var apiKeys = ApiKeyGenerator
                .GenerateApiKey()
                .GenerateIdentifiers(salt);

            string apiKey = apiKeys[0];
            string login = apiKeys[1];
            string pwd = apiKeys[2];

            var user = CreateUser(login, pwd, firstname, lastname, email, organisation, 0, c =>
            {
                bool changed = false;

                var principal = c.UserPrincipal;

                if (groupNames != null && groupNames.Length > 0)
                {
                    c.AddGroups(groupNames);
                    changed = true;
                }

                return changed;
            });

            return [apiKey, login];
        }

        /// <summary>
        /// Creates a new user in Active Directory with the specified properties.
        /// </summary>
        /// <param name="username">Username for the new account. Cannot be null or empty.</param>
        /// <param name="password">Password for the new account. Cannot be null.</param>
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
        public AdfsUser CreateUser(string username, string password, string firstName, string lastName, string email, string organization,
            int passwordExpiryDays = 0, Func<AdfsUser, bool> actionAdfsUser = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be null or empty", nameof(username));

            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("First name cannot be null or empty", nameof(firstName));

            if (string.IsNullOrEmpty(lastName))
                throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));

            if (string.IsNullOrEmpty(email))
                throw new ArgumentException("Email cannot be null or empty", nameof(email));

            if (password == null)
                throw new ArgumentNullException(nameof(password));

            string fullName = $"{firstName} {lastName}";
            UserPrincipal newUser = new UserPrincipal(this._principalContext)
            {
                SamAccountName = username,
                Name = fullName,
                GivenName = firstName,
                Surname = lastName,
                DisplayName = $"{fullName} ({organization})",
                Description = $"User from {organization}",
                EmailAddress = email,
                UserPrincipalName = $"{username}@{_principalContext.ConnectedServer}",
                Enabled = true,
            };

            newUser.SetPassword(password);

            // Configurer l'expiration du mot de passe
            if (passwordExpiryDays > 0)
            {
                // Définir la date d'expiration du mot de passe
                DateTime expirationDate = DateTime.Now.AddDays(passwordExpiryDays);
                newUser.PasswordNotRequired = false;
                newUser.PasswordNeverExpires = false;

                // On ne peut pas directement définir la date d'expiration du mot de passe avec UserPrincipal
                // On doit utiliser DirectoryEntry pour cela
                DirectoryEntry userEntry = newUser.GetUnderlyingObject() as DirectoryEntry;
                if (userEntry != null)
                {
                    // Convertir la date en format FILETIME (100-nanoseconde intervals depuis le 1er janvier 1601)
                    long fileTime = expirationDate.ToFileTime();
                    userEntry.Properties["pwdLastSet"].Value = fileTime;
                }
            }
            else
            {
                // Si passwordExpiryDays est 0 ou négatif, le mot de passe n'expire jamais
                newUser.PasswordNeverExpires = true;
            }

            // Sauvegarder l'utilisateur dans l'annuaire
            newUser.Save();

            var result = new AdfsUser(newUser);
            if (actionAdfsUser != null)
                if (actionAdfsUser(result))
                    result.Save();

            return result;
        }

        /// <summary>
        /// Releases all resources used by the AdfsConnection.
        /// </summary>
        /// <remarks>
        /// This method disposes the PrincipalContext if it has been created, releasing any resources it holds.
        /// After calling Dispose, do not attempt to use any methods or properties of this instance.
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
        public void Dispose()
        {
            _principalContext?.Dispose();
        }
    }
}
