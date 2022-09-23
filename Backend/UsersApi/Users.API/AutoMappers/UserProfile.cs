using AutoMapper;
using Users.API.Dto;
using Users.Entities.Entities.Concreate;

namespace Users.API.AutoMappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
