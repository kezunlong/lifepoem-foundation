using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;

namespace Lifepoem.Foundation.Utilities.Helpers
{
    public class ActiveDirectoryHelper
    {
        public string ActiveDirectoryPath = "LDAP://DC=corp,DC=irco,DC=com";

        public string Domain { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ActiveDirectoryHelper(string domain, string userName, string password)
        {
            this.Domain = domain;
            this.UserName = userName;
            this.Password = password;
        }

        public bool IsAuthenticated()
        {
            if (string.IsNullOrEmpty(Domain) || string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                return false;
            }

            DirectoryEntry entry = new DirectoryEntry(ActiveDirectoryPath, Domain + @"\" + UserName, Password);
            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);
                search.Filter = "(SAMAccountName=" + UserName + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();
                if (null != result)
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
    }
}
