using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI.Dtos
{
    public class ApplicationUserDto
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public Guid? EmployeeAccountId { get; set; }
        public Employee? EmployeeAccount { get; set; }
    }
}
