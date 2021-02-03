using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Extensions;
using Writely.Models;
using Xunit;

namespace Writely.UnitTests.Extensions
{
    public class QueryExtensionTests
    {
        
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
            var entries = Helpers.GetEntries( 3);
            var blah = "Blah, Blah, Blah";
            var shifty = "Shifty";
            var spiffy = "Spiffy";
            // First
            entries[0].Title = blah;
            // Second
            entries[2].Title = shifty;
            // Third
            entries[1].Title = spiffy;
            IQueryable<Entry> query = entries.AsQueryable();

            // Act
            var result = query.SortBy(SortOrder.Descending).ToList();

            // Assert
            result[0].Title.Should().Be(spiffy);
            result[1].Title.Should().Be(shifty);
            result[2].Title.Should().Be(blah);
        }

        [Fact]
        public void SortBy_Overload_ReturnsIOrderedEnumerable()
        {
            // Arrange
            var entries = Helpers.GetEntries(3);
            var x = "x";
            var y = "y";
            var z = "z";
            entries[0].Title = x;
            entries[1].Title = y;
            entries[2].Title = z;

            // Act
            var result = entries.SortBy("desc").ToList();

            // Assert
            result[0].Title.Should().Be(z);
            result[1].Title.Should().Be(y);
            result[2].Title.Should().Be(x);
        }

        [Fact]
        public void GetByTag_ReturnsFilteredEntries()
        {
            // Arrange
            var entries = Helpers.GetEntries(5);
            entries[0].Tags = "cats,dogs,bigfoot";
            entries[2].Tags = "reptiles,bigfoot";

            // Act
            var result = entries.GetByTag(new[] {"bigfoot"}).ToList();

            // Assert
            result.Count.Should().Be(2);
        }
    }
}