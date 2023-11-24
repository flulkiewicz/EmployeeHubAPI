using EmployeeHubAPI.Data;
using EmployeeHubAPI.Dtos;
using EmployeeHubAPI.Entities;
using EmployeeHubAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHubAPI.Controllers
{
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<ApplicationUserDto>> GetAllUsers()
        {
            var result = _userService.GetAllUserDtos();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<ApplicationUserDto> GetUser(string id)
        {
            var result = _userService.GetUserDto(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
        {
            var result = _userService.DeleteUser(id);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult<ApplicationUserDto> UpdateUser(string id, ApplicationUserUpdateDto userDto)
        {
            var result = _userService.UpdateUser(id, userDto);

            return Ok(result);
        }
    }
}
