using EmployeeHubAPI.Dtos.WorktimeSessionDtos;
using EmployeeHubAPI.Services;
using EmployeeHubAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHubAPI.Controllers
{
    //[Authorize(Policy = "Users")]
    [ApiController]
    [Route("worktime-session")]
    public class WorktimeController : ControllerBase
    {
        private readonly IWorktimeService _worktimeService;

        public WorktimeController(IWorktimeService worktimeService)
        {
            _worktimeService = worktimeService;
        }

        //Self handle session for currently logged employee
        [HttpPost]
        public async Task<ActionResult<WorktimeSessionResponse>> HandleSession(WorktimeSessionDto sessionDto)
        {
            var result = await _worktimeService.HandleCurrentUserSessionState(sessionDto);

            return Ok(result);
        }

        //Handle session for user - provide employee Id
        [HttpPost("{id}")]
        public async Task<ActionResult<WorktimeSessionResponse>> HandleSessionManually(string id, WorktimeSessionManualUpdateDto sessionDto)
        {
            var result = await _worktimeService.HandleCurrentUserSessionState(sessionDto, id);

            return Ok(result);
        }


        [HttpGet("montly-time/{userId}")]
        public async Task<ActionResult<List<WorktimeSessionDto>>> GetSummaryTimeForCurrentMonth(string userId)
        {
            var result = await _worktimeService.GetMonthlyTime(userId);

            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult<WorktimeSessionDto>> UpdateSession(WorktimeSessionAdminDto worktimeSessionDto)
        {
            var result = await _worktimeService.UpdateSession(worktimeSessionDto);

            return Ok(result);
        }

        [HttpDelete("{sessionId}")]
        public async Task<ActionResult<WorktimeSessionDto>> DeleteSession(Guid sessionId)
        {
            await _worktimeService.DeleteSession(sessionId);

            return Ok();
        }


        [HttpGet("user-sessions/{userId}")]
        public async Task<ActionResult<List<WorktimeSessionAdminDto>>> UserSessions(string userId)
        {
            var result = await _worktimeService.GetUserSessions(userId);

            return Ok(result);
        }
    }
}
