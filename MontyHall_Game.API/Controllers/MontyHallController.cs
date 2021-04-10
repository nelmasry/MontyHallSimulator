using Microsoft.AspNetCore.Mvc;
using MontyHall.API.Service;
using MontyHall_Game.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MontyHall_Game.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyHallController : ControllerBase
    {
        private IMontyHallService _service;
        public MontyHallController(IMontyHallService service)
        {
            _service = service;
        }


        [HttpGet("simulatemontyhall/{totalGamesCount}/{stickToSameDoor}")]
        public async Task<ActionResult> SimulateMontyHall(int totalGamesCount, bool stickToSameDoor)
        {
            try
            {
                if (totalGamesCount > 0)
                {
                    int wins = await _service.SimulateMontyHall(totalGamesCount, stickToSameDoor);
                    MontyHallSimulationResponse mhresponse = new MontyHallSimulationResponse()
                    {
                        Id = Guid.NewGuid().ToString(),
                        TotalWins = wins,
                        TotalLoss = totalGamesCount - wins,
                        TotalGames = totalGamesCount
                    };
                    return Ok(mhresponse);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
