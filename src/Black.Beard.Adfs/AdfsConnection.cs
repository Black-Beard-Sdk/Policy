using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Reflection.Metadata.Ecma335;

namespace Black.Beard.Adfs
{


    public class AdfsConnection : IDisposable
    {


        public AdfsConnection(string serverUrl, string domain, string username, string password)
        {
            _serverUrl = serverUrl ?? throw new ArgumentNullException(nameof(serverUrl));
            _domain = domain ?? throw new ArgumentNullException(nameof(domain));
            _username = username ?? throw new ArgumentNullException(nameof(username));
            _password = password ?? throw new ArgumentNullException(nameof(password));
        }

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

        public DirectoryEntry GetDirectoryEntry()
        {
            string ldapPath = $"LDAP://{_serverUrl}/{_domain}";
            return new DirectoryEntry(ldapPath, _username, _password);
        }

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

        public void Dispose()
        {
            _principalContext?.Dispose();
        }

        private readonly string _serverUrl;
        private readonly string _domain;
        private readonly string _username;
        private readonly string _password;
        private PrincipalContext _principalContext;

    }

}
