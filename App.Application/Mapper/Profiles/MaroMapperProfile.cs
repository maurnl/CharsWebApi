using App.Application.Dtos;
using App.Application.Extensions;
using App.Core.Model;
using AutoMapper;

namespace App.Application.Mapper.Profiles
{
    public class MaroMapperProfile : Profile
    {
        public MaroMapperProfile()
        {
            CreateMap<Character, CharacterReadDto>()
                    .ForMember(c => c.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                    .ForMember(c => c.RelatedTo,
                    opt => opt
                    .MapFrom(src => src.RelatedTo
                                    .Select(r => new RelationshipReadDto { CharacterId = r.CharacterId, CharacterName = r.Character.Name, Relationship = r.RelativeRelationshipName })))
                    .ForMember(c => c.Relationships,
                    opt => opt
                    .MapFrom(src => src.Relationships
                                    .Select(r => new RelationshipReadDto { CharacterId = r.RelatedCharacterId, CharacterName = r.RelatedCharacter.Name, Relationship = r.OppositeRelativeRelationshipName })));
            CreateMap<CharacterCreateDto, Character>()
                    .ForMember(g => g.Gender, opt => opt.MapFrom(g => (Gender)Enum.Parse(typeof(Gender), g.Gender.FirstLetterToUpper())));
        }
    }
}
