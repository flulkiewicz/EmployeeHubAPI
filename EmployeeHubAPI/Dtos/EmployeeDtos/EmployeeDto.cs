using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI.Dtos.EmployeeDtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public Employee? Supervisor { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Department? Department { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
    }
}
