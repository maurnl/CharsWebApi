using API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<CharacterReadDto>> GetCharacters()
        {
            return new List<CharacterReadDto>
            {
                new CharacterReadDto
                {
                    Id = 1,
                    Name = "Test"
                },
                new CharacterReadDto
                {
                    Id = 2,
                    Name = "Test"
                },                
                new CharacterReadDto
                {
                    Id = 3,
                    Name = "Test"
                },
            };
        }
    }
}
