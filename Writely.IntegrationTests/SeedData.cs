using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Writely.Data;
using Writely.Models;

namespace Writely.IntegrationTests
{
    public static class SeedData
    {
        public static void InitializeDatabase(AppDbContext context)
        {
            var hasher = new PasswordHasher<AppUser>();
            var password = "TotallyHashedPassword123!";
            var bob = new AppUser
            {
                Id = "UserIdBob",
                UserName = "bob.loblaw",
                FirstName = "Bob",
                LastName = "Loblaw",
                Email = "bob@gmail.com",
            };
            var hashedPassword = hasher.HashPassword(bob, password);
            bob.PasswordHash = hashedPassword;
            
            context.Users.Add(bob);

            var bobJournals = new List<Journal>
            {
                new Journal
                {
                    UserId = bob.Id,
                    Title = "Bob's Journal",
                }
            };
            bobJournals[0].Entries = AddEntries(bobJournals[0], 4);
            context.Journals!.AddRange(bobJournals);

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