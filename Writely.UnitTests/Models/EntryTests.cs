using System;
using FluentAssertions;
using Writely.Models;
using Xunit;

namespace Writely.UnitTests.Models
{
    public class EntryTests
    {
        [Fact]
        public void AddTags_TagsEmpty_CanAddSingleTag_ContainsTagWithNoSeparator()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            entry.Tags = "";

            // Act
            entry.AddTags("myTag");
            var result = entry.GetTags();

            // Assert
            result.Should().HaveCount(1);
            result![0].Should().Be("myTag");
        }


        [Fact]
        public void AddTags_UnnecessaryCommasRemoved()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            entry.Tags = "";

            // Act
            entry.AddTags(",,one,two,,three");
            var result = entry.GetTags();

            // Assert
            result?.Length.Should().Be(3);
        }

        [Fact]
        public void AddTags_ExistingTags_CanAddMultipleTags()
        {
            // Arrange
            var entry = Helpers.GetEntry();

            // Act
            entry.AddTags("four,five,six");
            var result = entry.GetTags();

            // Assert
            result.Should().HaveCount(6);
            entry.Tags.Should().Be("one,two,three,four,five,six");
        }

        [Fact]
        public void AddTags_FilterDuplicateTags_NoDuplicateTags()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            entry.Tags = "one,two,three,four";

            // Act
            entry.AddTags("four,four,five");
            var result = entry.GetTags();

            // Assert
            result.Should().HaveCount(5);
        }

        [Fact]
        public void AddTags_EmptyString_NoChangesMade()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            entry.Tags = "dogs,cats,rocks";

            // Act
            entry.AddTags("");

            // Assert
            entry.GetTags().Should().HaveCount(3);
        }
        
        [Fact]
        public void Update_AllPropertiesChanged_ReturnsTrue()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            var updateModel = new EntryUpdate
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
            entry.LastModified.Should().BeAfter(entry.CreatedAt);
        }

        [Fact]
        public void Update_OnePropertyChanged_ReturnsTrue()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            var updateModel = new EntryUpdate
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
            var updateModel = new EntryUpdate();

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