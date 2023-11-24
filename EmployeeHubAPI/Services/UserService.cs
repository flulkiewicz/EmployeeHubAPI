using AutoMapper;
using EmployeeHubAPI.Data;
using EmployeeHubAPI.Dtos;
using EmployeeHubAPI.Entities;
using EmployeeHubAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHubAPI.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ApplicationUserDto> GetAllUserDtos()
        {
            var dtos = GetAllUsers()
                .Select(x => _mapper.Map<ApplicationUserDto>(x))
                .ToList();

            return dtos;
        }

        public ApplicationUserDto GetUserDto(string id)
        {
            var dto = _mapper.Map<ApplicationUserDto>(GetUserById(id));

            return dto;
        }

        public ApplicationUserDto UpdateUser(string id, ApplicationUserUpdateDto userDto)
        {
            var user = GetUserById(id);

            _mapper.Map(userDto, user);
            _context.SaveChanges();

            var dto = _mapper.Map<ApplicationUserDto>(user);

            return dto;
        }

        public List<ApplicationUserDto> DeleteUser(string id)
        {
            var user = GetUserById(id);

            _context.Users.Remove(user);
            _context.SaveChanges();

            var dtos = GetAllUsers()
                .Select(x => _mapper.Map<ApplicationUserDto>(x))
                .ToList();

            return dtos;
        }
        

        private ApplicationUser GetUserById(string id)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null) throw new NotFoundException("User not found");

            return user;
        }

        private List<ApplicationUser> GetAllUsers() => _context.Users.ToList();



    }
}
