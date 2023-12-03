using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI.Dtos.EmployeeDtos
{
    public class EmployeeUserDto
    {
        public Guid Id { get; set; }
        public Employee? Supervisor { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Department? Department { get; set; }
    }
}
