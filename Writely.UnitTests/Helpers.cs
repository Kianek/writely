using System.Collections.Generic;
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
        
        public static AccountUpdate GetEmailUpdate(string email = "spiffynewemail@gmail.com")
            => new("UserId", emailUpdate: new() { Email = email });

        public static AccountUpdate GetPasswordUpdate(string password = "SpiffierPassword123!!")
            => new("UserId", passwordUpdate: 
                new(){Password = password, ConfirmPassword = password});
    }
}