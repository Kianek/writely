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
    public class JournalExtensionTests
    {
        [Fact]
        public void MapToDto_ReturnsListOfLength3()
        {
            // Arrange
            var journals = Helpers.GetJournals(3);

            // Act
            var result = journals.MapToDto();

            // Assert
            result.Should().BeOfType<List<JournalDto>>();
            result.Count.Should().Be(3);
        }

        [Fact]
        public void CanMapJournalToDto()
        {
            // Arrange
            var journal = Helpers.GetJournal();

            // Act
            var result = journal.ToDto();

            // Assert
            result.Should().BeOfType<JournalDto>();
            result.Id.Should().Be(journal.Id);
            result.UserId.Should().Be(journal.UserId);
        }

        [Fact]
        public void SortBy_Default_SortByDateDescending()
        {
            // Arrange
            var journals = Helpers.GetJournals(3);
            journals[2].LastModified = new DateTime(2021, 1, 18);
            journals[0].LastModified = new DateTime(2021, 1, 16);
            journals[1].LastModified = new DateTime(2021, 1, 12);

            // Act
            var result = journals.SortBy(SortOrder.DateDescending);

            // Assert
            result.Should().BeInDescendingOrder(j => j.LastModified);
        }

        [Fact]
        public void SortBy_SortByDateAscending()
        {
            // Arrange
            var journals = Helpers.GetJournals(3);
            journals[1].LastModified = new DateTime(2021, 1, 12);
            journals[0].LastModified = new DateTime(2021, 1, 16);
            journals[2].LastModified = new DateTime(2021, 1, 18);

            // Act
            var result = journals.SortBy(SortOrder.DateDescending);

            // Assert
            result.Should().BeInDescendingOrder(j => j.LastModified);
        }

        [Fact]
        public void SortBy_SortByTitleAscending()
        {
            // Arrange
            var journals = Helpers.GetJournals(3);
            journals[2].Title = "Blah, Blah, Blah";
            journals[0].Title = "Shifty";
            journals[1].Title = "Spiffy";

            // Act
            var result = journals.SortBy(SortOrder.Ascending);

            // Assert
            result.Should().BeInAscendingOrder(j => j.Title);
        }

        [Fact]
        public void SortBy_SortByTitleDescending()
        {
            // Arrange
            var journals = Helpers.GetJournals(3);
            journals[0].Title = "Blah, Blah, Blah";
            journals[2].Title = "Shifty";
            journals[1].Title = "Spiffy";

            // Act
            var result = journals.SortBy(SortOrder.Descending);

            // Assert
            result.Should().BeInDescendingOrder(j => j.Title);
        }
    }
}