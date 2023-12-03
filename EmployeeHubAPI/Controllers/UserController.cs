using EmployeeHubAPI.ApplicationUserDtos;
using EmployeeHubAPI.Data;
using EmployeeHubAPI.Dtos.ApplicationUserDtos;
using EmployeeHubAPI.Entities;
using EmployeeHubAPI.Services;
using EmployeeHubAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHubAPI.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<ApplicationUserDto>>> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUserDtos();

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDto>> GetUserAsync(string id)
        {
            var result = await _userService.GetUserDto(id);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserAsync(string id)
        {
            var result = await _userService.DeleteUser(id);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApplicationUserDto>> UpdateUserAsync(string id, ApplicationUserUpdateDto userDto)
        {
            var result = await _userService.UpdateUser(id, userDto);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("update")]
        public async Task<ActionResult<ApplicationUserDto>> SelfUpdateUserAsync(ApplicationUserUpdateDto userDto)
        {
            var result = await _userService.SelfUpdateUser(userDto);

            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("activate/{id}")]
        public async Task<ActionResult<ApplicationUserDto>> ActivateUser(string id, ApplicationUserActivationDto userDto)
        {
            var result = await _userService.UserActivation(id, userDto);

            return Ok(result);
        }
    }
}
