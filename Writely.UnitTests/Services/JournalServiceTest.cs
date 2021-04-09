using System;
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
        [Fact]
        public async Task GetById_UserFound_ReturnsJournal()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var service = GetJournalService();

            // Act
            var result = await service.GetById(journal.Id);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(journal.Title);
        }

        [Fact]
        public async Task GetById_UserFound_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetJournalService();

            // Assert
            service.Invoking(s => s.GetById(2L))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task GetById_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetJournalService(null);

            // Assert
            service.Invoking(s => s.GetById(1L))
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
            await Context.SaveChangesAsync();
            var service = GetJournalService();

            // Act
            var result = await service.GetAll();

            // Assert
            result.Should().HaveCount(5);
        }

        [Fact]
        public async Task GetAll_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetJournalService(null);

            // Assert
            service.Invoking(s => s.GetAll())
                .Should()
                .Throw<UserNotFoundException>();
        }

        [Fact]
        public async Task Add_SaveSuccessful_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetJournalService();
            var newJournal = new NewJournal {Title = "Fancy Journal"};

            // Act
            var result = await service.Add(newJournal);

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
            var service = GetJournalService();

            // Assert
            service.Invoking(s => s.Add(null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Add_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetJournalService(null);

            // Assert
            service.Invoking(s => s.Add(It.IsAny<NewJournal>()))
                .Should()
                .Throw<UserNotFoundException>();
        }

        [Fact]
        public async Task Update_JournalFound_UpdateSuccessful_ReturnsNumberOfUpdatedEntities()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var updateModel = new JournalUpdate {Title = "Shiny New Title"};
            var service = GetJournalService(journal.UserId);

            // Act
            var result = await service.Update(journal.Id, updateModel);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task Update_JournalUpdateModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            await PrepDbWithJournal();
            var service = GetJournalService();

            // Assert
            service.Invoking(s => s.Update(1L, null!))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Update_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetJournalService();

            // Assert
            service.Invoking(s => s.Update(1L, new JournalUpdate()))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        [Fact]
        public async Task Remove_JournalFoundAndRemoved_ReturnsTrue()
        {
            // Arrange
            var journal = await PrepDbWithJournal();
            var service = GetJournalService();

            // Act
            var result = await service.Remove(journal.Id);

            // Assert
            result.Should().Be(1);
        }

        [Fact]
        public async Task Remove_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            await PrepareDatabase();
            var service = GetJournalService();

            // Assert
            service.Invoking(s => s.Remove(1L))
                .Should()
                .Throw<JournalNotFoundException>();
        }

        private JournalService GetJournalService(string? userId = "UserId") 
            => new(Context!) { UserId = userId};

        private async Task<Journal> PrepDbWithJournal()
        {
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            await SaveJournal(journal);
            return journal;
        }
    }
}