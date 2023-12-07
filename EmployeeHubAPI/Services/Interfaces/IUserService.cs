using EmployeeHubAPI.ApplicationUserDtos;
using EmployeeHubAPI.Dtos.ApplicationUserDtos;

namespace EmployeeHubAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<ApplicationUserDto>> DeleteUser(string id);
        Task<List<ApplicationUserDto>> GetAllUserDtos();
        Task<ApplicationUserDto> GetUserDto(string? id = null);
        Task<ApplicationUserDto> UpdateUser(string id, ApplicationUserUpdateDto userDto);
        Task<ApplicationUserDto> SelfUpdateUser(ApplicationUserUpdateDto userDto);
        Task<ApplicationUserDto> UserActivation(string id, ApplicationUserActivationDto userDto);
        Task<List<ApplicationUserSupervisorDto>> GetSupervisors();
    }
}