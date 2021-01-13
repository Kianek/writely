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

        public JournalRepositoryTest()
        {
        }

        [Fact]
        public async Task GetById_JournalFound_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var journal = Helpers.GetJournal(_userId);
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
            Context.Journals.AddRange(Helpers.GetJournals(_userId, 5));
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
            Context.Journals.AddRange(Helpers.GetJournals(_userId, 5));
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
            
        }

        [Fact]
        public async Task Save_JournalNull_ReturnsNull()
        {
            
        }

        [Fact]
        public async Task Update_JournalUpdated_ReturnsJournal()
        {
            
        }

        [Fact]
        public async Task Update_JournalNull_ReturnsNull()
        {
            
        }

        [Fact]
        public async Task Delete_JournalDeleted_ReturnsTrue()
        {
            
        }

        [Fact]
        public async Task Delete_JournalNotFound_ReturnsFalse()
        {
            
        }

        private JournalRepository GetJournalRepo(AppDbContext context) => new JournalRepository(context);
    }
}