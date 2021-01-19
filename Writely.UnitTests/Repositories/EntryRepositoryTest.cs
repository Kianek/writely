using System.Threading.Tasks;
using Xunit;

namespace Writely.UnitTests.Repositories
{
    public class EntryRepositoryTest : RepositoryTestBase
    {
        [Fact]
        public async Task GetById_EntryFound_ReturnsEntry()
        {

        }

        [Fact]
        public async Task GetById_EntryNotFound_ReturnsNull()
        {

        }

        [Fact]
        public async Task GetAllByJournal_JournalFound_ReturnsEntries()
        {

        }

        [Fact]
        public async Task GetAllByJournal_JournalNotFound_ReturnsNull()
        {

        }

        [Fact]
        public async Task GetAllByTag_JournalFound_ReturnsMatchingEntries()
        {

        }

        [Fact]
        public async Task GetAllByTag_JournalFound_NoMatches_ReturnsNull()
        {
            
        }

        [Fact]
        public async Task Save_EntrySaved_ReturnsEntry()
        {
            
        }

        [Fact]
        public async Task Save_EntryNull_ThrowsArgumentNullException()
        {
            
        }

        [Fact]
        public async Task Update_EntryUpdated_ReturnEntry()
        {
            
        }

        [Fact]
        public async Task Update_EntryNull_ThrowsArgumentNullException()
        {
            
        }

        [Fact]
        public async Task Delete_JournalAndEntryFound_EntryDeleted_ReturnsTrue()
        {
            
        }

        [Fact]
        public async Task Delete_JournalNotFound_ReturnsFalse()
        {
            
        }

        [Fact]
        public async Task Delete_EntryNotFound_ReturnsFalse()
        {
            
        }
    }
}