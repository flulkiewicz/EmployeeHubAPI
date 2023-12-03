using EmployeeHubAPI.Dtos.WorktimeSessionDtos;
using EmployeeHubAPI.Services;
using EmployeeHubAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeHubAPI.Controllers
{
    [Authorize(Policy = "Users")]
    [ApiController]
    [Route("worktime-session")]
    public class WorktimeController : ControllerBase
    {
        private readonly IWorktimeService _worktimeService;

        public WorktimeController(IWorktimeService worktimeService)
        {
            _worktimeService = worktimeService;
        }

        //Self handle session for currently logged user
        [HttpGet]
        public async Task<ActionResult<WorktimeSessionResponse>> HandleSession()
        {
            var result = await _worktimeService.HandleCurrentUserSessionState();

            return Ok(result);
        }

        //Handle session for user - provide user Id
        [HttpGet("{id}")]
        public async Task<ActionResult<WorktimeSessionResponse>> HandleSessionManually(string id)
        {
            var result = await _worktimeService.HandleCurrentUserSessionState(id);

            return Ok(result);
        }

        [HttpGet("montly/{userId}")]
        public async Task<ActionResult<List<WorktimeSessionDto>>> GetSummaryTimeForCurrentMonth(string userId)
        {
            var result = await _worktimeService.GetMonthlyTime(userId);

            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<ActionResult<WorktimeSessionDto>> UpdateSession(WorktimeSessionDto worktimeSessionDto)
        {
            var result = await _worktimeService.UpdateSession(worktimeSessionDto);

            return Ok(result);
        }
    }
}
