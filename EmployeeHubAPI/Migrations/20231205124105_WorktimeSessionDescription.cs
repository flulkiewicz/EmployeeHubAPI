using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeHubAPI.Migrations
{
    /// <inheritdoc />
    public partial class WorktimeSessionDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WorktimeSessions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WorktimeSessions");
        }
    }
}
