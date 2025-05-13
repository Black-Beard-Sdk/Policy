// Ignore Spelling: Adfs

using System.DirectoryServices.AccountManagement;
using System.Runtime.Versioning;

namespace Bb.Adfs
{
    /// <summary>
    /// Represents a user in the ADFS directory and exposes its properties.
    /// </summary>
    /// <remarks>
    /// This class provides a simplified interface for working with Active Directory user accounts
    /// by wrapping a UserPrincipal object and exposing common operations.
    /// </remarks>
    [SupportedOSPlatform("windows")]
    public class AdfsUser
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="AdfsUser"/> class.
        /// </summary>
        /// <param name="userPrincipal">The UserPrincipal object to wrap. Cannot be null.</param>
        /// <remarks>
        /// This constructor creates a new AdfsUser instance that wraps the provided UserPrincipal.
        /// The UserPrincipal provides access to the underlying Active Directory user account.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when userPrincipal is null.</exception>
        /// <example>
        /// <code lang="C#">
        /// // Get a UserPrincipal object from Active Directory
        /// using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
        /// {
        ///     UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(context, "johndoe");
        ///     if (userPrincipal != null)
        ///     {
        ///         // Create an AdfsUser that wraps the UserPrincipal
        ///         AdfsUser user = new AdfsUser(userPrincipal);
        ///     }
        /// }
        /// </code>
        /// </example>
        public AdfsUser(UserPrincipal userPrincipal)
        {
            _userPrincipal = userPrincipal ?? throw new ArgumentNullException(nameof(userPrincipal));
        }

        public string UserPrincipalName
        {
            get => _userPrincipal.UserPrincipalName;
        }

        /// <summary>
        /// Gets or sets the username (SamAccountName) of the user.
        /// </summary>
        /// <remarks>
        /// The username is the SamAccountName used to identify the user in Active Directory.
        /// It is typically used for login purposes and must be unique within the domain.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when setting the value to null.</exception>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// // Get the current username
        /// string username = user.Username;
        /// Console.WriteLine($"Current username: {username}");
        /// 
        /// // Change the username
        /// user.Username = "newusername";
        /// user.Save();
        /// </code>
        /// </example>
        public string Login
        {
            get => _userPrincipal.SamAccountName;
            set => _userPrincipal.SamAccountName = value;
        }

        /// <summary>
        /// Gets or sets the display name of the user.
        /// </summary>
        /// <remarks>
        /// The display name is typically the full name of the user as it should appear in
        /// address lists and other user interfaces.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// // Get the current display name
        /// string displayName = user.DisplayName;
        /// Console.WriteLine($"Current display name: {displayName}");
        /// 
        /// // Change the display name
        /// user.DisplayName = "John Doe";
        /// user.Save();
        /// </code>
        /// </example>
        public string DisplayName
        {
            get => _userPrincipal.DisplayName;
            set => _userPrincipal.DisplayName = value;
        }

        public string GivenName
        {
            get => _userPrincipal.GivenName;
            set => _userPrincipal.GivenName = value;
        }

        public string Surname
        {
            get => _userPrincipal.Surname;
            set => _userPrincipal.Surname = value;
        }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        /// <remarks>
        /// The email address is used for communication and may also be used for account authentication
        /// in some scenarios.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// // Get the current email address
        /// string email = user.Email;
        /// Console.WriteLine($"Current email: {email}");
        /// 
        /// // Change the email address
        /// user.Email = "john.doe@example.com";
        /// user.Save();
        /// </code>
        /// </example>
        public string Email
        {
            get => _userPrincipal.EmailAddress;
            set => _userPrincipal.EmailAddress = value;
        }

        /// <summary>
        /// Sets the password for the user account.
        /// </summary>
        /// <param name="password">The new password to set. Cannot be null or empty.</param>
        /// <remarks>
        /// This method changes the user's password. The password should meet domain password complexity
        /// requirements, otherwise an exception will be thrown.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when password is null.</exception>
        /// <exception cref="ArgumentException">Thrown when password is empty or does not meet complexity requirements.</exception>
        /// <exception cref="PasswordException">Thrown when there is an issue setting the password.</exception>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// try
        /// {
        ///     // Set a new password for the user
        ///     user.SetPassword("NewSecureP@ssw0rd");
        ///     Console.WriteLine("Password changed successfully");
        /// }
        /// catch (Exception ex)
        /// {
        ///     Console.WriteLine($"Error changing password: {ex.Message}");
        /// }
        /// </code>
        /// </example>
        public void SetPassword(string password)
        {
            _userPrincipal.SetPassword(password);
        }

