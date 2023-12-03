using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeHubAPI.Entities
{
    public class WorktimeSession
    {
        public Guid Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string? UserId { get; set; }
    }

    public class WorktimeSessionConfiguratin : IEntityTypeConfiguration<WorktimeSession>
    {
        public void Configure(EntityTypeBuilder<WorktimeSession> builder)
        {
            builder.ToTable("WorktimeSessions", "bs");
        }
    }

}
