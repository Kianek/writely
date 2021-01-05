using System.Collections.Generic;
using Writely.Models;

namespace Writely.UnitTests
{
    public static class Helpers
    {
        public static Entry GetEntry() => new Entry
        {
            Id = 1, Title = "Entry 1", Tags = "one,two,three", Body = "Blah"
        };

        public static List<Entry> GetEntries(int num)
        {
            var entries = new List<Entry>();
            for (int i = 1; i <= num; i++)
            {
                entries.Add(new Entry
                {
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
                    Title = $"Journal {i}"
                });
            }

            return journals;
        }
    }
}