using EmployeeHubAPI.Dtos.WorktimeSessionDtos;
using EmployeeHubAPI.Entities;

namespace EmployeeHubAPI.Services.Interfaces
{
    public interface IWorktimeService
    {
        Task<WorktimeSessionResponse> HandleCurrentUserSessionState(WorktimeSessionDto sessionDto, string? userId = null);
        Task<object> GetMonthlyTime(string? userId = null, int? year = null, int? month = null);
        Task<List<WorktimeSessionAdminDto>> GetUserSessions(string userId);
        Task<WorktimeSessionDto> UpdateSession(WorktimeSessionUpdateDto session);
        Task DeleteSession(Guid sessionId);
        Task<WorktimeSession> AddSession(string userId, WorktimeSessionAddDto sessionDto);
    }
}