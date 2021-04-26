using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Writely.Controllers;
using Writely.Exceptions;
using Writely.Models;
using Writely.Models.Dto;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Controllers
{
    public class JournalsControllerTests
    {
        [Fact]
        public async Task GetById_CanGetJournal_ReturnsOk()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();
            
            // Act
            var response = await controller.GetById(5L);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public async Task GetById_JournalNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests();

            // Act
            var response = await controller.GetById(1L);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }
        
        [Fact]
        public async Task GetById_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests(null);

            // Act
            var response = await controller.GetById(1L);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetAll_CanFetchJournals_ReturnsOk()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();

            // Act
            var response = await controller.GetAll(new QueryFilter());

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public async Task GetAll_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests(null!);

            // Act
            var response = await controller.GetAll(new QueryFilter());

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task GetEntriesByJournal_JournalFound_ReturnsEntries()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();

            // Act
            var response = await controller.GetEntriesByJournal(1L, new QueryFilter());

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }
        
        [Fact]
        public async Task GetEntriesByJournal_JournalNotFound_ThrowsNotFoundException() {
            // Arrange
            var controller = PrepControllerForFailedRequests();

            // Act
            var response = await controller.GetEntriesByJournal(1L, new QueryFilter());
            
            // Act
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Add_CanAddNewJournal_ReturnsCreated()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();
            var newJournal = new NewJournal {Title = "Fancy Journal", UserId = "UserId"};

            // Act
            var response = await controller.Add(newJournal);

            // Assert
            response.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Add_UserNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests(null);
            var newJournal = new NewJournal();

            // Act
            var response = await controller.Add(newJournal);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Add_NewJournalNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests();

            // Act
            var response = await controller.Add(null!);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_UpdateSuccessful_ReturnsNoContent()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();
            var journalUpdate = new JournalUpdate {Title = "Spiffy Title" };

            // Act
            var response = await controller.Update(1L, journalUpdate);

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update_UpdateModelNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests();

            // Act
            var response = await controller.Update(1L, null!);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_JournalNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests();
            var journalUpdate = new JournalUpdate {Title = "Spiffy Title"};

            // Act
            var response = await controller.Update(1L, journalUpdate);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Remove_RemoveSuccessful_ReturnsOk()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();

            // Act
            var response = await controller.Remove(1L);

            // Assert
            response.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Remove_JournalNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailedRequests();

            // Act
            var response = await controller.Remove(1L);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        private JournalsController PrepControllerForSuccessfulRequests()
        {
            var service = new Mock<IJournalService>();
            var logger = new Mock<ILogger<JournalsController>>();

            service.SetupProperty(js => js.UserId, "UserId");
            service.Setup(js => js.GetById(It.IsAny<long>()))
                .ReturnsAsync(Helpers.GetJournal);
            service.Setup(js => js.GetAll(new QueryFilter()))
                .ReturnsAsync(Helpers.GetJournals(5));
            service.Setup(js => js.GetEntriesByJournal(It.IsAny<long>(), It.IsAny<QueryFilter>()))
                .ReturnsAsync(Helpers.GetEntries(5));
            service.Setup(js => js.Add(It.IsAny<NewJournal>()))
                .ReturnsAsync(Helpers.GetJournal);
            service.Setup(js => js.Update(It.IsAny<long>(), It.IsAny<JournalUpdate>()))
                .ReturnsAsync(1);
            service.Setup(js => js.Remove(It.IsAny<long>()))
                .ReturnsAsync(1);

            return new(logger.Object, service.Object);
        }

        private JournalsController PrepControllerForFailedRequests(string? userId = "userId")
        {
            var service = new Mock<IJournalService>();
            var logger = new Mock<ILogger<JournalsController>>();
            
            if (userId == null)
            {
                service.SetupProperty(js => js.UserId, "");
            }
            else
            {
                service.SetupProperty(js => js.UserId, "UserId");
            }
            service.Setup(js => js.GetById(It.IsAny<long>()))
                .Throws<JournalNotFoundException>();
            service.Setup(js => js.GetAll(It.IsAny<QueryFilter>()))
                .Throws<UserNotFoundException>();
            service.Setup(js => js.GetEntriesByJournal(It.IsAny<long>(), It.IsAny<QueryFilter>()))
                .Throws<JournalNotFoundException>();
            service.Setup(js => js.Add(It.IsAny<NewJournal>()))
                .Throws<UserNotFoundException>();
            service.Setup(js => js.Add(null!))
                .Throws<ArgumentNullException>();
            service.Setup(js => js.Update(It.IsAny<long>(), It.IsAny<JournalUpdate>()))
                .Throws<JournalNotFoundException>();
            service.Setup(js => js.Update(It.IsAny<long>(), null!))
                .Throws<ArgumentNullException>();
            service.Setup(js => js.Remove(It.IsAny<long>()))
                .Throws<JournalNotFoundException>();

            return new(logger.Object, service.Object);
        }
    }
}