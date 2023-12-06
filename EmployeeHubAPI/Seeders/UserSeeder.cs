using EmployeeHubAPI.Data;
using EmployeeHubAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHubAPI.Seeders
{
    public class UserSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _context;
        public UserSeeder(UserManager<ApplicationUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task SeedUsers()
        {
            if (!await _userManager.Users.AnyAsync())
            {
                var user = new ApplicationUser { UserName = "admin@wsbk91.pl", Email = "admin@wsbk91.pl" };
                await _userManager.CreateAsync(user, "String123!");
                await AssignRole(user.UserName, "Admin");

                for (int i = 0; i < 3; i++)
                {
                    var supervisor = new ApplicationUser { UserName = $"supervisor{i}@fajnafirma.pl", Email = $"supervisor{i}@fajnafirma.pl" };
                    await _userManager.CreateAsync(supervisor, "String123!");
                    await AssignRole(supervisor.UserName, "Supervisor");
                    await CreateEmployeeAccount(supervisor.UserName);
                }

                for (int i = 0; i < 20; i++)
                {
                    var employee = new ApplicationUser { UserName = $"employee{i}@fajnafirma.pl", Email = $"employee{i}@fajnafirma.pl" };
                    await _userManager.CreateAsync(employee, "String123!");
                    await AssignRole(employee.UserName, "User");
                    await CreateEmployeeAccount(employee.UserName);
                }
            }
        }
        
        private ApplicationUser GetUserByUsername(string username) 
            => _userManager.Users.First(x => x.UserName == username);

        private async Task AssignRole(string username, string role)
        {
            var user = GetUserByUsername(username);
            await _userManager.AddToRoleAsync(user, role);
        }

        private async Task CreateEmployeeAccount(string username)
        {
            var user = GetUserByUsername(username);
            if(await _userManager.IsInRoleAsync(user, "User"))
            {
                var supervisorsUsers = await _userManager.GetUsersInRoleAsync("Supervisor");

                Random random = new Random();
                int index = random.Next(supervisorsUsers.Count);

                var randomSupervisorEmpAccount = supervisorsUsers[index].EmployeeAccount;

                user.EmployeeAccount = new Employee
                {
                    Supervisor =  randomSupervisorEmpAccount
                };

            }

            if(await _userManager.IsInRoleAsync(user, "Supervisor"))
                user.EmployeeAccount = new Employee();

            user.Active = true;

            await _context.SaveChangesAsync();
        }
    }
}
