using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Writely.Controllers;
using Writely.Exceptions;
using Writely.Models;
using Writely.Services;
using Xunit;

namespace Writely.UnitTests.Controllers
{
    public class EntriesControllerTests
    {
        [Fact]
        public async Task GetById_CanGetEntry_ReturnsOk()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();

            // Act
            var response = await controller.GetById(1L);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetById_EntryNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailureRequests();

            // Act
            var response = await controller.GetById(1L);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Add_CanAddNewEntry_ReturnsCreatedAt()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();

            // Act
            var response = await controller.Add(new NewEntry());

            // Assert
            response.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task Add_JournalNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailureRequests();

            // Act
            var response = await controller.Add(new NewEntry());

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Update_CanUpdateEntry_ReturnsNoContent()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();

            // Act
            var response = await controller.Update(1L, new EntryUpdate());

            // Assert
            response.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Update_EntryUpdateNull_ReturnsBadRequest()
        {
            // Arrange
            var controller = PrepControllerForFailureRequests();

            // Act
            var response = await controller.Update(1L, null!);

            // Assert
            response.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Update_EntryNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailureRequests();

            // Act
            var response = await controller.Update(1L, new EntryUpdate());

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Delete_CanRemoveEntry_ReturnsOk()
        {
            // Arrange
            var controller = PrepControllerForSuccessfulRequests();

            // Act
            var response = await controller.Delete(1L);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Delete_JournalNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailureRequests();

            // Act
            var response = await controller.Delete(1L);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Delete_EntryNotFound_ReturnsNotFound()
        {
            // Arrange
            var controller = PrepControllerForFailureRequests();

            // Act
            var response = await controller.Delete(1L);

            // Assert
            response.Should().BeOfType<NotFoundObjectResult>();
        }

        private EntriesController PrepControllerForSuccessfulRequests()
        {
            var logger = new Mock<ILogger<EntriesController>>();
            var service = new Mock<IEntryService>();

            service.Setup(s => s.GetById(It.IsAny<long>()))
                .ReturnsAsync(Helpers.GetEntry);
            service.Setup(s => s.Add(It.IsAny<NewEntry>()))
                .ReturnsAsync(Helpers.GetEntry);
            service.Setup(s => s.Update(1L, new EntryUpdate()))
                .ReturnsAsync(Helpers.GetEntry);
            service.Setup(s => s.Remove(1L))
                .ReturnsAsync(Helpers.GetEntry);

            return new EntriesController(logger.Object, service.Object);
        }
        
        private EntriesController PrepControllerForFailureRequests()
        {
            var logger = new Mock<ILogger<EntriesController>>();
            var service = new Mock<IEntryService>();

            service.Setup(s => s.GetById(1L))
                .Throws<EntryNotFoundException>();
            service.Setup(s => s.Add(null!))
                .Throws<JournalNotFoundException>();
            service.Setup(s => s.Add(It.IsAny<NewEntry>()))
                .Throws<JournalNotFoundException>();
            service.Setup(s => s.Update(1L, null!))
                .Throws<ArgumentNullException>();
            service.Setup(s => s.Update(1L, new EntryUpdate()))
                .Throws<EntryNotFoundException>();
            service.Setup(s => s.Remove(1L))
                .Throws<JournalNotFoundException>();

            return new EntriesController(logger.Object, service.Object);
        }
    }
}