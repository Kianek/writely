using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Writely.Exceptions;
using Writely.Models;
using Writely.Models.Dto;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class JournalServiceTest : DatabaseTestBase
    {
        private JournalService _service;
        
        [Fact]
        public async Task GetById_UserFound_ReturnsJournal()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();

            // Act
            var result = await _service.GetById(journal.Id);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(journal.Title);
        }

        [Fact]
        public async Task GetById_UserFound_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.GetById(2L))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task GetById_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService("");

            // Assert
            _service.Invoking(s => s.GetById(1L))
                .Should()
                .Throw<UserNotFoundException>();
        }

        [Fact]
        public async Task GetAll_UserFound_ReturnsJournals()
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(5);
            Context?.Journals!.AddRange(journals);
            await Context!.SaveChangesAsync();
            _service = GetJournalService();

            // Act
            var result = await _service.GetAll(new QueryFilter());

            // Assert
            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAll_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService(null);

            // Assert
            _service.Invoking(s => s.GetAll(new QueryFilter()))
                .Should()
                .Throw<UserNotFoundException>();
        }

        [Fact]
        public async Task GetAllEntries_EntriesFound_ReturnsEntries()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();

            // Act
            var result = (await _service.GetAllEntries(journal.Id, new QueryFilter()))?.ToList();

            // Assert
            result?.Count.Should().Be(5);
        }

        [Fact]
        public async Task GetAllEntries_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.GetAllEntries(9L, new QueryFilter()))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task Add_SaveSuccessful_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var newJournal = new NewJournal {Title = "Fancy Journal"};
            _service = GetJournalService();

            // Act
            var result = await _service.Add(newJournal);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(newJournal.Title);
            result.UserId.Should().Be("UserId");
        }

        [Fact]
        public async Task Add_NewJournalModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.Add(null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Add_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService(null);

            // Assert
            _service.Invoking(s => s.Add(It.IsAny<NewJournal>()))
                .Should()
                .Throw<UserNotFoundException>();
        }

        [Fact]
        public async Task Update_JournalFound_UpdateSuccessful_ReturnsNumberOfUpdatedEntities()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var updateModel = new JournalUpdate {Title = "Shiny New Title"};
            _service = GetJournalService();

            // Act
            var result = await _service.Update(journal.Id, updateModel);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task Update_JournalUpdateModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            await PrepDbWithJournal();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.Update(1L, null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Update_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.Update(1L, new JournalUpdate()))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task Remove_JournalFoundAndRemoved_ReturnsTrue()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();

            // Act
            var result = await _service.Remove(journal.Id);

            // Assert
            result.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task Remove_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.Remove(1L))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task RemoveAllByUser_UserFound_JournalsRemoved()
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(3);
            Helpers.AddEntriesToJournal(journals[2], Helpers.GetEntries(20));
            Context!.Journals?.AddRange(journals);
            await Context.SaveChangesAsync();
            _service = GetJournalService();

            // Act
            var result = await _service.RemoveAllByUser("UserId");

            // Assert
            result.Should().BeGreaterOrEqualTo(20);
        }
        
        // Entry-related tests

        [Fact]
        public async Task GetEntry_EntryFound_ReturnsEntry()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();
            
            // Act
            var result = await _service.GetEntry(journal.Id, 1L);

            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetEntry_EntryNotFound_ThrowsEntryNotFoundException()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.GetEntry(journal.Id, 9L))
                .Should()
                .Throw<EntryNotFoundException>();
        }

        [Fact]
        public async Task GetEntry_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();
            
            // Assert
            _service.Invoking(s => s.GetEntry(5L, 1L))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task AddEntry_EntryAdded_ReturnsEntry()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var newEntry = GetValidEntry(journal.UserId, journal.Id);
            _service = GetJournalService();

            // Act
            var result = await _service.AddEntry(journal.Id, newEntry);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(newEntry.Title);
        }

        [Fact]
        public async Task AddEntry_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.AddEntry(4L, GetValidEntry("UserId", 5L)))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task AddEntry_NewEntryNull_ThrowsArgumentNullException()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.AddEntry(journal.Id, null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateEntry_JournalAndEntryFound_EntryUpdated_ReturnsEntry()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var entryId = journal.Entries[0].Id;
            var update = new EntryUpdate { Body = "Spiffy new body" };
            _service = GetJournalService();

            // Act
            var result = await _service.UpdateEntry(journal.Id, entryId, update);

            // Assert
            result.Should().NotBeNull();
            result.Body.Should().Be(update.Body);
        }

        [Fact]
        public async Task UpdateEntry_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Act
            _service.Invoking(s => s.UpdateEntry(4L, 2L, new EntryUpdate()))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task UpdateEntry_EntryNotFound_ThrowsEntryNotFoundException()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();

            // Act
            _service.Invoking(s => s.UpdateEntry(journal.Id, 10L, new EntryUpdate()))
                .Should()
                .Throw<EntryNotFoundException>();
        }

        [Fact]
        public async Task UpdateEntry_EntryUpdateNull_ThrowsArgumentNullException()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var entryId = journal.Entries[0].Id;
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.UpdateEntry(journal.Id, entryId, null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task RemoveEntry_JournalAndEntryFound_EntryRemoved_ReturnsEntry()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var entryId = journal.Entries[0].Id;
            _service = GetJournalService();

            // Act
            var result = await _service.RemoveEntry(journal.Id, entryId);

            // Assert
            journal.Entries.Should().NotContain(result);
        }

        [Fact]
        public async Task RemoveEntry_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.RemoveEntry(3L, 4L))
                .Should()
                .Throw<JournalNotFoundException>();
        }
        
        [Fact]
        public async Task RemoveEntry_JournalNotFound_ThrowsEntryNotFoundException()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            _service = GetJournalService();

            // Assert
            _service.Invoking(s => s.RemoveEntry(journal.Id, 10L))
                .Should()
                .Throw<EntryNotFoundException>();
        }
        
        private NewEntry GetValidEntry(string userId, long journalId) => new NewEntry(
                                                   userId,
                                                   journalId,
                                                   "New Entry",
                                                   "one,two,three",
                                                   "Some text here");
        
        private JournalService GetJournalService(string? userId = "UserId") 
            => new(Context!, new EntryService()) { UserId = userId};

        private async Task<Journal> PrepDbWithJournal()
        {
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            Helpers.AddEntriesToJournal(journal, Helpers.GetEntries(5));
            await SaveJournal(journal);
            return journal;
        }
    }
}