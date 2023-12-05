using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeHubAPI.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Employee? Supervisor { get; set; }
        public Guid? SupervisorId { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Department? Department { get; set; }
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        public List<WorktimeSession> WorktimeSessions { get; set; } = new List<WorktimeSession>();
    }

    public enum Department
    {
        Hr,
        IT,
        Sales,
        Vendor,
        Warehouse,
        Marketing,
        Production,
        Management,
        Administration,
    }

    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees", "bs");

            builder.HasOne(e => e.Supervisor)
                .WithMany(e => e.Employees);

            builder.HasOne(e => e.User)
                .WithOne(e => e.EmployeeAccount)
                .HasForeignKey<Employee>(e => e.UserId)
                .HasPrincipalKey<ApplicationUser>(e => e.Id);

            builder.HasMany(e => e.WorktimeSessions)
                .WithOne()
                .HasForeignKey(e => e.EmployeeId);

        }
    }
}
