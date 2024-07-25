using Microsoft.AspNetCore.Mvc;
using WaterJugChallengeWS.Interfaces;
using WaterJugChallengeWS.Models;
using WaterJugChallengeWS.Services;

namespace WaterJugChallengeWS.Controllers
{

   
    [ApiController]
    [Route("api/[controller]")]
    
    public class WaterJugController : ControllerBase
    {
        private readonly WaterJugService _waterJugService;
        private readonly ICacheService _cacheService;

        public WaterJugController(WaterJugService service, ICacheService cacheService)
        {
            _waterJugService = service;
            _cacheService = cacheService;
        }


        [HttpPost("solveChallenge")]
        public ActionResult Solve([FromBody] WaterJug waterJug)
        {
            string cacheKey = $"{waterJug.XCapacity}-{waterJug.YCapacity}-{waterJug.ZAmountWanted}";
            var response = _cacheService.Get<WaterJugResponse>(cacheKey);

            if (response is null)
            {
                response = new WaterJugResponse();
               
                response = _waterJugService.SolveChallenge(waterJug);

                _cacheService.Set(cacheKey, response, TimeSpan.FromMinutes(5));
            }

          
            return Ok(response);
        }   
    }
}
