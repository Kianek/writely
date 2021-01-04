using System.Collections.Generic;
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

        [Fact]
        public void MapToDto_ReturnsListOfDtos()
        {
            // Arrange
            var entries = new List<Entry>
            {
                new Entry
                {
                    Id = 1,
                    JournalId = 1,
                    UserId = "UserId",
                    Title = "Entry 1",
                    Tags = "one,two,three",
                    Body = "Blah"
                },
                new Entry
                {
                    Id = 2,
                    JournalId = 1,
                    UserId = "UserId",
                    Title = "Entry 2",
                    Tags = "one,two,three",
                    Body = "Blah"
                },
                new Entry
                {
                    Id = 3,
                    JournalId = 1,
                    UserId = "UserId",
                    Title = "Entry 3",
                    Tags = "one,two,three",
                    Body = "Blah"
                },
            };

            // Act
            var result = entries.MapToDto();

            // Assert
            result.Should().BeOfType<List<EntryDto>>();
            result.Count.Should().Be(3);
        }
    }
}