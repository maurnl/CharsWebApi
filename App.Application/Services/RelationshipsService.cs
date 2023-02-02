using App.Application.Services.Abstractions;
using App.Core.Model;
using App.Core.Model.Relationships;
using App.DataAccess.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class RelationshipsService : IRelationshipsService
    {
        private readonly IRepository<Character> _charactersRepo;
        private readonly IRepository<Relationship> _relationshipsRepo;

        public RelationshipsService(IRepository<Character> charactersRepo, IRepository<Relationship> relationshipsRepo)
        {
            _charactersRepo = charactersRepo;
            _relationshipsRepo = relationshipsRepo;
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
            relationshipBuilder.Link(character)
                               .WithCharacter(relatedCharacter)
                               .As(RelationshipTypeFactory.CreateRelationship(relationship));

            var relationshipModel = relationshipBuilder.Build();
            relationshipModel.RelationshipTypeName = relationship;
            _relationshipsRepo.Add(relationshipModel);
            _relationshipsRepo.SaveChanges();
        }

        public void DeleteRelationships(int characterId)
        {
            _relationshipsRepo.RemoveRange(_relationshipsRepo.GetAll().Where(r => r.RelatedCharacterId == characterId || r.CharacterId == characterId));
            _relationshipsRepo.SaveChanges();
        }
    }
}
