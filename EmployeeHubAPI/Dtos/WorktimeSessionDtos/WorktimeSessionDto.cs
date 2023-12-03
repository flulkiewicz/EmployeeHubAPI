namespace EmployeeHubAPI.Dtos.WorktimeSessionDtos
{
    public class WorktimeSessionDto
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? UserId { get; set; }
    }
}
