using API.Model;
using API.Model.Relationships;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly MaroDbContext _context;

        public CharacterRepository(MaroDbContext context)
        {
            _context = context;
        }

        public IQueryable<Character> GetAll()
        {
            return _context.Characters.AsQueryable() ?? Enumerable.Empty<Character>().AsQueryable();
        }

        public Character Get(int id)
        {
            return _context.Characters.SingleOrDefault(c => c.Id == id);
        }

        public void Add(Character character)
        {
            if(character is null)
            {
                throw new ArgumentNullException(nameof(character));
            }
            _context.Characters.Add(character);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void AddRelationship(int characterId, int relatedCharacterId, string relationship, RelationshipDirection direction)
        {
            if(!CharacterExists(characterId) || !CharacterExists(relatedCharacterId))
            {
                throw new ArgumentException();
            }

            var character = Get(characterId);
            var relatedCharacter = Get(relatedCharacterId);
            var relationshipBuilder = new RelationshipBuilder();
            var typeFactory = new RelationshipTypeFactory();
            relationshipBuilder.Link(character)
                               .WithCharacter(relatedCharacter)
                               .As(typeFactory.CreateRelationship(relationship));


            var relationshipModel = relationshipBuilder.Build();
            relationshipModel.RelationshipTypeName = relationship;
            _context.Relationships.Add(relationshipModel);
        }

        public bool CharacterExists(int characterId)
        {
            return _context.Characters.Any(c => c.Id == characterId);
        }
    }
}
