using System.Threading.Tasks;
using FluentAssertions;
using Writely.Data;
using Writely.Repositories;
using Xunit;

namespace Writely.UnitTests.Repositories
{
    public class JournalRepositoryTest : RepositoryTestBase
    {

        public JournalRepositoryTest()
        {
        }

        [Fact]
        public async Task GetById_JournalFound_ReturnsJournal()
        {
            // Arrange
            await PrepareDatabase();
            var userId = "UserId";
            var journal = Helpers.GetJournal(userId);
            Context.Journals?.Add(journal);
            await Context.SaveChangesAsync();
            var repo = GetJournalRepo(Context);

            // Act
            var result = await repo.GetById(userId, journal.Id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(journal.Id);
        }

        [Fact]
        public void GetById_JournalNotFound_ReturnsNull()
        {
            
        }

        [Fact]
        public void GetAll_JournalsFound_NoLimit_ReturnsAllJournals()
        {
            
        }

        [Fact]
        public void GetAll_JournalsFound_Limit_ReturnsLimitedJournals()
        {
            
        }

        [Fact]
        public void GetAll_JournalsFound_ReturnsSortedByTitleAscending()
        {
            
        }
        
        [Fact]
        public void GetAll_JournalsFound_ReturnsSortedByTitleDescending()
        {
            
        }
        [Fact]
        public void GetAll_JournalsFound_ReturnsSortedByDateAscending()
        {
            
        }
        [Fact]
        public void GetAll_JournalsFound_ReturnsSortedByDateDescending()
        {
            
        }

        [Fact]
        public void Save_JournalSaved_ReturnsJournal()
        {
            
        }

        [Fact]
        public void Save_JournalNull_ReturnsNull()
        {
            
        }

        [Fact]
        public void Update_JournalUpdated_ReturnsJournal()
        {
            
        }

        [Fact]
        public void Update_JournalNull_ReturnsNull()
        {
            
        }

        [Fact]
        public void Delete_JournalDeleted_ReturnsTrue()
        {
            
        }

        [Fact]
        public void Delete_JournalNotFound_ReturnsFalse()
        {
            
        }

        private JournalRepository GetJournalRepo(AppDbContext context) => new JournalRepository(context);
    }
}