using API.Dtos;
using API.Model;
using AutoMapper;

namespace API.Mapper.Profiles
{
    public class MaroMapperProfile : Profile
    {
        public MaroMapperProfile()
        {
            CreateMap<Character, CharacterReadDto>();
        }
    }
}
