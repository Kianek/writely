using System;
using Microsoft.AspNetCore.Identity;

namespace Writely.Data
{
    public class AppUser : IdentityUser
    {
        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset LastModified { get; }

        public AppUser()
        {
            CreatedAt = LastModified = DateTimeOffset.UtcNow;
        }
    }
}