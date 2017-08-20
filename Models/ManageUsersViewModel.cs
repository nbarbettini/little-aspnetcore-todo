using System.Collections.Generic;
using AspNetCoreTodo.Models;

namespace AspNetCoreTodo
{
    public class ManageUsersViewModel
    {
        public IEnumerable<ApplicationUser> Administrators { get; set; }

        public IEnumerable<ApplicationUser> Everyone { get; set; }
    }
}
