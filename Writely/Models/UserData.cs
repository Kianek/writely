using System;
using System.Collections.Generic;
using Writely.Data;
using Writely.Extensions;
using Writely.Models.Dto;

namespace Writely.Models
{
    public class UserData
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? LastModified { get; set; }
        public List<JournalDto> Journals { get; set; } = new();
        
        
        public UserData(AppUser user, List<Journal> journals)
        {
            Id = user.Id;
            Email = user.Email;
            Username = user.UserName;
            CreatedAt = user.CreatedAt;
            LastModified = user.LastModified;
            Journals = journals.MapToDto();
        }

        public UserData()
        {
        }
    }
}