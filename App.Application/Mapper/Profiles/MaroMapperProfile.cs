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
                    .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                    .ForMember(dest => dest.RelatedTo,
                    opt => opt
                    .MapFrom(dest => dest.RelatedTo
                                    .Select(r => new RelationshipReadDto { CharacterId = r.CharacterId, CharacterName = r.Character.Name, Relationship = r.RelativeRelationshipName })))
                    .ForMember(c => c.Relationships,
                    opt => opt
                    .MapFrom(dest => dest.Relationships
                                    .Select(r => new RelationshipReadDto { CharacterId = r.RelatedCharacterId, CharacterName = r.RelatedCharacter.Name, Relationship = r.OppositeRelativeRelationshipName })))
                    .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.Owner.UserName));
            CreateMap<CharacterCreateDto, Character>()
                    .ForMember(g => g.Gender, opt => opt.MapFrom(g => (Gender)Enum.Parse(typeof(Gender), g.Gender.FirstLetterToUpper())));
            CreateMap<BattleResult, BattleResultReadDto>().ForMember(b => b.Winner, opt => opt.MapFrom(src => src.Winner.Name));
        }
    }
}
