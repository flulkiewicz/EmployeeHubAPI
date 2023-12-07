namespace EmployeeHubAPI.Dtos.WorktimeSessionDtos
{
    public class WorktimeSessionUpdateDto : WorktimeSessionDto
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
