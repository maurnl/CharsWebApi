using API.Dtos;
using API.Extensions;
using API.Model;
using API.Model.Relationships;
using AutoMapper;

namespace API.Mapper.Profiles
{
    public class MaroMapperProfile : Profile
    {
        public MaroMapperProfile()
        {
            CreateMap<Relationship, RelationshipReadDto>()
                    .ForMember(t => t.CharacterName, opt => opt.MapFrom(src => src.Direction == RelationshipDirection.FromRight ? src.RelatedCharacter.Name : src.Character.Name))
                    .ForMember(t => t.CharacterId, opt => opt.MapFrom(src => src.Direction == RelationshipDirection.FromRight ? src.RelatedCharacter.Id : src.Character.Id))
                    .ForMember(t => t.Relationship, opt => opt.MapFrom(src => src.ToText(src.Character)));
            CreateMap<Character, CharacterReadDto>();
            CreateMap<CharacterCreateDto, Character>();
        }
    }
}
