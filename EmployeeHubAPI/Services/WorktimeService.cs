using AutoMapper;
using EmployeeHubAPI.Data;
using EmployeeHubAPI.Dtos.WorktimeSessionDtos;
using EmployeeHubAPI.Entities;
using EmployeeHubAPI.Exceptions;
using EmployeeHubAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeHubAPI.Services
{
    public class WorktimeService : IWorktimeService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public WorktimeService(DataContext dataContext, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _context = dataContext;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<WorktimeSessionResponse> HandleCurrentUserSessionState(WorktimeSessionDto sessionDto, string? userId)
        {
            var response = new WorktimeSessionResponse();

            userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await GetUserById(userId);

            var worktimeSessions = user.EmployeeAccount?.WorktimeSessions;

            var lastSession = worktimeSessions?.FirstOrDefault(x => x.End == null);

            if (lastSession is null)
            {
                var newSession = new WorktimeSession { Start = DateTime.UtcNow };
                worktimeSessions?.Add(newSession);

                response.State = "Session started";
                response.SessionInfo = _mapper.Map<WorktimeSessionDto>(newSession);
            }
            else
            {
                lastSession.End = DateTime.UtcNow;
                lastSession.Description = sessionDto.Description;

                response.State = "Session finished";
                response.SessionInfo = _mapper.Map<WorktimeSessionDto>(lastSession);
            }

            await _context.SaveChangesAsync();

            return response;
        }

        public async Task DeleteSession(Guid sessionId)
        {
            var session = _context.WorktimeSessions.FirstOrDefault(x => x.Id == sessionId);

            if (session is null) throw new NotFoundException("Session with given id not found");

            _context.WorktimeSessions.Remove(session);
            await _context.SaveChangesAsync();
        }

        public async Task<WorktimeSessionDto> UpdateSession(WorktimeSessionAdminDto sessionDto)
        {
            var session = await _context.WorktimeSessions.FirstOrDefaultAsync(x => x.Id == sessionDto.Id);
            
            if (session is null)
                throw new NotFoundException("Session not found");

            _mapper.Map(sessionDto, session);
            await _context.SaveChangesAsync();

            return sessionDto;
        }

        public async Task<WorktimeSession> AddSession(string userId, WorktimeSessionAddDto sessionDto)
        {
            var user = await GetUserById(userId);
            var currentUser = _userManager.Users.First(x => x.Id == GetCurrentUserId());

            if(await _userManager.IsInRoleAsync(currentUser, "Supervisor"))
                if (user.EmployeeAccount!.SupervisorId != currentUser.EmployeeAccount!.Id)
                    throw new Exception("You are not authorized to update this user");

            var session = _mapper.Map<WorktimeSession>(sessionDto);
            session.EmployeeId = user.EmployeeAccount!.Id;
            _context.WorktimeSessions.Add(session);

            await _context.SaveChangesAsync();

            return session;
        }


        public async Task<List<WorktimeSessionAdminDto>> GetUserSessions(string userId)
        {
            var user = await GetUserById(userId);
            var sessions = user.EmployeeAccount?.WorktimeSessions.OrderByDescending(x => x.Start);

            var sessionsDto = _mapper.Map<List<WorktimeSessionAdminDto>>(sessions);

            return sessionsDto;
        }


        public async Task<object> GetMonthlyTime(string? userId = null, int? year = null, int? month = null)
        {
            if (userId is null)
                userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var user = await GetUserById(userId);
            DateTime currentDate = DateTime.Now;
            
            if(year is null)
                year = currentDate.Year;

            if(month is null) 
                month = currentDate.Month;

            var employee = await _context.Employees.FirstAsync(x => x.UserId == userId);
            var sessions = employee?.WorktimeSessions
                .Where(x => x.Start.Month == month && x.Start.Year == year && x.End is not null)
                .ToList();

            var responseMessage = string.Empty;
            TimeSpan responseResult = TimeSpan.Zero;


            if (sessions is null || !sessions.Any())
                responseMessage = "No sessions for employee";
            else
            {
                responseResult = CalculateTotalWorktime(sessions);
                responseMessage = $"Summary time for monthly sessions: {responseResult.Hours:00}h {responseResult.Minutes:00}min";
            }



            return new { Info = responseMessage, Result = responseResult };
        }

        private async Task<ApplicationUser> GetUserById(string? userId = null)
        {
            var user = await _userManager.Users
                .Include(e => e.EmployeeAccount)
                .ThenInclude(e => e.WorktimeSessions)
                .FirstOrDefaultAsync(x => x.Id == userId) ?? throw new Exception("User for handling session not found");
            if (user is null)
                throw new NotFoundException("User not found");
            if (user.EmployeeAccount is null)
                throw new Exception("User not activated");

            return user;
        }


        private TimeSpan CalculateTotalWorktime(List<WorktimeSession> sessions)
        {
            TimeSpan totalWorktime = TimeSpan.Zero;

            foreach (var session in sessions)
            {
                if (session.End.HasValue)
                {
                    TimeSpan duration = session.End.Value - session.Start;
                    totalWorktime += duration;
                }
            }

            return totalWorktime;
        }

        private string GetCurrentUserId()
        {
            var currentUserId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId is null || currentUserId == string.Empty)
                throw new NotFoundException("There is no claim for current User Id");

            return currentUserId;

        }

    }

    public class WorktimeSessionResponse
    {
        public string? State { get; set; }
        public WorktimeSessionDto? SessionInfo { get; set; }
    }
}
