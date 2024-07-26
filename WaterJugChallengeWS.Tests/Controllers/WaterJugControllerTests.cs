using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterJugChallengeWS.Controllers;
using WaterJugChallengeWS.Interfaces;
using WaterJugChallengeWS.Models;
using WaterJugChallengeWS.Services;

namespace WaterJugChallengeWS.Tests.Controllers
{
    public class WaterJugControllerTests
    {
        private readonly Mock<WaterJugService> _waterJugServiceMock;
        private readonly Mock<ICacheService> _cacheServiceMock;
        private readonly WaterJugController _controller;

        public WaterJugControllerTests()
        {
            _waterJugServiceMock = new Mock<WaterJugService>();
            _cacheServiceMock = new Mock<ICacheService>();
            _controller = new WaterJugController(_waterJugServiceMock.Object, _cacheServiceMock.Object);
        }

        [Fact]
        public void Solve_ReturnsCachedResponse_WhenAvailable()
        {
            var waterJug = new WaterJug { XCapacity = 3, YCapacity = 5, ZAmountWanted = 4 };
            var expectedResponse = new WaterJugResponse { Solution = new List<string> { "Step 1" } };
            string cacheKey = $"{waterJug.XCapacity}-{waterJug.YCapacity}-{waterJug.ZAmountWanted}";

            // Return the expected response to simulate a cache hit
            _cacheServiceMock.Setup(c => c.Get<WaterJugResponse>(cacheKey)).Returns(expectedResponse);

            var result = _controller.Solve(waterJug) as OkObjectResult;

            // Assert that the response is correct and that it was not recalculated
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);
        }

        [Fact]
        public void Solve_CachesNewResponse_WhenNotCached()
        {
            var waterJug = new WaterJug { XCapacity = 3, YCapacity = 5, ZAmountWanted = 4 };
            var expectedResponse = new WaterJugResponse { Solution = new List<string> { "Step 1" } };
            string cacheKey = $"{waterJug.XCapacity}-{waterJug.YCapacity}-{waterJug.ZAmountWanted}";

            // Return null to simulate a cache miss
            _cacheServiceMock.Setup(c => c.Get<WaterJugResponse>(cacheKey)).Returns((WaterJugResponse)null);
            _waterJugServiceMock.Setup(s => s.SolveChallenge(waterJug)).Returns(expectedResponse);

            var result = _controller.Solve(waterJug) as OkObjectResult;

            // Assert that the response is correct and that it was cached
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(expectedResponse, result.Value);

            _cacheServiceMock.Verify(c => c.Set(cacheKey, expectedResponse, It.IsAny<TimeSpan>()), Times.Once);
        }
    }
}
