using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeHubAPI.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public Employee? Supervisor { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Department? Department { get; set; }
        public ApplicationUser? User { get; set; }
        public int UserId { get; set; }
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
                .HasPrincipalKey<ApplicationUser>(e => e.EmployeeAccountId);

        }
    }
}
