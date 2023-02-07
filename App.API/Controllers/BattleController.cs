using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        [Authorize]
        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return Ok("pong!");
        }
    }
}
