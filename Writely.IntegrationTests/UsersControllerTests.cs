using System.Threading.Tasks;
using Writely.Controllers;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.IntegrationTests
{
    public class UsersControllerTests : TestBase,  IClassFixture<WebAppFactory<Startup>>
    {
        
        public UsersControllerTests(WebAppFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Register_NewUser_Returns201()
        {
            // Arrange

            // Act

            // Assert
        }
        
        [Fact]
        public async Task Register_UserAlreadyRegistered_Returns400()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task ChangeEmail_ChangeSuccessful_Returns201()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task ChangeEmail_IncompleteAccountUpdate_Returns400()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task ChangePassword_ChangeSuccessful_Returns201()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task ChangePassword_IncompleteAccountUpdate_Returns400()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task DeleteAccount_AccountDeleted_Returns200()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
        
        [Fact]
        public async Task DeleteAccount_AccountNotFound_Returns400()
        {
            // Arrange
            
            // Act
            
            // Assert
        }
    }
}