using AutoMapper;
using Users.API.Dto;
using Users.Entities.Entities.Concreate;

namespace Users.API.AutoMappers
{
    public class DepartmentProfile: Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentDto, Department>();
        }
    }
}
