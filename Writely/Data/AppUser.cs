using System;
using Microsoft.AspNetCore.Identity;
using Writely.Models;

namespace Writely.Data
{
    public sealed class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTimeOffset CreatedAt { get; }
        public DateTimeOffset LastModified { get; }

        public AppUser(Registration reg) : this()
        {
            UserName = reg.Username;
            Email = reg.Email;
            FirstName = reg.FirstName;
            LastName = reg.LastName;
        }
        
        public AppUser()
        {
            CreatedAt = LastModified = DateTimeOffset.UtcNow;
        }
    }
}