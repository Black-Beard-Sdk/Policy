using System.DirectoryServices;

namespace Black.Beard.Adfs
{
    public class ActiveDirectoryHelper
    {
        private readonly string _ldapPath;
        private readonly string _username;
        private readonly string _password;

        public ActiveDirectoryHelper(string ldapPath, string username, string password)
        {
            _ldapPath = ldapPath;
            _username = username;
            _password = password;
        }

        public DirectoryEntry GetDirectoryEntry()
        {
            return new DirectoryEntry(_ldapPath, _username, _password);
        }

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