        /// <summary>
        /// Gets a value indicating whether the user account is enabled.
        /// </summary>
        /// <remarks>
        /// This property checks if the account is locked out. An account that is not locked out
        /// is considered enabled. If you need to check for other disabled states, you may need
        /// to access the underlying UserPrincipal.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// if (user.IsEnabled)
        /// {
        ///     Console.WriteLine("The user account is enabled");
        /// }
        /// else
        /// {
        ///     Console.WriteLine("The user account is disabled or locked out");
        /// }
        /// </code>
        /// </example>
        /// <returns>True if the account is not locked out; otherwise, false.</returns>
        /// <see cref="bool"/>
        public bool IsEnabled
        {
            get => !_userPrincipal.IsAccountLockedOut();
        }

        /// <summary>
        /// Enables or disables the user account.
        /// </summary>
        /// <param name="enabled">True to enable the account; false to disable it.</param>
        /// <remarks>
        /// When enabling an account, this method will both unlock the account and set the
        /// Enabled property to true. When disabling an account, it will only set the Enabled
        /// property to false.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// // Enable the user account
        /// user.SetEnabled(true);
        /// user.Save();
        /// 
        /// // Later, disable the user account
        /// user.SetEnabled(false);
        /// user.Save();
        /// </code>
        /// </example>
        public void SetEnabled(bool enabled)
        {
            if (enabled)
            {
                _userPrincipal.UnlockAccount();
                _userPrincipal.Enabled = true;
            }
            else
            {
                _userPrincipal.Enabled = false;
            }
        }

        /// <summary>
        /// Saves changes made to the user account to the Active Directory.
        /// </summary>
        /// <remarks>
        /// This method must be called after making changes to the user's properties in order
        /// for those changes to be persisted to Active Directory. If not called, changes will be discarded.
        /// </remarks>
        /// <exception cref="PrincipalOperationException">Thrown when there is an error saving changes to the directory.</exception>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// // Make changes to the user
        /// user.DisplayName = "John A. Doe";
        /// user.Email = "john.doe@example.com";
        /// 
        /// // Save the changes to Active Directory
        /// user.Save();
        /// </code>
        /// </example>
        public void Save()
        {
            _userPrincipal.Save();
        }        

        /// <summary>
        /// Adds the user to the specified Active Directory groups.
        /// </summary>
        /// <param name="groupNames">An array of group names to add the user to. Cannot be null.</param>
        /// <returns>The current AdfsUser instance for method chaining.</returns>
        /// <remarks>
        /// This method finds each group by name in the Active Directory and adds the user as a member.
        /// The method saves each group after adding the user, but does not save the user itself.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when groupNames is null.</exception>
        /// <exception cref="ArgumentException">Thrown when a group specified in groupNames does not exist in the directory.</exception>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// try
        /// {
        ///     // Add the user to multiple groups
        ///     user.AddGroups("Sales", "Marketing", "External");
        ///     
        ///     // Or use method chaining
        ///     user.AddGroups("Employees")
        ///         .SetEnabled(true)
        ///         .Save();
        /// }
        /// catch (ArgumentException ex)
        /// {
        ///     Console.WriteLine($"Error adding to groups: {ex.Message}");
        /// }
        /// </code>
        /// </example>
        /// <see cref="AdfsUser"/>
        public AdfsUser AddGroups(params string[] groupNames)
        {
            // Ajouter l'utilisateur aux groupes spécifiés
            foreach (string groupName in groupNames)
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(_userPrincipal.Context, groupName);
                if (group == null)
                    throw new ArgumentException($"Group '{groupName}' does not exist in the directory", nameof(groupNames));

                group.Members.Add(this._userPrincipal);
                group.Save();
            }

            return this;
        }

        /// <summary>
        /// Gets the underlying UserPrincipal object.
        /// </summary>
        /// <remarks>
        /// This property provides access to the underlying UserPrincipal object, allowing
        /// for advanced operations not directly exposed by the AdfsUser class.
        /// </remarks>
        /// <example>
        /// <code lang="C#">
        /// AdfsUser user = GetUserFromActiveDirectory();
        /// 
        /// // Access the underlying UserPrincipal for advanced operations
        /// UserPrincipal principal = user.UserPrincipal;
        /// 
        /// // Now you can use any UserPrincipal functionality
        /// DateTime? lastPasswordSet = principal.LastPasswordSet;
        /// bool mustChangePassword = principal.LastPasswordSet == null;
        /// </code>
        /// </example>
        /// <returns>The UserPrincipal object wrapped by this AdfsUser instance.</returns>
        /// <see cref="UserPrincipal"/>
        public UserPrincipal UserPrincipal => _userPrincipal;

        private readonly UserPrincipal _userPrincipal;
    }
}
