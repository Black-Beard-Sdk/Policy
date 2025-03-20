using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Black.Beard.Adfs
{

    /// <summary>
    /// Représente un utilisateur dans l'annuaire ADFS et expose ses propriétés.
    /// </summary>
    public class AdfsUser
    {

        public AdfsUser(UserPrincipal userPrincipal)
        {
            _userPrincipal = userPrincipal ?? throw new ArgumentNullException(nameof(userPrincipal));
        }

        public string Username
        {
            get => _userPrincipal.SamAccountName;
            set => _userPrincipal.SamAccountName = value;
        }

        public string DisplayName
        {
            get => _userPrincipal.DisplayName;
            set => _userPrincipal.DisplayName = value;
        }

        public string Email
        {
            get => _userPrincipal.EmailAddress;
            set => _userPrincipal.EmailAddress = value;
        }

        public void SetPassword(string password)
        {
            _userPrincipal.SetPassword(password);
        }

        public bool IsEnabled
        {
            get => !_userPrincipal.IsAccountLockedOut();
        }

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

        public void Save()
        {
            _userPrincipal.Save();
        }        

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

        public UserPrincipal UserPrincipal => _userPrincipal;

        private readonly UserPrincipal _userPrincipal;

    }

}
