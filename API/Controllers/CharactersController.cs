using API.Data;
using API.Dtos;
using API.Model;
using API.Model.Relationships;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _repository;
        private readonly IMapper _mapper;

        public CharactersController(
            ICharacterRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<CharacterReadDto>> GetCharacters()
        {
            var characters = _repository.GetAll().Include(c => c.RelatedChars).Include(c => c.RelatedToChars).Include(c => c.Relationships).Include(c => c.RelatedChars).ToList();
            return Ok(_mapper.Map<IEnumerable<CharacterReadDto>>(characters));
        }

        [HttpGet("{id}", Name = "GetCharacterInfo")]
        public ActionResult<CharacterReadDto> GetCharacterInfo(int id)
        {
            var character = _repository.Get(id);

            if(character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpGet("{id}/family")]
        public ActionResult<IEnumerable<RelationshipReadDto>> GetFamilyForCharacter(int id)
        {
            var character = _repository.Get(id);

            if(character == null)
            {
                return NotFound();
            }

            var family = _mapper.Map<IEnumerable<RelationshipReadDto>>(character.Relationships);
            return Ok(family);
        }

        [HttpPost("create")]
        public ActionResult<CharacterReadDto> CreateCharacter(CharacterCreateDto character)
        {
            var characterModel = _mapper.Map<Character>(character);

            try
            {
                _repository.Add(characterModel);
                _repository.SaveChanges();
            }
            catch(DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            catch(ArgumentNullException)
            {
                return BadRequest();
            }

            var readCharacter = _mapper.Map<CharacterReadDto>(characterModel);
            return CreatedAtRoute(nameof(GetCharacterInfo), new { id = readCharacter.Id }, readCharacter);
        }

        [HttpPatch("{id}/family/add/{relatedId}")]
        public ActionResult<CharacterReadDto> AddRelationship(int id,
                                            int relatedId,
                                            [FromQuery] string relationship = "stranger",
                                            [FromQuery] RelationshipDirection direction = RelationshipDirection.NonDirectional)
        {
            try
            {
                _repository.AddRelationship(id, relatedId, relationship, direction);
                _repository.SaveChanges();
            }catch(ArgumentException)
            {
                return NotFound();
            }catch(DbUpdateConcurrencyException)
            {
                return BadRequest();
            }catch(NotSupportedException)
            {
                return BadRequest();
            }

            var updatedCharacter = _mapper.Map<CharacterReadDto>(_repository.Get(id));
            return Ok(updatedCharacter);
        }
    }
}
