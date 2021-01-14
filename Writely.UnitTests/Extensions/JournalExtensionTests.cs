using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            result.Count().Should().Be(3);
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
        public async Task SortBy_Default_SortByDateDescending()
        {
            // Arrange
            var journals = Helpers.GetJournals( 3);
            // First
            journals[2].LastModified = new DateTime(2021, 1, 18);
            // Second
            journals[0].LastModified = new DateTime(2021, 1, 16);
            // Third
            journals[1].LastModified = new DateTime(2021, 1, 12);
            IQueryable<Journal> query = journals.AsQueryable();

            // Act
            var result = query.SortBy(SortOrder.DateDescending).ToList();

            // Assert
            result[2].LastModified.Should().BeBefore(result[0].LastModified);
            result[0].LastModified.Should().BeAfter(result[1].LastModified);
        }

        [Fact]
        public async Task SortBy_SortByDateAscending()
        {
            // Arrange
            var journals = Helpers.GetJournals( 3);
            // First
            journals[1].LastModified = new DateTime(2021, 1, 12);
            // Second
            journals[0].LastModified = new DateTime(2021, 1, 16);
            // Third
            journals[2].LastModified = new DateTime(2021, 1, 18);
            IQueryable<Journal> query = journals.AsQueryable();

            // Act
            var result = query.SortBy(SortOrder.DateDescending).ToList();

            // Assert
            result[1].LastModified.Should().BeBefore(result[0].LastModified);
            result[0].LastModified.Should().BeAfter(result[2].LastModified);
        }

        [Fact]
        public async Task SortBy_SortByTitleAscending()
        {
            // Arrange
            var journals = Helpers.GetJournals(3);
            var blah = "Blah, Blah, Blah";
            var shifty = "Shifty";
            var spiffy = "Spiffy";
            // First
            journals[2].Title = blah;
            // Second
            journals[0].Title = shifty;
            // Third
            journals[1].Title = spiffy;
            IQueryable<Journal> query = journals.AsQueryable();

            // Act
            var result = query.SortBy(SortOrder.Ascending).ToList();

            // Assert
            result[0].Title.Should().Be(blah);
            result[1].Title.Should().Be(shifty);
            result[2].Title.Should().Be(spiffy);
        }

        [Fact]
        public async Task SortBy_SortByTitleDescending()
        {
            // Arrange
            var journals = Helpers.GetJournals( 3);
            var blah = "Blah, Blah, Blah";
            var shifty = "Shifty";
            var spiffy = "Spiffy";
            // First
            journals[0].Title = blah;
            // Second
            journals[2].Title = shifty;
            // Third
            journals[1].Title = spiffy;
            IQueryable<Journal> query = journals.AsQueryable();

            // Act
            var result = query.SortBy(SortOrder.Descending).ToList();

            // Assert
            result[0].Title.Should().Be(spiffy);
            result[1].Title.Should().Be(shifty);
            result[2].Title.Should().Be(blah);
        }
}
}