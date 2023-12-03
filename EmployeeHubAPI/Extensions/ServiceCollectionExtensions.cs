using EmployeeHubAPI.Data;
using EmployeeHubAPI.Seeders;
using EmployeeHubAPI.Services;
using EmployeeHubAPI.Services.Interfaces;

namespace EmployeeHubAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IWorktimeService, WorktimeService>();    

            //Seeders
            services.AddScoped<Seeder>();
            services.AddScoped<UserSeeder>();
            services.AddScoped<RoleSeeder>();
            services.AddScoped<WorktimeSeeder>();
        }
    }
}
