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
                .ToTable("Roles")
                .HasData(
                    new IdentityRole { Name = "Admin", NormalizedName = "admin"},
                    new IdentityRole { Name = "Supervisor", NormalizedName = "supervisor" },
                    new IdentityRole { Name = "User", NormalizedName = "user"}
                ); ;

            builder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles");

        }

       
    }
}
