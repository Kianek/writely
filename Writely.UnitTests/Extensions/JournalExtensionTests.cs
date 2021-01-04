using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Writely.Extensions;
using Writely.Models;
using Writely.Models.Dto;
using Xunit;

namespace Writely.UnitTests.Extensions
{
    public class JournalExtensionTests
    {
        [Fact]
        public void MapToDto_ReturnsListOfLength3()
        {
            // Arrange
            var userId = "UserId";
            var journals = new List<Journal>
            {
                new Journal
                {
                    Title = "Journal 1",
                    UserId = userId
                },
                new Journal
                {
                    Title = "Journal 2",
                    UserId = userId
                },
                new Journal
                {
                    Title = "Journal 3",
                    UserId = userId
                },
            };
            
            // Act
            var result = journals.MapToDto();
            
            // Assert
            result.Should().BeOfType<List<JournalDto>>();
            result.Count().Should().Be(3);
        }

        [Fact]
        public void CanMapJournalToDto()
        {
            // Arrange
            var journal = new Journal
            {
                Id = 1,
                UserId = "UserId",
                Title = "Journal 1"
            };

            // Act
            var result = journal.ToDto();

            // Assert
            result.Should().BeOfType<JournalDto>();
            result.Id.Should().Be(journal.Id);
            result.UserId.Should().Be(journal.UserId);
        }
    }
}