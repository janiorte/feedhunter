using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FeedHunter.API.Model
{
    public class User : IdentityUser<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
