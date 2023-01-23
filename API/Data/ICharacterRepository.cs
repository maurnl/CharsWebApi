using API.Model;
using API.Model.Relationships;

namespace API.Data
{
    public interface ICharacterRepository
    {
        IQueryable<Character> GetAll();
        Character Get(int id);
        void Add(Character entity);
        void AddRelationship(int characterId, int relatedCharacterId, string relationship);
        bool CharacterExists(int characterId);
        void SaveChanges();
    }
}
