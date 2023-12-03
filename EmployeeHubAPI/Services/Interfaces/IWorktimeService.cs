using EmployeeHubAPI.Dtos.WorktimeSessionDtos;

namespace EmployeeHubAPI.Services.Interfaces
{
    public interface IWorktimeService
    {
        Task<WorktimeSessionResponse> HandleCurrentUserSessionState(string? userId = null);
        Task<object> GetMonthlyTime(string? userId = null, int? year = null, int? month = null);
        Task<List<WorktimeSessionDto>> GetUserSessions(string userId);
        Task<WorktimeSessionDto> UpdateSession(WorktimeSessionDto session);
    }
}