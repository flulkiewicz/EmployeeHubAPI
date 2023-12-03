using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI.Dtos.EmployeeDtos
{
    public class EmployeeRegisterDto
    {
        public Guid? SupervisorId { get; set; }
        public Department? Department { get; set; }
        public string? UserId { get; set; }
    }
}
