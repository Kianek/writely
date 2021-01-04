using FluentAssertions;
using Writely.Extensions;
using Writely.Models;
using Writely.Models.Dto;
using Xunit;

namespace Writely.UnitTests.Extensions
{
    public class EntryExtensionTests
    {
        [Fact]
        public void MapEntryToDto_ReturnsDto()
        {
            // Arrange
            var entry = new Entry
            {
                Id = 1,
                JournalId = 1,
                UserId = "UserId",
                Title = "My Entry",
                Tags = "one,two,three",
                Body = "Blah blah blah",
            };

            // Act
            var result = entry.ToDto();
            
            // Assert
            result.Should().BeOfType<EntryDto>();
            result.Id.Should().Be(entry.Id);
        }
    }
}