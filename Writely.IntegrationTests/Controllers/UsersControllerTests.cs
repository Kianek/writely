using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Writely.Models;
using Xunit;

namespace Writely.IntegrationTests
{
    public class UsersControllerTests
    {
        public class UnregisteredUsersControllerTests : TestBase, IClassFixture<WebAppFactory<Startup>>
        {
            private Registration _registration;
            
            public UnregisteredUsersControllerTests(WebAppFactory<Startup> factory) : base(factory)
            {
                _registration = new Registration
                {
                    FirstName = "Robert",
                    LastName = "Zombie",
                    Username = "Dragula666",
                    Email = "rob@scaryguy.com",
                    Password = "SecretScaryPassword123!",
                    ConfirmPassword = "SecretScaryPassword123!",
                };
            }


            [Fact]
            public async Task Register_NewUser_Returns200()
            {
                // Arrange
                var registrationJson = _registration.ToJson();

                // Act
                var response = await _client.PostAsync("api/users/register", registrationJson);

                // Assert
                response.EnsureSuccessStatusCode();
                response.StatusCode.Should().Be(HttpStatusCode.OK);
            }

            [Fact]
            public async Task Register_UserAlreadyRegistered_Returns400()
            {
                // Arrange
                var registrationJson = _registration.ToJson();

                // Act
                var response = await _client.PostAsync("api/users/register", registrationJson);

                // Assert
                response.IsSuccessStatusCode.Should().BeFalse();
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        public class RegisteredUserUpdateTests : TestBase, IClassFixture<WebAppFactory<Startup>>
        {
            public RegisteredUserUpdateTests(WebAppFactory<Startup> factory) : base(factory)
            {
            }

            [Fact]
            public async Task ChangeEmail_ChangeSuccessful_Returns204()
            {
                // Arrange
                var update =  new EmailUpdate("UserIdBob","bob@newmail.com")
                    .ToJson();

                // Act
                var response = await _client.PatchAsync("api/users/change-email", update);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }

            [Fact]
            public async Task ChangeEmail_IncompleteAccountUpdate_Returns400()
            {
                // Arrange
                var update = new EmailUpdate("UserIdBob", null!).ToJson();

                // Act
                var response = await _client.PatchAsync("api/users/change-email", update);

                // Assert
                response.IsSuccessStatusCode.Should().BeFalse();
            }

            [Fact]
            public async Task ChangePassword_ChangeSuccessful_Returns204()
            {
                // Arrange
                var update = new PasswordUpdate
                ("UserIdBob",
                    "TotallyHashedPassword123!",
                    "SuperCoolNewPassword123!").ToJson();

                // Act
                var response = await _client.PatchAsync("api/users/change-password", update);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            }

            [Fact]
            public async Task ChangePassword_IncompleteAccountUpdate_Returns400()
            {
                // Arrange
                var update = new PasswordUpdate("UserIdBob", null!, "Ladeedaa3333$4@")
                    .ToJson();

                // Act
                var response = await _client.PatchAsync("api/users/change-password", update);

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }

        public class DeleteUserTests : TestBase, IClassFixture<WebAppFactory<Startup>>
        {
            public DeleteUserTests(WebAppFactory<Startup> factory) : base(factory)
            {
            }


            [Fact]
            public async Task DeleteAccount_AccountDeleted_Returns200()
            {
                // Act
                var response = await _client.DeleteAsync("api/users/UserIdBob");

                // Assert
                response.EnsureSuccessStatusCode();
            }

            [Fact]
            public async Task DeleteAccount_AccountNotFound_Returns400()
            {
                // Act
                var response = await _client.DeleteAsync("api/users/SomeUserWhoDoesntExist");

                // Assert
                response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            }
        }
    }
}