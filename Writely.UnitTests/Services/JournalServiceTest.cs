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

            // Act

            // Assert
        }

        [Fact]
        public async Task GetById_UserFound_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange

            // Act
            
            // Assert
        }

        [Fact]
        public async Task GetById_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public async Task GetAllByUserId_UserFound_ReturnsJournals()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public async Task GetAllByUserId_UserNotFound_ThrowsUserNotFoundException()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public async Task Save_SaveSuccessful_ReturnsDto()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public async Task Save_NewJournalModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public async Task Update_JournalFound_UpdateSuccessful_ReturnsDto()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public async Task Update_JournalUpdateModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            
            // Act
            
            // Assert
        }

        [Fact]
        public async Task Update_JournalNotFound_ThrowsJournalNotFoundException()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
    }
}