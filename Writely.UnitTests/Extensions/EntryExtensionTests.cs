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
            var first = result[0];
            var second = result[1];
            var third = result[2];
            first.LastModified.Should().BeAfter(second.LastModified);
            second.LastModified.Should().BeAfter(third.LastModified);
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
            var first = result[0];
            var second = result[1];
            var third = result[2];
            first.LastModified.Should().BeBefore(second.LastModified);
            second.LastModified.Should().BeBefore(third.LastModified);
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
            var first = result[0];
            var second = result[1];
            var third = result[2];
            first.Title.Should().Be(zippy);
            second.Title.Should().Be(skippy);
            third.Title.Should().Be(flippy);
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
            var first = result[0];
            var second = result[1];
            var third = result[2];
            first.Title.Should().Be(flippy);
            second.Title.Should().Be(skippy);
            third.Title.Should().Be(zippy);
        }

        [Fact]
        public void Update_AllPropertiesChanged_ReturnsTrue()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            var updateModel = new EntryUpdateModel
            {
                Title = "Super New Title",
                Tags = "some,new,tags",
                Body = "Totally different body"
            };

            // Act
            var result = entry.Update(updateModel);

            // Assert
            result.Should().BeTrue();
            entry.Title.Should().Be(updateModel.Title);
            entry.Tags.Should().Be(updateModel.Tags);
            entry.Body.Should().Be(updateModel.Body);
        }

        [Fact]
        public void Update_OnePropertyChanged_ReturnsTrue()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            var updateModel = new EntryUpdateModel
            {
                Tags = "blah,dee,bloo"
            };

            // Act
            var result = entry.Update(updateModel);

            // Assert
            result.Should().BeTrue();
            entry.Tags.Should().Be(updateModel.Tags);
        }

        [Fact]
        public void Update_NoChange_ReturnsFalse()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            var updateModel = new EntryUpdateModel();

            // Act
            var result = entry.Update(updateModel);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Update_EntryUpdateModelNull_ThrowsArgumentNullException()
        {
            // Arrange
            var entry = Helpers.GetEntry();

            // Assert
            entry.Invoking(e => e.Update(null))
                .Should()
                .Throw<ArgumentNullException>();
        }
    }
}