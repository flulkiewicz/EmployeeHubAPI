using EmployeeHubAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHubAPI.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .ToTable("Users");

            builder.Entity<IdentityUserToken<string>>()
                .ToTable("Tokens");

            builder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims");

            builder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins");

            builder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims");

            builder.Entity<IdentityRole>()
                .ToTable("Roles");

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles");

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorktimeSession> WorktimeSessions { get; set; }
       
    }
}
