using System;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Exceptions;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class EntryServiceTest : DatabaseTestBase
    {
        [Fact]
        public async Task GetById_EntryFound_ReturnsEntry()
        {
            // Arrange
            await PrepDbWithJournalAndEntries();
            var service = GetEntryService();

            // Act
            var result = await service.GetById(2L);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Contain("2");
        }

        [Fact]
        public async Task GetById_EntryNotFound_ThrowsEntryNotFoundException()
        {
            // Arrange
            var service = await PrepDbAndEntryService();

            // Assert
            service.Invoking(s => s.GetById(5L))
                .Should()
                .Throw<EntryNotFoundException>();
        }

        [Fact]
        public async Task GetAll_JournalFound_ReturnsEntries()
        {
            // Arrange
            var journal = await PrepDbWithJournalAndEntries(10);
            var service = GetEntryService();

            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().HaveCount(journal.Entries.Count);
        }

        [Fact]
        public async Task GetAll_JournalNotFound_ReturnsEmptySequence()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetEntryService(null);
            
            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllByTag_JournalFound_ReturnsEntriesByTag()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            var entries = Helpers.GetEntries(10);
            Helpers.AddEntriesToJournal(journal, entries);
            entries[1].Tags = "dogs";
            entries[2].Tags = "dogs,cats";
            entries[4].Tags = "dogs,pirates";
            Context!.Journals?.Add(journal);
            await Context!.SaveChangesAsync();
            var service = GetEntryService(journal.Id);

            // Act
            var result = await service.GetAllByTag("dogs");

            // Assert
            result.Should().HaveCount(3);
        }

        [Fact]
        public async Task GetAllByTag_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetEntryService();

            // Assert
            service.Invoking(s => s.GetAllByTag("blah,tags"))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task Add_JournalFound_EntrySaved_ReturnsEntry()
        {
            // Arrange
            await PrepDbWithJournalAndEntries();
            var service = GetEntryService();
            var newEntry = new NewEntry
            {
                Title = "Spiffy New Entry",
                Tags = "gimme,some,reggae",
                Body =  "Axl Rose is the best evar"
            };

            // Act
            var result = await service.Add(newEntry);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(newEntry.Title);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData(3L)]
        public async Task Add_JournalNotFound_ThrowsJournalNotFoundException(long? journalId)
        {
            // Arrange
            var service = await PrepDbAndEntryService(journalId);

            // Assert
            service.Invoking(s => s.Add(new NewEntry()))
                .Should()
                .Throw<JournalNotFoundException>();
        }
        
        [Fact]
        public async Task Update_EntryFound_EntryUpdated_ReturnsEntry() 
        {
            // Arrange
            await PrepDbWithJournalAndEntries();
            var updateModel = new EntryUpdate
            {
                Title = "Totally Different Title",
                Tags = "some,new,tags",
                Body = "And now for something completely different."
            };
            var service = GetEntryService();

            // Act
            var result = await service.Update(1L, updateModel);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(updateModel.Title);
            result.Tags.Should().Be(updateModel.Tags);
            result.Body.Should().Be(updateModel.Body);
            result.LastModified.Should().BeAfter(result.CreatedAt);
        }

        [Fact]
        public async Task Update_EntryNotFound_ThrowsEntryNotFoundException()
        {
            // Arrange
            await PrepDbWithJournalAndEntries(0);
            var service = GetEntryService();
            
            // Assert
            service.Invoking(s => s.Update(3L, new EntryUpdate()))
                .Should()
                .Throw<EntryNotFoundException>();
        }
        
        [Fact]
        public async Task Update_UpdateModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            var service = await PrepDbAndEntryService();

            // Assert
            service.Invoking(s => s.Update(1L, null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Remove_EntryFound_Removed()
        {
            // Arrange
            var journal = await PrepDbWithJournalAndEntries();
            var service = GetEntryService();

            // Act
            var removedEntry = await service.Remove(1L);

            // Assert
            removedEntry.Should().NotBeNull();
            journal.Entries.Should().NotContain(removedEntry);
        }

        [Fact]
        public async Task Remove_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            var service = await PrepDbAndEntryService();
            service.JournalId = 0L;

            // Assert
            service.Invoking(s => s.Remove(1L))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task Remove_EntryNotFound_ThrowsEntryNotFoundException()
        {
            // Arrange
            await PrepDbWithJournalAndEntries(1);
            var service = GetEntryService();

            // Assert
            service.Invoking(s => s.Remove(5L))
                .Should()
                .Throw<EntryNotFoundException>();
        }

        private async Task<IEntryService> PrepDbAndEntryService(long? journalId = 1L)
        {
            await PrepareDatabase();
            return GetEntryService(journalId);
        }

        private async Task<Journal> PrepDbWithJournalAndEntries(int numOfEntries = 5)
        {
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            Helpers.AddEntriesToJournal(journal, Helpers.GetEntries(numOfEntries));
            Context!.Journals?.Add(journal);
            await Context.SaveChangesAsync();
            return journal;
        }

        private IEntryService GetEntryService(long? journalId = 1L) => new EntryService(Context!) { JournalId = journalId};
    }
}