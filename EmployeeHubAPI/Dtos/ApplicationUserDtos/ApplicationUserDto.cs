using EmployeeHubAPI.Dtos.EmployeeDtos;
using Microsoft.AspNetCore.Identity;

namespace EmployeeHubAPI.ApplicationUserDtos
{
    public class ApplicationUserDto
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid? EmployeeAccountId { get; set; }
        public EmployeeUserDto? EmployeeAccount { get; set; }
        public bool Active { get; set; }
        public List<string> Roles {  get; set; } = new List<string>();
    }
}
