using System.DirectoryServices;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryHelper"/> class.
        /// </summary>
        /// <param name="ldapPath">The LDAP path to the Active Directory. Must be a valid LDAP path.</param>
        /// <param name="username">The username to authenticate with Active Directory. Cannot be null.</param>
        /// <param name="password">The password to authenticate with Active Directory. Cannot be null.</param>
        /// <param name="logger">Optional logger for logging operations. If null, no logging will be performed.</param>
        /// <remarks>
        /// This constructor initializes a new Active Directory helper with authentication credentials.
        /// The LDAP path should be in the format "LDAP://domain.com/DC=domain,DC=com".
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when ldapPath, username, or password is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Create a new Active Directory helper with a logger
        /// string ldapPath = "LDAP://mycompany.com/DC=mycompany,DC=com";
        /// string username = "administrator";
        /// string password = "p@ssw0rd";
        /// ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger&lt;ActiveDirectoryHelper&gt;();
        /// 
        /// var adHelper = new ActiveDirectoryHelper(ldapPath, username, password, logger);
        /// </code>
        /// </example>
        public ActiveDirectoryHelper(string ldapPath, string username, string password, ILogger logger = null)
        {
            _ldapPath = ldapPath ?? throw new ArgumentNullException(nameof(ldapPath));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
            _logger = logger;
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
                _logger?.LogError(ex, "Error checking if user exists: {Username}, Error: {ErrorMessage}", 
                    username, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets user properties from Active Directory by username.
        /// </summary>
        /// <param name="username">The username (sAMAccountName) to search for. Cannot be null or empty.</param>
        /// <param name="propertiesToLoad">The properties to load from the user account. If null or empty, default properties will be loaded.</param>
        /// <returns>A dictionary containing the requested properties and their values, or null if the user does not exist or an error occurs.</returns>
        /// <remarks>
        /// This method searches for a user in Active Directory with the specified username and retrieves the requested properties.
        /// If the propertiesToLoad parameter is null or empty, a default set of common properties will be loaded.
        /// The returned dictionary contains property names as keys and their corresponding values.
        /// Any exceptions encountered during the search are caught and logged, and the method returns null in such cases.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when username is null.</exception>
        /// <exception cref="ArgumentException">Thrown when username is empty.</exception>
        /// <see cref="Dictionary{TKey, TValue}"/>
        /// <example>
        /// <code lang="C#">
        /// var adHelper = new ActiveDirectoryHelper("LDAP://mycompany.com/DC=mycompany,DC=com", "administrator", "p@ssw0rd");
        /// 
        /// // Get specific properties for a user
        /// var properties = adHelper.GetUserProperties("johndoe", new[] { "cn", "mail", "displayName", "department" });
        /// 
        /// if (properties != null)
        /// {
        ///     foreach (var prop in properties)
        ///     {
        ///         Console.WriteLine($"{prop.Key}: {prop.Value}");
        ///     }
        /// }
        /// else
        /// {
        ///     Console.WriteLine("User not found or an error occurred.");
        /// }
        /// </code>
        /// </example>
        public Dictionary<string, object> GetUserProperties(string username, string[] propertiesToLoad = null)
        {
            if (username == null)
                throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));

            _logger?.LogDebug("Getting user properties for {Username}", username);

            try
            {
                using (var entry = GetDirectoryEntry())
                {
                    using (var searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = $"(sAMAccountName={username})";

                        // Use provided properties or default ones
                        string[] propsToLoad = propertiesToLoad ?? new[] 
                        { 
                            "cn", 
                            "givenName", 
                            "sn", 
                            "displayName", 
                            "mail", 
                            "userPrincipalName", 
                            "memberOf",
                            "distinguishedName",
                            "whenCreated",
                            "whenChanged"
                        };

                        foreach (var prop in propsToLoad)
                        {
                            searcher.PropertiesToLoad.Add(prop);
                        }

                        var result = searcher.FindOne();
                        if (result == null)
                        {
                            _logger?.LogWarning("User not found: {Username}", username);
                            return null;
                        }

                        var properties = new Dictionary<string, object>();
                        foreach (string propertyName in propsToLoad)
                        {
                            if (result.Properties.Contains(propertyName))
                            {
                                if (result.Properties[propertyName].Count > 1)
                                {
                                    // For multi-valued properties
                                    var values = new List<object>();
                                    foreach (var value in result.Properties[propertyName])
                                    {
                                        values.Add(value);
                                    }
                                    properties[propertyName] = values.ToArray();
                                }
                                else if (result.Properties[propertyName].Count == 1)
                                {
                                    // For single-valued properties
                                    properties[propertyName] = result.Properties[propertyName][0];
                                }
                            }
                        }

                        _logger?.LogInformation("Successfully retrieved {PropertyCount} properties for user {Username}", 
                            properties.Count, username);
                        return properties;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting user properties for {Username}: {ErrorMessage}", 
                    username, ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Checks if a group with the specified name exists in the Active Directory.
        /// </summary>
        /// <param name="groupName">The name of the group to check. Cannot be null or empty.</param>
        /// <returns>True if the group exists in the Active Directory, false otherwise or if an error occurs.</returns>
        /// <remarks>
        /// This method searches the Active Directory for a group with the specified name.
        /// It creates a DirectorySearcher with a filter targeting the group name and checks if any results are returned.
        /// Any exceptions encountered during the search are caught and logged, and the method returns false in such cases.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when groupName is null.</exception>
        /// <exception cref="ArgumentException">Thrown when groupName is empty.</exception>
        /// <see cref="bool"/>
        /// <example>
        /// <code lang="C#">
        /// var adHelper = new ActiveDirectoryHelper("LDAP://mycompany.com/DC=mycompany,DC=com", "administrator", "p@ssw0rd");
        /// 
        /// // Check if a group exists
        /// bool exists = adHelper.GroupExists("Marketing");
        /// 
        /// if (exists)
        ///     Console.WriteLine("The group exists in Active Directory.");
        /// else
        ///     Console.WriteLine("The group does not exist in Active Directory.");
        /// </code>
        /// </example>
        public bool GroupExists(string groupName)
        {
            if (groupName == null)
                throw new ArgumentNullException(nameof(groupName));
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("Group name cannot be empty", nameof(groupName));

            _logger?.LogDebug("Checking if group exists: {GroupName}", groupName);

            try
            {
                using (var entry = GetDirectoryEntry())
                {
                    using (var searcher = new DirectorySearcher(entry))
                    {
                        searcher.Filter = $"(&(objectClass=group)(cn={groupName}))";
                        searcher.PropertiesToLoad.Add("cn");
                        var result = searcher.FindOne();
                        
                        bool exists = result != null;
                        _logger?.LogDebug("Group {GroupName} {Exists}", groupName, exists ? "exists" : "does not exist");
                        return exists;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error checking if group exists: {GroupName}, Error: {ErrorMessage}", 
                    groupName, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Gets the members of a group from Active Directory.
        /// </summary>
        /// <param name="groupName">The name of the group. Cannot be null or empty.</param>
        /// <returns>A list of member names (sAMAccountName) in the group, or an empty list if the group does not exist or has no members.</returns>
        /// <remarks>
        /// This method searches Active Directory for a group with the specified name and retrieves its members.
        /// It extracts the sAMAccountName attribute for each member and returns it as a list of strings.
        /// Any exceptions encountered during the search are caught and logged, and the method returns an empty list in such cases.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when groupName is null.</exception>
        /// <exception cref="ArgumentException">Thrown when groupName is empty.</exception>
        /// <see cref="List{T}"/>
        /// <example>
        /// <code lang="C#">
        /// var adHelper = new ActiveDirectoryHelper("LDAP://mycompany.com/DC=mycompany,DC=com", "administrator", "p@ssw0rd");
        /// 
        /// // Get members of a group
        /// List&lt;string&gt; members = adHelper.GetGroupMembers("Marketing");
        /// 
        /// Console.WriteLine($"Group 'Marketing' has {members.Count} members:");
        /// foreach (string member in members)
        /// {
        ///     Console.WriteLine(member);
        /// }
        /// </code>
        /// </example>
        public List<string> GetGroupMembers(string groupName)
        {
            if (groupName == null)
                throw new ArgumentNullException(nameof(groupName));
            if (string.IsNullOrEmpty(groupName))
                throw new ArgumentException("Group name cannot be empty", nameof(groupName));

            _logger?.LogDebug("Getting members of group: {GroupName}", groupName);
            var members = new List<string>();

            try
            {
                using (var entry = GetDirectoryEntry())
                {
                    using (var searcher = new DirectorySearcher(entry))
                    {
                        // Find the group
                        searcher.Filter = $"(&(objectClass=group)(cn={groupName}))";
                        var result = searcher.FindOne();

                        if (result == null)
                        {
                            _logger?.LogWarning("Group not found: {GroupName}", groupName);
                            return members;
                        }

                        // Get the group's DirectoryEntry
                        using (DirectoryEntry groupEntry = result.GetDirectoryEntry())
                        {
                            // For each member, get their sAMAccountName
                            foreach (object member in groupEntry.Properties["member"])
                            {
                                using (var memberEntry = new DirectoryEntry($"LDAP://{member}", _username, _password))
                                {
                                    if (memberEntry.Properties["sAMAccountName"].Value != null)
                                    {
                                        members.Add(memberEntry.Properties["sAMAccountName"].Value.ToString());
                                    }
                                }
                            }
                        }
                    }
                }

                _logger?.LogInformation("Group {GroupName} has {MemberCount} members", groupName, members.Count);
                return members;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error getting members of group {GroupName}: {ErrorMessage}", 
                    groupName, ex.Message);
                return members;
            }
        }
    }
}
