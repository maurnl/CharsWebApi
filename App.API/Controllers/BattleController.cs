using App.Application.Dtos;
using App.Application.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _battleService;

        public BattleController(IBattleService battleService)
        {
            _battleService = battleService;
        }

        [HttpPost("new")]
        public Results<Ok<BattleResultReadDto>, BadRequest> NewBattle(int characterOneId, int characterTwoId)
        {
            try
            {
                var result = _battleService.GenerateBattle(characterOneId, characterTwoId);
                return TypedResults.Ok(result);
            }
            catch (ArgumentException ex)
            {
                return TypedResults.BadRequest();
            }
        }
    }
}
