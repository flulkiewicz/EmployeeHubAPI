using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI.Dtos.EmployeeDtos
{
    public class EmployeeUpdateDto
    {
        public Employee? Supervisor { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Department? Department { get; set; }
    }
}
