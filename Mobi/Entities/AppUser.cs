using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Mobi.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<UserFavorite> UserFavorites { get; set; }

    }
}
