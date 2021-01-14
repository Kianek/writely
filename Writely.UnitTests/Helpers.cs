using System.Collections.Generic;
using Writely.Models;

namespace Writely.UnitTests
{
    public static class Helpers
    {
        public static Entry GetEntry(string userId) => new Entry
        {
            UserId = userId, Id = 1, Title = "Entry 1", Tags = "one,two,three", Body = "Blah"
        };

        public static List<Entry> GetEntries(string userId, int num)
        {
            var entries = new List<Entry>();
            for (int i = 1; i <= num; i++)
            {
                entries.Add(new Entry
                {
                    UserId = userId,
                    Id = i,
                    Title = $"Entry {i}",
                    Tags = "one,two,three",
                    Body = "Blah"
                });
            }

            return entries;
        }

        public static Journal GetJournal(string userId) => new Journal
        {
            UserId = userId,
            Id = 1,
            Title = "Journal 1"
        };

        public static List<Journal> GetJournals(string userId, int num)
        {
            var journals = new List<Journal>();
            for (int i = 1; i <= num; i++)
            {
                journals.Add(new Journal
                {
                    UserId = userId,
                    Id = i,
                    Title = $"Journal {i}"
                });
            }

            return journals;
        }
    }
}