using API.Data;
using API.Dtos;
using API.Model;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly IRepository<Character> _repository;
        private readonly IMapper _mapper;

        public CharactersController(
            IRepository<Character> repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<CharacterReadDto>> GetCharacters()
        {
            var characters = _repository.GetAll();
            return Ok(_mapper.Map<IEnumerable<CharacterReadDto>>(characters));
        }
    }
}
