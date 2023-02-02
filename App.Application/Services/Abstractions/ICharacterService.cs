using App.Application.Dtos;

namespace App.Application.Services.Abstractions
{
    public interface ICharacterService
    {
        Task<CharacterReadDto> CreateCharacter(CharacterCreateDto character);
        List<CharacterReadDto> GetAllCharacters();
        CharacterReadDto GetCharacterById(int id);
        CharacterReadDto UpdateCharacter(int characterId, CharacterUpdateDto newCharData);
        CharacterReadDto DeleteCharacter(int characterId);
    }
}