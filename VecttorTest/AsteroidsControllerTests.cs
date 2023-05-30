using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Vecttor.Controllers;
using Vecttor.Models;
using Vecttor.Services;

namespace Vecttor.Tests
{
    [TestClass]
    public class AsteroidsControllerTests
    {
        private AsteroidsController _controller;
        private Mock<IAsteroidsService> _mockAsteroidService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAsteroidService = new Mock<IAsteroidsService>();
            _controller = new AsteroidsController(_mockAsteroidService.Object);
        }

        [TestMethod]
        public async Task GetAsteroids_WithValidDays_ReturnsOkResult()
        {
            // Arrange
            int days = 5;
            var expectedAsteroids = new List<AsteroidApiResponse>() { };
            _mockAsteroidService
                .Setup(service => service.GetAsteroids(days))
                 .Returns(Task.FromResult<IEnumerable<AsteroidApiResponse>>(expectedAsteroids));

            // Act
            var result = await _controller.GetAsteroids(days) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(expectedAsteroids, result.Value);
        }

        [TestMethod]
        public async Task GetAsteroids_WithInvalidDays_ReturnsBadRequest()
        {
            // Arrange
            int days = 0;

            // Act
            var result = await _controller.GetAsteroids(days) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.AreEqual("The 'days' parameter must be a value between 1 and 7.", result.Value);
        }
    }
}
