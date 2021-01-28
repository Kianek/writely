using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Writely.Exceptions;
using Writely.Models;
using Writely.Models.Dto;
using Writely.Repositories;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class JournalServiceTest
    {
        [Fact]
        public async Task GetById_UserFound_ReturnsJournal()
        {
            // Arrange
            var journal = Helpers.GetJournal();
            var mockRepo = new Mock<IJournalRepository>();
            mockRepo.Setup(r => r.GetById(journal.Id))
                .ReturnsAsync(() => journal);
            var service = new JournalService(mockRepo.Object);

            // Act
            var result = await service.GetById("UserId", journal.Id);

            // Assert
            result.Should().BeOfType<JournalDto>();
            result.Title.Should().Be(journal.Title);
        }

        [Fact]
        public async Task GetById_UserFound_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            var mockRepo = new Mock<IJournalRepository>();
            var journalId = 1L;
            mockRepo.Setup(r => r.GetById(journalId))
                .ReturnsAsync(() => null);
            var service = new JournalService(mockRepo.Object);

            // Assert
            service.Invoking(s => s.GetById("UserId", journalId))
                .Should()
                .Throw<JournalNotFoundException>()
                .WithMessage($"Journal not found: {journalId}");

        }

        [Fact]
        public async Task GetById_UserNotFound_ThrowsUserNotFoundException()
        {
            
        }

        [Fact]
        public async Task GetAllByUserId_UserFound_ReturnsJournals()
        {
            
        }

        [Fact]
        public async Task GetAllByUserId_UserNotFound_ThrowsUserNotFoundException()
        {
            
        }

        [Fact]
        public async Task Save_SaveSuccessful_ReturnsDto()
        {
            
        }

        [Fact]
        public async Task Save_NewJournalModelNull_ThrowsArgumentNullException()
        {
            
        }

        [Fact]
        public async Task Update_JournalFound_UpdateSuccessful_ReturnsDto()
        {
            
        }

        [Fact]
        public async Task Update_JournalUpdateModelNull_ThrowsArgumentNullException()
        {
            
        }

        [Fact]
        public async Task Update_JournalNotFound_ThrowsJournalNotFoundException()
        {
            
        }
    }
}