using System.Net;
using FluentAssertions;
using Writely.Models;
using Xunit;

namespace Writely.IntegrationTests.Controllers
{
    public class EntriesControllerTests : TestBase, IClassFixture<WebAppFactory<Startup>>
    {
        public EntriesControllerTests(WebAppFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async void GetById_EntryFound_Returns200()
        {
            // Act
            var response = await _client.GetAsync("api/entries/2");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void GetById_EntryNotFound_Returns404()
        {
            // Act
            var response = await _client.GetAsync("api/entries/087293");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Add_JournalFound_Returns201()
        {
            // Arrange
            var newEntry = new NewEntry
            {
                UserId = "UserIdBob",
                JournalId = 1L,
                Title = "Blah",
                Tags = "flarp,shrimp,bagels",
                Body = "Lookie here, punk."
            };
            
            // Act
            var response = await _client.PostAsync("api/entries", newEntry.ToJson());

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Add_JournalNotFound_Returns404()
        {
            // Arrange
            var newEntry = new NewEntry
            {
                UserId = "UserIdBob",
                JournalId = 8L,
                Title = "Blah",
                Tags = "flarp,shrimp,bagels",
                Body = "Lookie here, punk."
            };
            
            // Act
            var response = await _client.PostAsync("api/entries", newEntry.ToJson());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Update_EntryFound_Returns204()
        {
            // Arrange
            var update = new EntryUpdate
            {
                Title = "Spiffy New Title",
                Tags = "un,deux,trois",
                Body = "Yep, there's some text here."
            };

            // Act
            var response = await _client.PatchAsync("api/entries/5", update.ToJson());

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Update_EntryNotFound_Returns404()
        {
            // Arrange
            var update = new EntryUpdate();

            // Act
            var response = await _client.PatchAsync("api/entries/48023", update.ToJson());

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Delete_EntryFound_Returns200()
        {
            // Act
            var response = await _client.DeleteAsync("api/entries/1");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async void Delete_EntryNotFound_Returns404()
        {
            // Act
            var response = await _client.DeleteAsync("api/entries/320394");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}