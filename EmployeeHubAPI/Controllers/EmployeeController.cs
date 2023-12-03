using EmployeeHubAPI.Dtos.EmployeeDtos;
using EmployeeHubAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EmployeeHubAPI.Controllers
{
    [ApiController]
    [Authorize(Roles = "Admin, Supervisor")]
    [Route("employees")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> Add(EmployeeRegisterDto employeeDto)
        {
            var result = await _employeeService.AddEmployee(employeeDto);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<EmployeeDto>>> GetAll()
        {
            var result = await _employeeService.GetAll();

            return Ok(result);
        }

    }
}
