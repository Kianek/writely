using System;
using System.Collections.Generic;
using System.Linq;
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

        [Fact]
        public void SortBy_Default_SortByDateDescending()
        {
            // Arrange 
            var entries = Helpers.GetEntries(3);
            entries[0].LastModified = new DateTime(2021, 1, 14);
            entries[1].LastModified = new DateTime(2021, 1, 19);
            entries[2].LastModified = new DateTime(2021, 1, 25);

            // Act
            var result = entries.AsQueryable().SortBy().ToList();

            // Assert
            result.Should().BeInDescendingOrder(e => e.LastModified);
        }

        [Fact]
        public void SortBy_Default_SortByDateAscending()
        {
            // Arrange 
            var entries = Helpers.GetEntries(3);
            entries[0].LastModified = new DateTime(2021, 1, 14);
            entries[1].LastModified = new DateTime(2021, 1, 19);
            entries[2].LastModified = new DateTime(2021, 1, 25);

            // Act
            var result = entries.AsQueryable().SortBy(SortOrder.DateAscending).ToList();

            // Assert
            result.Should().BeInAscendingOrder(e => e.LastModified);
        }

        [Fact]
        public void SortBy_Default_SortByTitleDescending()
        {
            // Arrange
            var entries = Helpers.GetEntries(3);
            var skippy = "Skippy";
            var flippy = "Flippy";
            var zippy = "Zippy";
            entries[0].Title = skippy;
            entries[1].Title = flippy;
            entries[2].Title = zippy;

            // Act
            var result = entries.AsQueryable().SortBy(SortOrder.Descending).ToList();

            // Assert
            result.Should().BeInDescendingOrder(e => e.Title);
        }

        [Fact]
        public void SortBy_Default_SortByTitleAscending()
        {
            // Arrange
            var entries = Helpers.GetEntries(3);
            var skippy = "Skippy";
            var flippy = "Flippy";
            var zippy = "Zippy";
            entries[0].Title = skippy;
            entries[1].Title = flippy;
            entries[2].Title = zippy;

            // Act
            var result = entries.AsQueryable().SortBy(SortOrder.Ascending).ToList();

            // Assert
            result.Should().BeInAscendingOrder(e => e.Title);
        }
    }
}