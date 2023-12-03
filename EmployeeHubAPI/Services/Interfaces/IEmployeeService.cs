using EmployeeHubAPI.Dtos.EmployeeDtos;

namespace EmployeeHubAPI.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> AddEmployee(EmployeeRegisterDto employeeDto);
        Task<List<EmployeeDto>> GetAll();
        Task<EmployeeDto> UpdateEmployee(Guid id, EmployeeUpdateDto employeeDto);

    }
}