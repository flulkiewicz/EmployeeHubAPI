﻿namespace EmployeeHubAPI.Dtos.WorktimeSessionDtos
{
    public class WorktimeSessionAdminDto : WorktimeSessionDto
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public Guid? EmployeeId { get; set; }
    }
}
