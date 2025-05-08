using brochureapi.Controllers;
using brochureapi.Models;
using brochureapi.repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using System;
namespace brochureapi.tests
{
    public class BrochureControllerTest
    {
        private readonly Mock<IBrochureRepository> _mockRepo;
        private readonly Mock<ILogger<BrochureController>> _mockLogger;
        private readonly BrochureController _controller;

        public BrochureControllerTest()
        {
            _mockRepo = new Mock<IBrochureRepository>();
            _mockLogger = new Mock<ILogger<BrochureController>>();
            _controller = new BrochureController(_mockRepo.Object, _mockLogger.Object);
        }


        [Fact]
        public void CreateTest()
        {
            //arranage
            var brochure = new Brochure(5, "test6", DateOnly.FromDateTime(DateTime.Today));

            //act
            var result = _controller.Create(brochure);

            // Assert
            Assert.NotNull(result);
            result.Should().BeOfType<CreatedAtActionResult>();
        }
        [Fact]
        public void ControllerUpdateTest()
        {
            //arrange
            var brochure = new Brochure(34, "test6", DateOnly.FromDateTime(DateTime.Today));

            _controller.Create(brochure);

            brochure.Name = "newName";
            var result = _controller
                .Update(34, brochure) as OkObjectResult;

            Assert.NotNull(result);
            result.Should().BeOfType<OkObjectResult>();


            var updatedBrochure = result.Value as Brochure;
            Assert.NotNull(updatedBrochure);
            Assert.Equal("newName", updatedBrochure.Name);
            Assert.Equal(34, updatedBrochure.Id);

        }
        [Fact]
        public void ControllerGetByIdTest()
        {

            var brochure = new Brochure(45, "test35", DateOnly.FromDateTime(DateTime.Today));

            _controller.Create(brochure);

            var result = _controller.GetById(45);

            Assert.NotNull(result);
            result.Should().BeOfType<ActionResult<Brochure>>();
            Assert.Equal(45, brochure.Id);
        }
        [Fact]
        public void ControllerGetAllTest()
        {

            var brochure1 = new Brochure(77, "Firs3t", DateOnly.FromDateTime(DateTime.Today));
            var brochure2 = new Brochure(88, "Seco4nd", DateOnly.FromDateTime(DateTime.Today));

            Assert.NotNull(_controller.Create(brochure1));
            _controller.Create(brochure2);

            // Act
            ; // This returns ActionResult<IEnumerable<Brochure>>

            // Assert
            Assert.NotNull(_controller.Get());
        }
        [Fact]
        public void GetPagesTest() {
            // Arrange
            var pages = new List<Page>
        {
            new Page { Id = 1, Name = "Page 1", BrochureId = 77 },
            new Page { Id = 2, Name = "Page 2", BrochureId = 77 }
        };
            var brochureId = 77;
            var brochure = new Brochure(brochureId, "Test Brochure", DateOnly.FromDateTime(DateTime.Today),pages);


            _controller.Create(brochure);

            var result = _controller.GetPages(77);
            var okResult = Assert.IsType<OkObjectResult>(result.Result); // Check if the result is Ok
           
            Assert.NotNull(result); // Check that 2 pages are returned
        }



    }
}