using FluentAssertions;
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
    }
}