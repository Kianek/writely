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
            
        }

        [Fact]
        public async Task GetAll_JournalsFound_Limit_ReturnsLimitedJournals()
        {
            
        }

        [Fact]
        public async Task GetAll_JournalsFound_ReturnsSortedByTitleAscending()
        {
            
        }
        
        [Fact]
        public async Task GetAll_JournalsFound_ReturnsSortedByTitleDescending()
        {
            
        }
        [Fact]
        public async Task GetAll_JournalsFound_ReturnsSortedByDateAscending()
        {
            
        }
        [Fact]
        public async Task GetAll_JournalsFound_ReturnsSortedByDateDescending()
        {
            
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