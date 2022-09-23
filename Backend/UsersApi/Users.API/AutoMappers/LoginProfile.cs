using AutoMapper;
using Users.API.Dto;
using Users.Entities.Entities.Concreate;

namespace Users.API.AutoMappers
{
    public class LoginProfile:Profile
    {
        public LoginProfile()
        {
            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();
        }
    }
}
