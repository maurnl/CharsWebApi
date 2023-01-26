﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.Application.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using App.Core.Model;
using Microsoft.AspNetCore.Authorization;
using App.Application.Services.Abstractions;

namespace App.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharactersController(ICharacterService charsService)
        {
            _characterService = charsService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CharacterReadDto>), 200)]
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
        public Results<Created<CharacterReadDto>, BadRequest> CreateCharacter(CharacterCreateDto character)
        {
            CharacterReadDto createdCharacter;
            try
            {
                createdCharacter = _characterService.CreateCharacter(character);
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

        [HttpDelete("{id}/delete")]
        [ProducesResponseType(typeof(CharacterReadDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CharacterReadDto> DeleteCharacter(int id)
        {
            try
            {
                var character = _characterService.DeleteCharacter(id);
                return Ok(character);
            }catch(Exception)
            {
                return NotFound();
            }
        }
    }
}
