using App.Application.Dtos;

namespace App.Application.Services
{
    public interface ICharacterService
    {
        CharacterReadDto CreateCharacter(CharacterCreateDto character);
        List<CharacterReadDto> GetAllCharacters();
        CharacterReadDto GetCharacterById(int id);
        void AddRelationship(int characterId, int relatedCharacterId, string relationship);
    }
}