using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ldap_Searcher
{
    public class LdapManager
    {
        private string ldapServer;
        private string user;
        private string pass;

        public LdapManager(string ldapServer, string user = "mbossi", string pass = "Inverno18!")
        {
            this.LdapServer = ldapServer;
            this.User = user;
            this.Pass = pass;
        }

        public string LdapServer { get => ldapServer; set => ldapServer = value; }
        public string User { get => user; set => user = value; }
        public string Pass { get => pass; set => pass = value; }

        public List<UserPrincipal> SearchUsersInGroups(List<string> _groupsToSearch)
        {
            List<UserPrincipal> searchedMembers = new List<UserPrincipal>();

            using (PrincipalContext context = new PrincipalContext(ContextType.Domain, LdapServer, User, Pass))
            {

                foreach (var _group in _groupsToSearch)
                {
                    List<UserPrincipal> groupMembers = new List<UserPrincipal>();
                    using (var group = GroupPrincipal.FindByIdentity(context, _group))
                    {
                        if (group == null)
                        {
                            Console.WriteLine($"Gruppo {_group} non trovato");
                        }
                        else
                        {
                            var users = group.GetMembers(true);
                            foreach (UserPrincipal user in users)
                            {
                                searchedMembers.Add(user);
                                groupMembers.Add(user);
                            }

                        }
                    }
                    this.printUsersForGroup(_group, groupMembers);
                }
            }
            
            return searchedMembers;
        }

        private void printUsersForGroup(string _group, List<UserPrincipal> users)
        {
            Console.WriteLine($"\nRicerca nel gruppo {_group}");
            if (!users.Any())
            {
                Console.WriteLine($"Nessun utente nel gruppo: {_group}");
            }
            foreach (var x in users)
            {
                string infos = "";
                infos += $"Nome: {x.DisplayName}";

                if (x.EmailAddress != null)
                {
                    infos += $" - Email: {x.EmailAddress}";
                }
                else
                {
                    infos += $" - Email: NO MAIL";
                }

                if (x.AccountExpirationDate != null)
                {
                    infos += $" -ExpirationDate: { x.AccountExpirationDate}";
                }
                Console.WriteLine(infos);
            }
            //searchedMembers.ForEach(x => Console.WriteLine($"Nome: {x.DisplayName} - Email: {x.EmailAddress} - ExpirationDate: {x.AccountExpirationDate}"));
           
        }

        public string CleanEmailsString(List<UserPrincipal> usersInGroup)
        {
            string mailAddressSeparator = ";";
            string toList = usersInGroup.Any() ? usersInGroup.Where(u => u.EmailAddress != null && (u.AccountExpirationDate == null || u.AccountExpirationDate >= DateTime.Now)).Select(u => u.EmailAddress).Aggregate((i, j) => i + mailAddressSeparator + j) : string.Empty;

            return toList;
        }
    }
}
