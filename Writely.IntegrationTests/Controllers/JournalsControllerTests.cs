using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Models;
using Writely.Models.Dto;
using Xunit;

namespace Writely.IntegrationTests.Controllers
{
    public class JournalsControllerTests : TestBase, IClassFixture<WebAppFactory<Startup>>
    {
        public JournalsControllerTests(WebAppFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetById_JournalFound_Returns200()
        {
            // Act
            var response = await _client.GetAsync("api/journals/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetById_JournalNotFound_Returns404()
        {
            // Act
            var response = await _client.GetAsync("api/journals/234");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task GetAll_Returns200()
        {
            // Act
            var response = await _client.GetAsync("api/journals");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetEntriesByJournal_JournalFound_Returns200()
        {
            // Act
            var response = await _client.GetAsync("api/journals/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetEntriesByJournal_JournalNotFound_Returns404()
        {
            // Act
            var response = await _client.GetAsync("api/journals/493098045");
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Add_JournalAdded_Returns201()
        {
            // Arrange
            var newJournal = new NewJournal {Title = "Fancy New Journal", UserId = "UserIdBob"};
            
            // Act
            var response = await _client.PostAsync("api/journals", newJournal.ToJson());

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_JournalFound_Returns204()
        {
            // Arrange
            var journalUpdate = new JournalUpdate {Title = "Spiffy New Title"};

            // Act
            var response = await _client.PatchAsync("api/journals/1", journalUpdate.ToJson());

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Update_JournalNotFound_Returns404()
        {
            // Arrange
            var journalUpdate = new JournalUpdate {Title = "Spiffy New Title"};

            // Act
            var response = await _client.PatchAsync("api/journals/184732", journalUpdate.ToJson());
            
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Remove_JournalFound_Returns200()
        {
            // Act
            var response = await _client.DeleteAsync("api/journals/2");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Remove_JournalNotFound_Returns404()
        {
            // Act
            var response = await _client.DeleteAsync("api/journals/47463");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}