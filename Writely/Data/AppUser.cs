using System;
using Microsoft.AspNetCore.Identity;

namespace Writely.Data
{
    public class AppUser : IdentityUser
    {
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? LastModified { get; set; }

        public AppUser()
        {
            CreatedAt = LastModified = DateTimeOffset.UtcNow;
        }
    }
}