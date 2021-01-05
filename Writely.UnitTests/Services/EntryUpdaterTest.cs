using FluentAssertions;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class EntryUpdaterTest
    {
        [Fact]
        public void UpdateEntry_UpdatesAllFields_ReturnsTrue()
        {
            // Arrange
            var entry = new Entry
            {
                Id = 1,
                Title = "Old Entry",
                Tags = "",
                Body = "Blah"
            };
            var updateModel = new EntryUpdateModel
            {
                Title = "Updated Entry",
                Tags = "one,two,three",
                Body = "Super neat entry",
            };
            var updater = new EntryUpdater();

            // Act
            var (updatedEntry, didUpdate) = updater.Update(entry, updateModel);

            // Assert
            didUpdate.Should().BeTrue();
            updatedEntry.Title.Should().Be(updateModel.Title);
            updatedEntry.Tags.Should().Be(updateModel.Tags);
            updatedEntry.Body.Should().Be(updateModel.Body);
        }

        [Fact]
        public void UpdateEntry_UpdateSingleField_ReturnsTrue()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            var updateModel = new EntryUpdateModel
            {
                Title = "Spiffy New Title"
            };
            var updater = new EntryUpdater();
            
            // Act
            var (updatedEntry, didUpdate) = updater.Update(entry, updateModel);

            // Assert
            didUpdate.Should().BeTrue();
            updatedEntry.Title.Should().Be(updateModel.Title);
        }

        [Fact]
        public void UpdateEntry_NoChanges_ReturnsFalse()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            var updateModel = new EntryUpdateModel
            {
                Title = entry.Title
            };
            var updater = new EntryUpdater();
            
            // Act
            var (_, didUpdate) = updater.Update(entry, updateModel);

            // Assert
            didUpdate.Should().BeFalse();
        }
    }
}