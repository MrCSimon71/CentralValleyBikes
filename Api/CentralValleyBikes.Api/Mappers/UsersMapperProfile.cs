using AutoMapper;
using CentralValleyBikes.Domain.Entities;
using CentralValleyBikes.Api.DTOs;

namespace CentralValleyBikes.Api.Mappers
{
    public class UsersMapperProfile : Profile
    {
        public UsersMapperProfile()
        {
            CreateMap<User, UserDto>()
                .AfterMap((src, dest) => dest.Password = null);

            CreateMap<UserRegistrationDto, User>();
            CreateMap<UserDto, User>();
        }
    }
}
