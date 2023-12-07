using AutoMapper;
using EmployeeHubAPI.ApplicationUserDtos;
using EmployeeHubAPI.Dtos.ApplicationUserDtos;
using EmployeeHubAPI.Dtos.EmployeeDtos;
using EmployeeHubAPI.Dtos.WorktimeSessionDtos;
using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserUpdateDto, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserSupervisorDto>();
            #endregion

            #region Employee
            CreateMap<EmployeeRegisterDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, EmployeeUserDto>();
            #endregion

            #region WortimeSession
            CreateMap<WorktimeSession, WorktimeSessionDto>();
            CreateMap<WorktimeSessionDto, WorktimeSession>();
            CreateMap<WorktimeSessionAdminDto, WorktimeSession>();
            CreateMap<WorktimeSession, WorktimeSessionAdminDto>();
            CreateMap<WorktimeSessionManualUpdateDto, WorktimeSession>();
            CreateMap<WorktimeSessionAddDto, WorktimeSession>();
            #endregion
        }
    }
}
