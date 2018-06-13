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
            LdapManager manager = new LdapManager("int.cgu.it");
            List<string> groupsToSearch = new List<string> { "EVIFRS" };   //ecc.

            List<UserPrincipal> searchedMembers = manager.SearchUsersInGroups(groupsToSearch);

            //Console.WriteLine("\n### Pulitura Email ###");
            //Console.WriteLine(manager.CleanEmailsString(searchedMembers));

            Console.ReadKey();
        }
    }
}
