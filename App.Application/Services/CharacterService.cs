using Microsoft.EntityFrameworkCore;
using AutoMapper;
using App.Application.Dtos;
using App.Core.Model;
using App.DataAccess.Abstractions;
using App.Core.Model.Relationships;
using App.Application.Extensions;
using App.Application.Services.Abstractions;

namespace App.Application.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IRepository<Character> _charactersRepo;
        private readonly IRepository<Relationship> _relationshipRepo;
        private readonly IMapper _mapper;

        public CharacterService(
            IRepository<Character> charsRepo,
            IRepository<Relationship> relRepo,
            IMapper mapper)
        {
            _charactersRepo = charsRepo;
            _mapper = mapper;
            _relationshipRepo = relRepo;
        }

        public List<CharacterReadDto> GetAllCharacters()
        {
            var characters = _charactersRepo.GetAll()
                                       .Include(c => c.Relationships)
                                       .Include(c => c.RelatedTo);
            var lista = characters.ToList();
            foreach (var item in characters)
            {
                item.UpdateRelationshipNames();
            }
            return _mapper.Map<IEnumerable<CharacterReadDto>>(characters).ToList();
        }

        public CharacterReadDto GetCharacterById(int id)
        {
            var character = GetAllCharacters().FirstOrDefault(c => c.Id == id);
            return _mapper.Map<CharacterReadDto>(character);
        }

        public void AddRelationship(int characterId, int relatedCharacterId, string relationship)
        {
            if (!_charactersRepo.Exists(characterId) || !_charactersRepo.Exists(relatedCharacterId))
            {
                throw new ArgumentException(nameof(characterId));
            }

            var character = _charactersRepo.Get(characterId);
            var relatedCharacter = _charactersRepo.Get(relatedCharacterId);
            var relationshipBuilder = new RelationshipBuilder();
            var typeFactory = new RelationshipTypeFactory();
            relationshipBuilder.Link(character)
                               .WithCharacter(relatedCharacter)
                               .As(typeFactory.CreateRelationship(relationship));

            var relationshipModel = relationshipBuilder.Build();
            relationshipModel.RelationshipTypeName = relationship;
            _relationshipRepo.Add(relationshipModel);
            _relationshipRepo.SaveChanges();
        }

        public CharacterReadDto CreateCharacter(CharacterCreateDto character)
        {
            if (!GenderExists(character.Gender))
            {
                throw new ArgumentException(nameof(character.Gender));
            }

            var newCharacter = _mapper.Map<Character>(character);

            try
            {
                _charactersRepo.Add(newCharacter);
                _charactersRepo.SaveChanges();
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

        public CharacterReadDto UpdateCharacter(int characterId, CharacterUpdateDto newCharData)
        {
            if (!_charactersRepo.Exists(characterId))
            {
                throw new ArgumentException(nameof(characterId));
            }

            if (!GenderExists(newCharData.Gender))
            {
                throw new ArgumentException(nameof(newCharData.Gender));
            }

            var character = _charactersRepo.Get(characterId);
            character.Gender = (Gender)Enum.Parse(typeof(Gender), newCharData.Gender.FirstLetterToUpper());
            character.Name = newCharData.Name;

            _charactersRepo.Update(character);
            _charactersRepo.SaveChanges();
            return _mapper.Map<CharacterReadDto>(character);
        }

        private bool GenderExists(string name)
        {
            return Enum.GetNames(typeof(Gender)).FirstOrDefault(g => g.ToString() == name.FirstLetterToUpper()) != null;
        }

        public CharacterReadDto DeleteCharacter(int characterId)
        {
            var character = _charactersRepo.Get(characterId);

            if (character is null)
            {
                throw new ArgumentException(nameof(characterId));
            }

            _relationshipRepo.RemoveRange(_relationshipRepo.GetAll().Where(r => r.RelatedCharacterId == characterId || r.CharacterId == characterId));
            _charactersRepo.Remove(character);
            _charactersRepo.SaveChanges();
            _relationshipRepo.SaveChanges();
            return _mapper.Map<CharacterReadDto>(character);
        }
    }
}

