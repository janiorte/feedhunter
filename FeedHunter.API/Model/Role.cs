﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FeedHunter.API.Model
{
    public class Role : IdentityRole<int>
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
