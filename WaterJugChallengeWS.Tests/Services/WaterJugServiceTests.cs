using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WaterJugChallengeWS.Models;
using WaterJugChallengeWS.Services;

namespace WaterJugChallengeWS.Tests.Services
{
    public class WaterJugServiceTests
    {
        private readonly WaterJugService _service;

        public WaterJugServiceTests()
        {
            _service = new WaterJugService();
        }

        [Fact]
        public void SolveChallenge_NoSolution_IfAmountWantedGreaterThanSumOfCapacities()
        {
            
            var waterJug = new WaterJug
            {
                XCapacity = 3,
                YCapacity = 5,
                ZAmountWanted = 9
            };

          
            var response = _service.SolveChallenge(waterJug);

           
            Assert.Single(response.Solution);
            Assert.Equal("No Solution.", response.Solution[0]);
        }

        [Fact]
        public void SolveChallenge_FindsSolution_ForSimpleCase()
        {
            
            var waterJug = new WaterJug
            {
                XCapacity = 3,
                YCapacity = 5,
                ZAmountWanted = 4
            };

          
            var response = _service.SolveChallenge(waterJug);

           
            Assert.NotEmpty(response.Solution);
        }

        [Fact]
        public void SolveChallenge_FindsSolution_ForExactCapacity()
        {
           
            var waterJug = new WaterJug
            {
                XCapacity = 3,
                YCapacity = 5,
                ZAmountWanted = 5
            };

           
            var response = _service.SolveChallenge(waterJug);

           
            Assert.Contains("Fill bucket Y", response.Solution);
        }

        [Fact]
        public void SolveChallenge_FindsSolution_ForMultipleSteps()
        {
            
            var waterJug = new WaterJug
            {
                XCapacity = 2,
                YCapacity = 6,
                ZAmountWanted = 4
            };

           
            var response = _service.SolveChallenge(waterJug);

            
            Assert.NotEmpty(response.Solution);
            Assert.True(response.Solution.Count > 1);
        }
    }
}

