using EmployeeHubAPI.Dtos.WorktimeSessionDtos;

namespace EmployeeHubAPI.Services.Interfaces
{
    public interface IWorktimeService
    {
        Task<WorktimeSessionResponse> HandleCurrentUserSessionState(WorktimeSessionDto sessionDto, string? userId = null);
        Task<object> GetMonthlyTime(string? userId = null, int? year = null, int? month = null);
        Task<List<WorktimeSessionAdminDto>> GetUserSessions(string userId);
        Task<WorktimeSessionDto> UpdateSession(WorktimeSessionAdminDto session);
        Task DeleteSession(Guid sessionId);
    }
}