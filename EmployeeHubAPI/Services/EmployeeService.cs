using AutoMapper;
using EmployeeHubAPI.Data;
using EmployeeHubAPI.Dtos.EmployeeDtos;
using EmployeeHubAPI.Entities;
using EmployeeHubAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EmployeeHubAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IMapper _mapper;

        public EmployeeService(DataContext context, IHttpContextAccessor httpContext, IMapper mapper)
        {
            _context = context;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> AddEmployee(EmployeeRegisterDto employeeDto)
        {
            if (employeeDto.UserId.IsNullOrEmpty())
                employeeDto.UserId = GetCurrentUserId();

            var employee = _mapper.Map<Employee>(employeeDto);
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            var dto =  _mapper.Map<EmployeeDto>(employee);

            return dto;
        }

        public async Task<EmployeeDto> UpdateEmployee(Guid id, EmployeeUpdateDto employeeDto)
        {
            var employee = await GetEmployeeById(id);
            _mapper.Map(employeeDto, employee);

            await _context.SaveChangesAsync();

            var dto = _mapper.Map<EmployeeDto>(employee);

            return dto;
        }

        public async Task<List<EmployeeDto>> GetAll() => await GetAllEmployeesDtos();

        private async Task<Employee> GetEmployeeById(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee is null) throw new NotFoundException("Employee not found");

            return employee;
        }
        private async Task<List<EmployeeDto>> GetAllEmployeesDtos()
        {
            var employees = await GetAllEmployees();
            var dtos = employees
                .Select(x => _mapper.Map<EmployeeDto>(x))
                .ToList();

            return dtos;
        }
        private async Task<List<Employee>> GetAllEmployees() => await _context.Employees.ToListAsync();
        private string GetCurrentUserId()
        {
            var currentUserId = _httpContext.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId is null || currentUserId == string.Empty)
                throw new NotFoundException("There is no claim for current User Id");

            return currentUserId;

        }
    }
}
