namespace EmployeeHubAPI.Dtos.WorktimeSessionDtos
{
    public class WorktimeSessionAddDto : WorktimeSessionDto
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
