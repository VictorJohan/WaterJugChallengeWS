using Microsoft.AspNetCore.Mvc;
using WaterJugChallengeWS.Models;
using WaterJugChallengeWS.Services;

namespace WaterJugChallengeWS.Controllers
{

   
    [ApiController]
    [Route("api/[controller]")]
    
    public class WaterJugController : ControllerBase
    {
        private readonly WaterJugService _waterJugService;

        public WaterJugController(WaterJugService service)
        {
            _waterJugService = service;
        }


        [HttpPost("solve")]
        public ActionResult Solve([FromBody] WaterJug waterJug)
        {

            var response = _waterJugService.Solve(waterJug);

            return Ok(response);
        }   
    }
}
