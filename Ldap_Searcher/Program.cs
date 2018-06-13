using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;

namespace Ldap_Searcher
{
    class Program
    {
        static void Main(string[] args)
        {
            LdapManager manager = new LdapManager("DOMAIN");
            List<string> groupsToSearch = new List<string> { "GROUP_NAME" };   //ecc.

            List<UserPrincipal> searchedMembers = manager.SearchUsersInGroups(groupsToSearch);

            //Console.WriteLine("\n### Pulitura Email ###");
            //Console.WriteLine(manager.CleanEmailsString(searchedMembers));

            Console.ReadKey();
        }
    }
}
