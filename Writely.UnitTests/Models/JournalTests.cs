using System;
using FluentAssertions;
using Writely.Models;
using Xunit;

namespace Writely.UnitTests.Models
{
    public class JournalTests
    {
        private readonly Journal _journal;

        public JournalTests()
        {
            _journal = Helpers.GetJournal();
        }
        
        [Fact]
        public void Add_CanAddEntryAndSetNavigationProperties()
        {
            // Arrange
            var entry = Helpers.GetEntry();

            // Act
            _journal.Add(entry);

            // Assert
            _journal.Entries.Contains(entry).Should().BeTrue();
            _journal.LastModified.Should().BeAfter(_journal.CreatedAt);
            entry.Journal.Should().Be(_journal);
            entry.JournalId.Should().Be(_journal.Id);
        }

        [Fact]
        public void Add_EntryNull_ThrowsArgumentNullException()
        {
            // Assert
            _journal.Invoking(j => j.Add(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public void Remove_EntryRemoved_ReturnsTrue()
        {
            // Arrange
            var entry = Helpers.GetEntry();
            _journal.Add(entry);

            // Act
            var result = _journal.Remove(entry);

            // Assert
            result.Should().BeTrue();
            _journal.Entries.Should().NotContain(entry);
        }

        [Fact]
        public void Remove_EntryNotFound_ReturnsFalse()
        {
            // Arrange
            var entry = Helpers.GetEntry();

            // Assert
            _journal.Remove(entry).Should().BeFalse();
        }

        [Fact]
        public void Remove_EntryNull_ThrowsArgumentNullException()
        {
            // Assert
            _journal.Invoking(j => j.Remove(null))
                .Should()
                .Throw<ArgumentNullException>();
        }
}
}