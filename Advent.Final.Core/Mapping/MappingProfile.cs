using Advent.Final.Entities.DTOs;
using Advent.Final.Entities.Entities;
using AutoMapper;

namespace Advent.Final.Core.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserCreateDto>();
        }
    }
}
