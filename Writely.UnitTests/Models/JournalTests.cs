using System;
using FluentAssertions;
using Writely.Models;
using Xunit;

namespace Writely.UnitTests.Models
{
    public class JournalTests
    {
        private Journal journal;

        public JournalTests()
        {
            journal = Helpers.GetJournal();
        }
        
        [Fact]
        public void Add_CanAddEntryAndSetNavigationProperties()
        {
            // Arrange
            var entry = Helpers.GetEntry();

            // Act
            journal.Add(entry);

            // Assert
            journal.Entries.Contains(entry).Should().BeTrue();
            journal.LastModified.Should().BeAfter(journal.CreatedAt);
            entry.Journal.Should().Be(journal);
            entry.JournalId.Should().Be(journal.Id);
        }

        [Fact]
        public void Add_EntryNull_ThrowsArgumentNullException()
        {
            // Assert
            journal.Invoking(j => j.Add(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public void Remove_EntryRemoved_ReturnsTrue()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            journal.Add(entry);

            // Act
            var result = journal.Remove(entry);

            // Assert
            result.Should().BeTrue();
            journal.Entries.Should().NotContain(entry);
        }

        [Fact]
        public void Remove_EntryNotFound_ReturnsFalse()
        {
            // Arrange
            var entry = Helpers.GetEntry();

            // Assert
            journal.Remove(entry).Should().BeFalse();
        }

        [Fact]
        public void Remove_EntryNull_ThrowsArgumentNullException()
        {
            // Assert
            journal.Invoking(j => j.Remove(null))
                .Should()
                .Throw<ArgumentNullException>();
        }
}
}