using EmployeeHubAPI.Data;
using EmployeeHubAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHubAPI.Seeders
{
    public class WorktimeSeeder
    {
        private readonly DataContext _dataContext;
        private Random _random = new Random();

        public WorktimeSeeder(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedWorktimeSessions()
        {
            var employees =  _dataContext.Employees;

            await employees.ForEachAsync(e => { 
                e.WorktimeSessions = CreateWorktimeSessions(_random.Next(10, 30)); 
            });

            await _dataContext.SaveChangesAsync();
        }


        private List<WorktimeSession> CreateWorktimeSessions(int numberOfSessions)
        {
            var sessions = new List<WorktimeSession>();

            while (sessions.Count < numberOfSessions)
            {
                var start = GenerateRandomDateTime();
                var end = start.AddHours(_random.Next(8, 11));

                if (!IsOverlapping(sessions, start, end))
                {
                    sessions.Add(new WorktimeSession { Start = start, End = end });
                }
            }

            return sessions;
        }

        private bool IsOverlapping(List<WorktimeSession> sessions, DateTime start, DateTime end)
        {
            foreach (var session in sessions)
            {
                if (start < session.End && end > session.Start)
                {
                    return true;
                }
            }
            return false;
        }

        private DateTime GenerateRandomDateTime()
        {
            var currentDate = DateTime.Now;
            var currentMonth = currentDate.Month;
            var year = currentDate.Year;

            var month = _random.Next(2) == 0 ? currentMonth : (currentMonth == 1 ? 12 : currentMonth - 1);

            int maxDay;
            if (month == currentMonth)
            {
                maxDay = currentDate.Day - 1; 
            }
            else
            {
                maxDay = DateTime.DaysInMonth(year, month); 
            }

            if (maxDay < 1)
            {
                month = month == 1 ? 12 : month - 1;
                maxDay = DateTime.DaysInMonth(year, month);
                year = month == 12 ? year - 1 : year;
            }

            var day = _random.Next(1, maxDay + 1);
            var hour = _random.Next(24);

            return new DateTime(year, month, day, hour, 0, 0);
        }

    }
}
