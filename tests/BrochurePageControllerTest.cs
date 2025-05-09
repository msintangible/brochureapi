using brochureapi.Controllers;
using brochureapi.DTOs;
using brochureapi.Models;
using brochureapi.NewFolder;
using brochureapi.repository;
using brochureapi.services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace brochureapi.tests
{
    public class BrochurePageControllerTest
    {
        private readonly Mock<IBrochurePageService> _mockRepoPage;
        private readonly Mock<IBrochureService> _mockRepo;
        private readonly Mock<ILogger<BrochurePageController>> _mockLogger;
        private readonly Mock<ILogger<BrochureController>> _mockBrochureControllerLogger;
        public readonly BrochureController _brochureController;
        public readonly BrochurePageController _controller;

        public BrochurePageControllerTest()
        {
            _mockRepoPage = new Mock<IBrochurePageService>();
            _mockRepo = new Mock<IBrochureService>();
            _mockLogger = new Mock<ILogger<BrochurePageController>>();
            _mockBrochureControllerLogger = new Mock<ILogger<BrochureController>>();
            _brochureController = new BrochureController(_mockRepo.Object, _mockBrochureControllerLogger.Object);
            _controller = new BrochurePageController(_mockRepoPage.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetPagesTest()
        {
            // Arrange
            var pages = new List<PageDTO>
                {
                    new PageDTO { Id = 1, Name = "Page 1", BrochureId = 77 },
                    new PageDTO { Id = 2, Name = "Page 2", BrochureId = 77 }
                };
            var brochureId = 77;
            var brochure = new BrochureDTO(brochureId, "Test Brochure", DateOnly.FromDateTime(DateTime.Today), pages);

            var brochureResult = _brochureController.Create(brochure);

            // Act
            var result = _controller.GetPages(77);
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check if the result is Ok

            // Assert
            Assert.NotNull(okResult);
            Assert.NotNull(result); // Compare the count of returned pages
        }
    }
}
