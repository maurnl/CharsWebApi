using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Application.Services;
using App.Application.Dtos;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService charsService)
        {
            _characterService = charsService;
        }

        [HttpGet]
        public ActionResult<List<CharacterReadDto>> GetCharacters()
        {
            return _characterService.GetAllCharacters();
        }

        [HttpGet("{id}", Name = "GetCharacterInfo")]
        public ActionResult<CharacterReadDto> GetCharacterInfo(int id)
        {
            var character = _characterService.GetCharacterById(id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpGet("{id}/family")]
        public ActionResult<IEnumerable<RelationshipReadDto>> GetFamilyForCharacter(int id)
        {
            var character = _characterService.GetCharacterById(id);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character.Relationships);
        }

        [HttpPut("{id}")]
        public ActionResult<CharacterReadDto> UpdateCharacter(int id, CharacterUpdateDto newInfo)
        {
            try 
            {
                var character = _characterService.UpdateCharacter(id, newInfo);
                return Ok(character);
            }catch(Exception ex)
            {
                return BadRequest(new { Error = $"Invalid {ex.Message}" });
            }
        }

        [HttpPost("create")]
        public ActionResult<CharacterReadDto> CreateCharacter(CharacterCreateDto character)
        {
            CharacterReadDto createdCharacter;
            try
            {
                createdCharacter = _characterService.CreateCharacter(character);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }

            return CreatedAtRoute(nameof(GetCharacterInfo), new { id = createdCharacter.Id }, createdCharacter);
        }

        [HttpPost("{id}/family/add/{relatedId}")]
        public ActionResult<CharacterReadDto> AddRelationship(int id,
                                            int relatedId,
                                            [FromQuery] string relationship = "stranger")
        {
            try
            {
                _characterService.AddRelationship(id, relatedId, relationship);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var updatedCharacter = _characterService.GetCharacterById(id);
            return Ok(updatedCharacter);
        }
    }
}
