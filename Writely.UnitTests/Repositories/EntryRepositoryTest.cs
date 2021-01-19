using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Models;
using Writely.Repositories;
using Xunit;

namespace Writely.UnitTests.Repositories
{
    public class EntryRepositoryTest : RepositoryTestBase
    {
        [Fact]
        public async Task GetById_EntryFound_ReturnsEntry()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            var entries = Helpers.GetEntries(1);
            AddEntriesToJournal(journal, entries);
            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();
            var repo = new EntryRepository(Context);

            // Act
            var result = await repo.GetById(entries[0].Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(entries[0].Id);
        }

        [Fact]
        public async Task GetById_EntryNotFound_ReturnsNull()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();
            var repo = new EntryRepository(Context);

            // Act
            var result = await repo.GetById(1L);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllByJournal_JournalFound_ReturnsEntries()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            var entries = Helpers.GetEntries(3);
            AddEntriesToJournal(journal, entries);
            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();
            var repo = new EntryRepository(Context);

            // Act
            var result = await repo.GetAllByJournal(journal.Id);

            // Assert
            result.Count.Should().Be(entries.Count);
        }

        [Fact]
        public async Task GetAllByJournal_JournalNotFound_ReturnsNull()
        {
            // Arrange
            await PrepareDatabase();
            var repo = new EntryRepository(Context);

            // Act
            var result = await repo.GetAllByJournal(1L);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAllByTag_JournalFound_ReturnsMatchingEntries()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            var entries = Helpers.GetEntries(5);
            entries[1].Tags = "dogs,cats";
            entries[2].Tags = "cats";
            entries[4].Tags = "frogs,dogs,cats,chickens";
            AddEntriesToJournal(journal, entries);
            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();
            var repo = new EntryRepository(Context);

            // Act
            var result = await repo.GetAllByTag(journal.Id, new[] {"cats"});

            // Assert
            result?.Count.Should().Be(3);
            result?.ForEach(e 
                => e.GetTags()?.Contains("cats").Should().BeTrue());
        }

        [Fact]
        public async Task GetAllByTag_JournalFound_NoMatches_ReturnsEmptyList()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            var entries = Helpers.GetEntries(3);
            AddEntriesToJournal(journal, entries);
            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();
            var repo = new EntryRepository(Context);

            // Act
            var result = await repo.GetAllByTag(journal.Id, new[]{ "blah" });

            // Assert
            result.Count.Should().Be(0);
        }

        [Fact]
        public async Task GetAllByTag_JournalNotFound_ReturnsNull()
        {
            // Arrange
            await PrepareDatabase();
            var repo = new EntryRepository(Context);

            // Act
            var result = await repo.GetAllByTag(1L, new[] {"Dogs"});

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Save_EntrySaved_ReturnsEntry()
        {
            
        }

        [Fact]
        public async Task Save_EntryNull_ThrowsArgumentNullException()
        {
            
        }

        [Fact]
        public async Task Update_EntryUpdated_ReturnEntry()
        {
            
        }

        [Fact]
        public async Task Update_EntryNull_ThrowsArgumentNullException()
        {
            
        }

        [Fact]
        public async Task Delete_JournalAndEntryFound_EntryDeleted_ReturnsTrue()
        {
            
        }

        [Fact]
        public async Task Delete_JournalNotFound_ReturnsFalse()
        {
            
        }

        [Fact]
        public async Task Delete_EntryNotFound_ReturnsFalse()
        {
            
        }

        private void AddEntriesToJournal(Journal journal, List<Entry> entries)
        {
            entries.ForEach(entry =>
            {
                entry.JournalId = journal.Id;
                entry.Journal = journal;
                journal.Entries.Add(entry);
            });
        }
    }
}