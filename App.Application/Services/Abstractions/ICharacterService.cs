using App.Application.Dtos;

namespace App.Application.Services.Abstractions
{
    public interface ICharacterService
    {
        Task<CharacterReadDto> CreateCharacter(CharacterCreateDto character);
        List<CharacterReadDto> GetAllCharacters();
        CharacterReadDto GetCharacterById(int id);
        void AddRelationship(int characterId, int relatedCharacterId, string relationship);
        CharacterReadDto UpdateCharacter(int characterId, CharacterUpdateDto newCharData);
        CharacterReadDto DeleteCharacter(int characterId);
    }
}