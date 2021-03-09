using System;
using System.Collections.Generic;
using Writely.Data;
using Writely.Models;

namespace Writely.IntegrationTests
{
    public static class SeedData
    {
        public static void InitializeDatabase(AppDbContext context)
        {
            var bob = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "bob.loblaw",
                FirstName = "Bob",
                LastName = "Loblaw",
                Email = "bob@gmail.com",
                PasswordHash = "TotallyHashedPassword123!"
            };
            var flem = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "flimmy.mcflimflam",
                FirstName = "Flem",
                LastName = "McFlimFlam",
                Email = "flem@gmail.com",
                PasswordHash = "TotallyHashedPassword123!"
            };
            context.Users.AddRange(bob, flem);

            var bobJournals = new List<Journal>
            {
                new Journal
                {
                    UserId = bob.Id,
                    Title = "Bob's Journal",
                }
            };
            bobJournals[0].Entries = AddEntries(bobJournals[0], 4);
            context.Journals.AddRange(bobJournals);
            
            var flemJournals = new List<Journal>
            {
                new Journal
                {
                    UserId = flem.Id,
                    Title = "Flem's Journal",
                }
            };
            flemJournals[0].Entries = AddEntries(flemJournals[0], 4);
            context.Journals.AddRange(flemJournals);

            context.SaveChanges();
        }

        private static List<Entry> AddEntries(Journal journal, int numOfEntries)
        {
            var entries = new List<Entry>();
            for (int i = 0; i < numOfEntries; i++)
            {
                entries.Add(
                    new Entry
                    {
                        UserId = journal.UserId,
                        Journal = journal,
                        Title = $"Entry{i+1}",
                        Body = "Blah"
                    });
            }

            return entries;
        }
    }
}