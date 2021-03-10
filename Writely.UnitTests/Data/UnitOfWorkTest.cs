using System.Threading.Tasks;
using FluentAssertions;
using Writely.Data;
using Xunit;

namespace Writely.UnitTests.Data
{
    public class UnitOfWorkTest : DatabaseTestBase
    {
        [Theory]
        [InlineData(3)]
        [InlineData(6)]
        [InlineData(10)]
        public async Task Complete_CanSaveChanges_ReturnsNumberOfChangesSaved(int numOfJournals)
        {
            // Arrange
            await PrepareDatabase();
            var journals = Helpers.GetJournals(numOfJournals);
            var unitOfWork = new UnitOfWork(Context!) { UserId = "UserId"};

            // Act
            unitOfWork.Journals.AddRange(journals);
            var result = await unitOfWork.Complete();

            // Assert
            result.Should().Be(numOfJournals);
        }
    }
}