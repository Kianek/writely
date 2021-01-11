using FluentAssertions;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Services
{
    public class JournalUpdaterTest
    {
        private readonly string _userId = "UserId";
        [Fact]
        public void Update_ChangeTitle_ReturnsTrue()
        {
            // Arrange
            var journal = Helpers.GetJournal(_userId);
            var updateModel = new JournalUpdateModel
            {
                Title = "Spiffy New Title"
            };
            var updater = new JournalUpdater();
            
            // Act
            var (updatedJournal, didUpdate) = updater.Update(journal, updateModel);

            // Assert
            didUpdate.Should().BeTrue();
            updatedJournal.Title.Should().Be(updateModel.Title);
        }

        [Fact]
        public void Update_TitlesIdentical_NoUpdate_ReturnsFalse()
        {
            // Arrange
            var journal = Helpers.GetJournal(_userId);
            var updateModel = new JournalUpdateModel
            {
                Title = journal.Title
            };
            var updater = new JournalUpdater();

            // Act
            var (_, didUpdate) = updater.Update(journal, updateModel);

            // Assert
            didUpdate.Should().BeFalse();
        }

        [Fact]
        public void Update_UpdateModelNull_ReturnsFalse()
        {
            // Arrange
            var journal = Helpers.GetJournal(_userId);
            var updater = new JournalUpdater();

            // Act
            var (_, didUpdate) = updater.Update(journal, null);

            //Assert
            didUpdate.Should().BeFalse();
        }
    }
}