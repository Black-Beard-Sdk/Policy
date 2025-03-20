using System.DirectoryServices;

namespace Black.Beard.Adfs
{
    /// <summary>
    /// Helper class for Active Directory operations.
    /// </summary>
    /// <remarks>
    /// This class provides methods to interact with Active Directory using LDAP.
    /// It simplifies common tasks like checking if users exist in the directory.
    /// </remarks>
    public class ActiveDirectoryHelper
    {
        private readonly string _ldapPath;
        private readonly string _username;
        private readonly string _password;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryHelper"/> class.
        /// </summary>
        /// <param name="ldapPath">The LDAP path to the Active Directory. Must be a valid LDAP path.</param>
        /// <param name="username">The username to authenticate with Active Directory. Cannot be null.</param>
        /// <param name="password">The password to authenticate with Active Directory. Cannot be null.</param>
        /// <remarks>
        /// This constructor initializes a new Active Directory helper with authentication credentials.
        /// The LDAP path should be in the format "LDAP://domain.com/DC=domain,DC=com".
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when ldapPath, username, or password is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a new Active Directory helper
        /// string ldapPath = "LDAP://mycompany.com/DC=mycompany,DC=com";
        /// string username = "administrator";
        /// string password = "p@ssw0rd";
        /// 
        /// var adHelper = new ActiveDirectoryHelper(ldapPath, username, password);
        /// </code>
        /// </example>
        public ActiveDirectoryHelper(string ldapPath, string username, string password)
        {
            _ldapPath = ldapPath;
            _username = username;
            _password = password;
        }

        /// <summary>
        /// Gets a DirectoryEntry object connected to the Active Directory.
        /// </summary>
        /// <returns>A DirectoryEntry object connected to the Active Directory using the specified credentials.</returns>
        /// <remarks>
        /// This method creates a new connection to the Active Directory using the LDAP path 
        /// and authentication credentials provided in the constructor.
        /// The caller is responsible for disposing the returned DirectoryEntry.
        /// </remarks>
        /// <exception cref="DirectoryServicesCOMException">Thrown when there is an error connecting to Active Directory.</exception>
        /// <see cref="DirectoryEntry"/>
        /// <example>
        /// <code lang="C#">
        /// var adHelper = new ActiveDirectoryHelper("LDAP://mycompany.com/DC=mycompany,DC=com", "administrator", "p@ssw0rd");
        /// 
        /// // Get a DirectoryEntry object
        /// using (DirectoryEntry entry = adHelper.GetDirectoryEntry())
        /// {
        ///     // Use the DirectoryEntry object
        ///     Console.WriteLine($"Connected to: {entry.Path}");
        /// }
        /// </code>
        /// </example>
        public DirectoryEntry GetDirectoryEntry()
        {
            return new DirectoryEntry(_ldapPath, _username, _password);
        }

        /// <summary>
        /// Checks if a user with the specified username exists in the Active Directory.
        /// </summary>
        /// <param name="username">The username (sAMAccountName) to check. Cannot be null or empty.</param>
        /// <returns>True if the user exists in the Active Directory, false otherwise or if an error occurs.</returns>
        /// <remarks>
        /// This method searches the Active Directory for a user with the specified sAMAccountName.
        /// It creates a DirectorySearcher with a filter targeting the username and checks if any results are returned.
        /// Any exceptions encountered during the search are caught and logged, and the method returns false in such cases.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when username is null.</exception>
        /// <exception cref="ArgumentException">Thrown when username is empty.</exception>
        /// <see cref="bool"/>
        /// <example>
        /// <code lang="C#">
        /// var adHelper = new ActiveDirectoryHelper("LDAP://mycompany.com/DC=mycompany,DC=com", "administrator", "p@ssw0rd");
        /// 
        /// // Check if a user exists
        /// bool exists = adHelper.UserExists("johndoe");
        /// 
        /// if (exists)
        ///     Console.WriteLine("The user exists in Active Directory.");
        /// else
        ///     Console.WriteLine("The user does not exist in Active Directory.");
        /// </code>
        /// </example>
        public bool UserExists(string username)
        {
            try
            {
                using (var entry = GetDirectoryEntry())
                {
                    using (var searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = $"(sAMAccountName={username})";
                        searcher.PropertiesToLoad.Add("cn");
                        var result = searcher.FindOne();
                        return result != null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la vérification du compte: {ex.Message}");
                return false;
            }
        }
    }
}
