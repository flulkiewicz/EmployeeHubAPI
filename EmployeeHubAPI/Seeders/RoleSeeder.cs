using Microsoft.AspNetCore.Identity;

namespace EmployeeHubAPI.Seeders
{
    public class RoleSeeder
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleSeeder(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task SeedRoles()
        {
            if (_roleManager.Roles.Any())
                return;

            await _roleManager.CreateAsync(new IdentityRole("User"));
            await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
            await _roleManager.CreateAsync(new IdentityRole("Admin"));
        }
    }
}
