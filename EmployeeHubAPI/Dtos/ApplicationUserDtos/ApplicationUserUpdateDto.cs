using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI.ApplicationUserDtos
{
    public class ApplicationUserUpdateDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string PhoneNumber { get; set; }
    }
}
