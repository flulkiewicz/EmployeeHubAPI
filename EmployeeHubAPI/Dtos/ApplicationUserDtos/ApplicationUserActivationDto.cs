namespace EmployeeHubAPI.Dtos.ApplicationUserDtos
{
    public class ApplicationUserActivationDto
    {
        public required string Role {  get; set; }
        public Guid? SupervisorId { get; set; }
    }
}
