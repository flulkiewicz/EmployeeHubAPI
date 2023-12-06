using AutoMapper;
using EmployeeHubAPI.ApplicationUserDtos;
using EmployeeHubAPI.Data;
using EmployeeHubAPI.Dtos.ApplicationUserDtos;
using EmployeeHubAPI.Entities;
using EmployeeHubAPI.Exceptions;
using EmployeeHubAPI.Exceptions.User;
using EmployeeHubAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EmployeeHubAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(DataContext context, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public async Task<List<ApplicationUserDto>> GetAllUserDtos()
        {
            var users = await GetAllUsers();

            var dtos = users
                .Select(x => _mapper.Map<ApplicationUserDto>(x))
                .ToList();

            foreach (var x in users)
            {
                var roles = await _userManager.GetRolesAsync(x);
                var dto = dtos.First(e => e.Id == x.Id);
                dto.Roles = roles.ToList();
            }

            return dtos;
        }

        public async Task<ApplicationUserDto> GetUserDto(string? id = null)
        {
            if (id == null)
                id = GetCurrentUserId();

            var user = await GetUserById(id);
            var dto = _mapper.Map<ApplicationUserDto>(user);

            var roles = await _userManager.GetRolesAsync(user);

            dto.Roles = roles.ToList();

            return dto;
        }

        public async Task<ApplicationUserDto> UpdateUser(string id, ApplicationUserUpdateDto userDto)
        {
            var user = await GetUserById(id);

            _mapper.Map(userDto, user);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<ApplicationUserDto>(user);

            return dto;
        }

        public async Task<List<ApplicationUserDto>> DeleteUser(string id)
        {
            var user = await GetUserById(id);

            _context.Users.Remove(user);
            _context.SaveChanges();

            var users = await GetAllUsers();
            var dtos = users.Select(x => _mapper.Map<ApplicationUserDto>(x))
                .ToList();

            return dtos;
        }

        public async Task<ApplicationUserDto> SelfUpdateUser(ApplicationUserUpdateDto userDto)
        {
            var userId = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null) throw new NotFoundException("User id not found");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            _mapper.Map(userDto, user);
            await _context.SaveChangesAsync();

            var result = _mapper.Map<ApplicationUserDto>(user);

            return result;
        }

        public async Task<ApplicationUserDto> UserActivation(string id, ApplicationUserActivationDto userDto)
        {
            var user = await GetUserById(id);
            if (user.Active) throw new UserIsActiveException();

            await _userManager.AddToRoleAsync(user, userDto.Role);
            user.EmployeeAccount = new Employee();

            if(userDto.SupervisorId != null)
            {
                var supervisor = await _context.Users
                    .Include(x => x.EmployeeAccount)
                    .FirstOrDefaultAsync(x => x.Id ==  userDto.SupervisorId.ToString());

                user.EmployeeAccount.Supervisor = supervisor!.EmployeeAccount ?? throw new NotFoundException("Supervisor not found");
            }

            user.Active = true;
            await _context.SaveChangesAsync();

            var result = _mapper.Map<ApplicationUserDto>(user);
            return result;
        }


        private async Task<ApplicationUser> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
                id = GetCurrentUserId();

            var user = await _context.Users
                .Include(x => x.EmployeeAccount)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user is null) throw new NotFoundException("User not found");

            return user;
        }

       

        private async Task<List<ApplicationUser>> GetAllUsers()
        {
            var userId = GetCurrentUserId();
            var user = await GetUserById(userId);
            List<ApplicationUser> users = new();

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                users = await _userManager.Users
                    .Include(e => e.EmployeeAccount)
                    .ToListAsync();

            } else if(await _userManager.IsInRoleAsync(user, "Supervisor"))
            {
                users = await _userManager.Users
                    .Include(e => e.EmployeeAccount)
                    .Where(e => e.EmployeeAccount!.SupervisorId == user.EmployeeAccount!.Id)
                    .ToListAsync();
            }

            return users;
        }

        private string GetCurrentUserId()
        {
            var currentUserId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserId is null || currentUserId == string.Empty)
                throw new NotFoundException("There is no claim for current User Id");

            return currentUserId;

        }
    }
}
