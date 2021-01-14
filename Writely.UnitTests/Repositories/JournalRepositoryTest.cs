using System;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Data;
using Writely.Repositories;
using Xunit;

namespace Writely.UnitTests.Repositories
{
    public class JournalRepositoryTest : RepositoryTestBase
    {
        private string _userId = "UserId";

        [Fact]
        public async Task GetById_JournalFound_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            Context.Journals?.Add(journal);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo(Context);

            // Act
            var result = await repo.GetById(_userId, journal.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(journal.Id);
        }

        [Fact]
        public async Task GetById_JournalNotFound_ReturnsNull()
        {
            // Arrange
            await PrepareDatabase();
            var repo = GetJournalRepo(Context);

            // Act
            var result = await repo.GetById(_userId, 4);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetAll_JournalsFound_NoLimit_ReturnsAllJournals()
        {
            // Arrange
            await PrepareDatabase();
            Context.Journals.AddRange(Helpers.GetJournals(5));
            await Context.SaveChangesAsync();
            var repo = new JournalRepository(Context);

            // Act
            var result = await repo.GetAll(_userId);

            // Assert
            result.Count.Should().Be(5);
        }

        [Fact]
        public async Task GetAll_JournalsFound_Limit_ReturnsLimitedJournals()
        {
            // Arrange
            await PrepareDatabase();
            Context.Journals.AddRange(Helpers.GetJournals(5));
            await Context.SaveChangesAsync();
            var repo = new JournalRepository(Context);
            
            // Act
            var result = await repo.GetAll(_userId, 2);

            // Assert
            result.Count.Should().Be(2);
        }

        [Fact]
        public async Task Save_JournalSaved_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            var repo = new JournalRepository(Context);

            // Act
           await repo.Save(journal);
           var result = await Context.Journals.FindAsync(journal.Id);

           // Assert
           result.Should().NotBeNull();
        }

        [Fact]
        public async Task Save_JournalNull_ThrowsArgumentNullException()
        {
            // Arrange
            await PrepareDatabase();
            var repo = new JournalRepository(Context);

            // Assert
            repo.Invoking(r => r.Save(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Update_JournalUpdated_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();
            var repo = new JournalRepository(Context);

            // Act
            journal.Title = "Better Title";
            var result = await repo.Update(journal);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(journal.Title);
        }

        [Fact]
        public async Task Update_JournalNull_ThrowsArgumentNullException()
        {
            // Arrange
            await PrepareDatabase();
            var repo = new JournalRepository(Context);

            // Assert
            repo.Invoking(r => r.Update(null))
                .Should()
                .Throw<ArgumentNullException>();
        }

        [Fact]
        public async Task Delete_JournalDeleted_ReturnsTrue()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal();
            Context.Journals.Add(journal);
            await Context.SaveChangesAsync();
            long id = journal.Id;
            var repo = new JournalRepository(Context);
            

            // Act
            var result = await repo.Delete(id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_JournalNotFound_ReturnsFalse()
        {
            // Arrange
            await PrepareDatabase();
            var repo = new JournalRepository(Context);

            // Act
            var result = await repo.Delete(1L);

            // Assert
            result.Should().BeFalse();
        }

        private JournalRepository GetJournalRepo(AppDbContext context) => new JournalRepository(context);
    }
}