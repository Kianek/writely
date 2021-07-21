using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Moq;
using Writely.Data;
using Writely.Models;

namespace Writely.UnitTests
{
    public static class Helpers
    {
        private static readonly string _userId = "UserId";
        
        public static Entry GetEntry() => new Entry
        {
            UserId = _userId, Id = 1, Title = "Entry 1", Tags = "one,two,three", Body = "Blah"
        };

        public static List<Entry> GetEntries(int num, long journalId = 1L)
        {
            var entries = new List<Entry>();
            for (int i = 1; i <= num; i++)
            {
                entries.Add(new Entry
                {
                    UserId = _userId,
                    Id = i,
                    Title = $"Entry {i}",
                    Tags = "one,two,three",
                    Body = "Blah"
                });
            }

            return entries;
        }

        public static Journal GetJournal() => new Journal
        {
            UserId = _userId,
            Id = 1,
            Title = "Journal 1"
        };

        public static List<Journal> GetJournals(int num)
        {
            var journals = new List<Journal>();
            for (int i = 1; i <= num; i++)
            {
                journals.Add(new Journal
                {
                    UserId = _userId,
                    Id = i,
                    Title = $"Journal {i}"
                });
            }

            return journals;
        }
        
        
        public static void AddEntriesToJournal(Journal journal, List<Entry> entries)
        {
            entries.ForEach(journal.Add);
        }
        
        public static Registration GetRegistration() => new Registration
        {
            Username = "bob.loblaw",
            FirstName = "Bob",
            LastName = "Loblaw",
            Email = "bob@loblawlaw.com",
            Password = "SecretPassword123!",
            ConfirmPassword = "SecretPassword123!",
        };
        
        public static EmailUpdate GetEmailUpdate(string email = "spiffynewemail@gmail.com")
            => new("UserId", email);

        public static PasswordUpdate GetPasswordUpdate(string password = "SpiffierPassword123!!")
            => new("UserId", password, password);
        
        public static NewEntry GetNewEntry(long journalId = 1L) =>
            new NewEntry(
                "UserId", 
                journalId, 
                "Spiffy Title", 
                "tag1,tag2,tag3,", 
                "Stuff goes here");

        public static Credentials GetCredentials() 
            => new Credentials("bob@gmail.com", "Password123!");

        public static Mock<UserManager<AppUser>> GetMockUserManager()
            => new (new Mock<IUserStore<AppUser>>().Object,
                null, null, null, null, null, null, null, null);
    }
}