using Microsoft.AspNetCore.Identity;

namespace EmployeeHubAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Employee? EmployeeAccount { get; set; }
        public bool Active { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
