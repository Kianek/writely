using System;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Exceptions;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class EntryServiceTest
    {
        private readonly Journal _journal;
        private readonly IEntryService _service;
        
        public EntryServiceTest()
        {
            _service = new EntryService();
            _journal = Helpers.GetJournal();
            var entries = Helpers.GetEntries(4, _journal.Id);
            Helpers.AddEntriesToJournal(_journal, entries);
        }

        [Fact]
        public async Task GetById_EntryFound_ReturnsEntry()
        {
            // Act
            var result = _service.GetById(_journal, 2L);

            // Assert
            result.Should().NotBeNull();
            result?.Title.Should().Contain("2");
        }

        [Fact]
        public async Task GetById_EntryNotFound_ThrowsEntryNotFoundException()
        {
            // Assert
            _service.Invoking(s => s.GetById(_journal, 5L))
                .Should()
                .Throw<EntryNotFoundException>();
        }

        [Fact]
        public async Task Add_JournalFound_EntrySaved_ReturnsEntry()
        {
            // Arrange
            var newEntry = new NewEntry(
                _journal.UserId, _journal.Id,
                "Spiffy New Entry",
                "gimme,some,reggae",
                "Axl Rose is the best evar");

            // Act
            var result = _service.Add(_journal, newEntry);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(newEntry.Title);
        }

        [Fact]
        public async Task Add_NewEntryNull_ThrowArgumentNullException()
        {
            // Act
            _service.Invoking(s => s.Add(_journal, null!))
                .Should()
                .Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task Add_JournalNull_ThrowsArgumentNullException()
        {
            // Assert
            _service.Invoking(s => s.Add(null!, new NewEntry("", 0L, "", "", "")))
                .Should()
                .Throw<JournalNotFoundException>();
        }
        
        [Fact]
        public async Task Update_EntryFound_EntryUpdated_ReturnsEntry() 
        {
            // Arrange
            var updateModel = new EntryUpdate
            {
                Title = "Totally Different Title",
                Tags = "some,new,tags",
                Body = "And now for something completely different."
            };

            // Act
            var result = _service.Update(_journal, 2L, updateModel);

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
            // Assert
            _service.Invoking(s => s.Update(_journal, 9L, new EntryUpdate()))
                .Should()
                .Throw<EntryNotFoundException>();
        }
        
        [Fact]
        public async Task Update_JournalNull_ThrowsArgumentNullException()
        {
            // Assert
            _service.Invoking(s => s.Update(null!, 1L, new EntryUpdate()))
                .Should()
                .Throw<ArgumentNullException>();
        }
        
        [Fact]
        public async Task Update_UpdateModelNull_ThrowsArgumentNullException()
        {
            // Assert
            _service.Invoking(s => s.Update(_journal, 1L, null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Remove_EntryFound_Removed()
        {
            // Act
            var removedEntry = _service.Remove(_journal, 1L);

            // Assert
            removedEntry.Should().NotBeNull();
            _journal.Entries.Should().NotContain(removedEntry);
        }

        [Fact]
        public async Task Remove_JournalNull_ThrowsArgumentNullException()
        {
            // Assert
            _service.Invoking(s => s.Remove(null!, 1L))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Remove_EntryNotFound_ThrowsEntryNotFoundException()
        {
            // Assert
            _service.Invoking(s => s.Remove(_journal, 8L))
                .Should()
                .Throw<EntryNotFoundException>();
        }

        private IEntryService GetEntryService() => new EntryService();
    }
}