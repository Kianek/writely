using System.Collections.Generic;
using FluentAssertions;
using Writely.Extensions;
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
            var entry = Helpers.GetEntry();

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
            var entries = Helpers.GetEntries(3);

            // Act
            var result = entries.MapToDto();

            // Assert
            result.Should().BeOfType<List<EntryDto>>();
            result.Count.Should().Be(3);
        }
    }
}