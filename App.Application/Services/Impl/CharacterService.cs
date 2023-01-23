using Microsoft.EntityFrameworkCore;
using AutoMapper;
using App.Application.Dtos;
using App.Core.Model;
using App.DataAccess.Abstractions;
using App.Core.Model.Relationships;

namespace App.Application.Services.Impl
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> _chararctersRepo;
        private readonly IRepository<Relationship> _relationshipRepo;
        private readonly IMapper _mapper;

        public CharacterService(
            IRepository<Character> charsRepo,
            IRepository<Relationship> relRepo,
            IMapper mapper)
        {
            _chararctersRepo = charsRepo;
            _mapper = mapper;
            _relationshipRepo = relRepo;
        }

        public List<CharacterReadDto> GetAllCharacters()
        {
            var characters = _chararctersRepo.GetAll()
                                       .Include(c => c.Relationships)
                                       .Include(c => c.RelatedTo);
            foreach (var item in characters)
            {
                item.UpdateRelationshipNames();
            }
            return _mapper.Map<IEnumerable<CharacterReadDto>>(characters).ToList();
        }

        public CharacterReadDto GetCharacterById(int id)
        {
            return _mapper.Map<CharacterReadDto>(_chararctersRepo.Get(id));
        }

        public void AddRelationship(int characterId, int relatedCharacterId, string relationship)
        {
            if (!_chararctersRepo.Exists(characterId) || !_chararctersRepo.Exists(relatedCharacterId))
            {
                throw new ArgumentException(nameof(characterId));
            }

            var character = _chararctersRepo.Get(characterId);
            var relatedCharacter = _chararctersRepo.Get(relatedCharacterId);
            var relationshipBuilder = new RelationshipBuilder();
            var typeFactory = new RelationshipTypeFactory();
            relationshipBuilder.Link(character)
                               .WithCharacter(relatedCharacter)
                               .As(typeFactory.CreateRelationship(relationship));


            var relationshipModel = relationshipBuilder.Build();
            relationshipModel.RelationshipTypeName = relationship;
            _relationshipRepo.Add(relationshipModel);
        }

        public CharacterReadDto CreateCharacter(CharacterCreateDto character)
        {
            var newCharacter = _mapper.Map<Character>(character);
            try
            {
                _chararctersRepo.Add(newCharacter);
                _chararctersRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }
            catch (Exception e)
            {
                throw e;
            }

            return _mapper.Map<CharacterReadDto>(newCharacter);
        }

    }
}

