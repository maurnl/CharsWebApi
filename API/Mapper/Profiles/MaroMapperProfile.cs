using API.Dtos;
using API.Model;
using API.Model.Relationships;
using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace API.Mapper.Profiles
{
    public class MaroMapperProfile : Profile
    {
        public MaroMapperProfile()
        {
            CreateMap<Character, CharacterReadDto>()
                    .ForMember(c => c.RelatedTo,
                    opt => opt
                    .MapFrom(src => src.RelatedTo
                                    .Select(r => new RelationshipReadDto { CharacterId = r.CharacterId, CharacterName = r.Character.Name, Relationship = r.RelativeRelationshipName })))
                    .ForMember(c => c.Relationships,
                    opt => opt
                    .MapFrom(src => src.Relationships
                                    .Select(r => new RelationshipReadDto { CharacterId = r.RelatedCharacterId, CharacterName = r.RelatedCharacter.Name, Relationship = r.OppositeRelativeRelationshipName })));
            CreateMap<CharacterCreateDto, Character>();
        }
    }
}
