using Microsoft.AspNetCore.Identity;

namespace EmployeeHubAPI.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Employee? EmployeeAccount { get; set; }
        public Guid? EmployeeAccountId { get; set; }
    }
}
