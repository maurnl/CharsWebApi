using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Application.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using App.Core.Model;
using Microsoft.AspNetCore.Authorization;
using App.Application.Services.Abstractions;

namespace App.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly IRelationshipsService _relationshipsService;

        public CharactersController(ICharacterService charsService,
                    IRelationshipsService relationshipsService)
        {
            _characterService = charsService;
            _relationshipsService = relationshipsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CharacterReadDto>), StatusCodes.Status200OK)]
        public IResult GetCharacters()
        {
            var characters = _characterService.GetAllCharacters();
            return TypedResults.Ok(characters);
        }

        [HttpGet("{id}", Name = "GetCharacterInfo")]
        [ProducesResponseType(typeof(CharacterReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Results<Ok<CharacterReadDto>, NotFound> GetCharacterInfo(int id)
        {
            var character = _characterService.GetCharacterById(id);

            if (character is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(character);
        }

        [HttpGet("{id}/family")]
        [ProducesResponseType(typeof(List<List<RelationshipReadDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Results<Ok<List<List<RelationshipReadDto>>>, NotFound> GetFamilyForCharacter(int id)
        {
            var character = _characterService.GetCharacterById(id);

            if (character is null)
            {
                return TypedResults.NotFound();
            }
             var relationshipsLists = new List<List<RelationshipReadDto>>
                {
                    character.Relationships.ToList(),
                    character.RelatedTo.ToList()
                };
            return TypedResults.Ok(relationshipsLists);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CharacterReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Results<Ok<CharacterReadDto>, NotFound> UpdateCharacter(int id, CharacterUpdateDto newInfo)
        {
            try 
            {
                var character = _characterService.UpdateCharacter(id, newInfo);
                return TypedResults.Ok(character);
            }catch
            {
                return TypedResults.NotFound();
            }
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CharacterReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Results<Created<CharacterReadDto>, BadRequest>> CreateCharacter(CharacterCreateDto character)
        {
            CharacterReadDto createdCharacter;
            try
            {
                createdCharacter = await _characterService.CreateCharacter(character);
            }
            catch (DbUpdateConcurrencyException)
            {
                return TypedResults.BadRequest();
            }
            catch (ArgumentNullException)
            {
                return TypedResults.BadRequest();
            }

            var uri = Url.Action(nameof(GetCharacterInfo), new { id = createdCharacter.Id }) ?? $"/{createdCharacter.Id}";
            return TypedResults.Created(uri, createdCharacter);
        }

        [HttpPost("{id}/family/add/{relatedId}")]
        [ProducesResponseType(typeof(CharacterReadDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Results<Ok<CharacterReadDto>, NotFound, BadRequest> AddRelationship(int id,
                                            int relatedId,
                                            [FromQuery] string relationship = "stranger")
        {
            try
            {
                _relationshipsService.AddRelationship(id, relatedId, relationship);
            }
            catch (ArgumentException)
            {
                return TypedResults.NotFound();
            }
            catch (Exception)
            {
                return TypedResults.BadRequest();
            }

            var updatedCharacter = _characterService.GetCharacterById(id);
            return TypedResults.Ok(updatedCharacter);
        }

        [HttpDelete("{id}/delete")]
        [ProducesResponseType(typeof(CharacterReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Results<Ok<CharacterReadDto>, NotFound> DeleteCharacter(int id)
        {
            try
            {
                var character = _characterService.DeleteCharacter(id);
                _relationshipsService.DeleteRelationships(id);
                return TypedResults.Ok(character);
            }catch(Exception)
            {
                return TypedResults.NotFound();
            }
        }
    }
}
