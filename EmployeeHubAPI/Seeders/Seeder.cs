namespace EmployeeHubAPI.Seeders
{
    public class Seeder
    {
        private readonly RoleSeeder _roleSeeder;
        private readonly UserSeeder _userSeeder;
        private readonly WorktimeSeeder _worktimeSeeder;

        public Seeder(RoleSeeder roleSeeder, UserSeeder userSeeder, WorktimeSeeder worktimeSeeder)
        {
            _roleSeeder = roleSeeder;
            _userSeeder = userSeeder;
            _worktimeSeeder = worktimeSeeder;
        }

        public async Task Seed()
        {
            await _roleSeeder.SeedRoles();
            await _userSeeder.SeedUsers();
            await _worktimeSeeder.SeedWorktimeSessions();
        }
    }
}
