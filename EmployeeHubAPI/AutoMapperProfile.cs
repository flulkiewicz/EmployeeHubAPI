using AutoMapper;
using EmployeeHubAPI.Dtos;
using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region UserMaps
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserUpdateDto, ApplicationUser>();
            #endregion
        }
    }
}
