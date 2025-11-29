using ETL_web_project.Data.Context;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;
using ETL_web_project.DTOs.Etl.Jobs;
using ETL_web_project.DTOs.Etl.Schedule;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ETL_web_project.Services
{
    public class EtlScheduleOverviewService : IEtlScheduleOverviewService
    {
        private readonly ProjectContext _context;

        public EtlScheduleOverviewService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<EtlScheduleOverviewDto> GetOverviewAsync()
        {
            var dto = new EtlScheduleOverviewDto();

            var schedules = await _context.EtlSchedules
                .Include(s => s.Job)
                .ToListAsync();

            dto.TotalJobs = schedules.Count;
            dto.ActiveJobs = schedules.Count(s => s.IsActive);

            var jobIds = schedules.Select(s => s.JobId).ToList();

            var lastRuns = await _context.EtlRuns
                .Where(r => jobIds.Contains(r.JobId))
                .GroupBy(r => r.JobId)
                .Select(g => g.OrderByDescending(r => r.StartTime).FirstOrDefault())
                .ToListAsync();

            DateTime? closestNext = null;

            dto.Rows = schedules.Select(s =>
            {
                var lastRun = lastRuns.FirstOrDefault(r => r.JobId == s.JobId);

                DateTime? nextRun = null;
                if (s.IsActive)
                {
                    nextRun = CalculateNextRun(s.FrequencyText, lastRun);
                }

                if (nextRun != null)
                {
                    if (!closestNext.HasValue || nextRun < closestNext.Value)
                        closestNext = nextRun;
                }

                return new EtlScheduleRowDto
                {
                    JobId = s.JobId,
                    ScheduleId = s.ScheduleId,
                    JobName = s.Job?.JobName ?? "",
                    JobCode = s.Job?.JobCode ?? "",
                    FrequencyText = s.FrequencyText,
                    IsActive = s.IsActive,
                    LastRunTime = lastRun?.StartTime,
                    LastRunStatus = lastRun?.Status,
                    NextRunTime = nextRun
                };
            }).ToList();

            dto.ClosestNextRun = closestNext;

            // Available Jobs
            var jobs = await _context.EtlJobs
                .OrderBy(j => j.JobName)
                .ToListAsync();

            dto.AvailableJobs = jobs.Select(j =>
            {
                var lastRun = lastRuns.FirstOrDefault(r => r.JobId == j.JobId);

                return new EtlJobListItemDto
                {
                    JobId = j.JobId,
                    JobName = j.JobName,
                    JobCode = j.JobCode,
                    Description = j.Description,
                    IsActive = j.IsActive,
                    LastRunId = lastRun?.RunId,
                    LastStatus = lastRun?.Status,
                    LastStartTime = lastRun?.StartTime,
                    LastEndTime = lastRun?.EndTime,
                    LastRowsRead = lastRun?.RowsRead,
                    LastRowsInserted = lastRun?.RowsInserted,
                    LastRowsUpdated = lastRun?.RowsUpdated
                };
            }).ToList();

            return dto;
        }

        // ----------------------------------------------------------------------
        // FREQUENCY PARSING – Burada gerçek scheduler mantığı çalışır
        // ----------------------------------------------------------------------
        private DateTime? CalculateNextRun(string frequency, EtlRun lastRun)
        {
            if (string.IsNullOrWhiteSpace(frequency))
                return null;

            var now = DateTime.Now;

            // Normalize (noktayı iki nokta yap, case insensitive, double spaces temizle)
            frequency = frequency
                .Trim()
                .Replace(".", ":")
                .Replace("  ", " ")
                .ToLower();

            // WEEKLY -------------------------------------------
            if (frequency.StartsWith("weekly"))
            {
                // weekly on monday 05:30
                // weekly monday 05:30
                // weekly monday 5:30
                // weekly on monday 5.30
                var tokens = frequency.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                // weekly + (on) + Monday + HH:mm
                // tokens: [weekly, on, monday, 05:30]
                // OR:     [weekly, monday, 05:30]

                string dayText = "";
                string timeText = "";

                if (tokens.Length == 4)
                {
                    dayText = tokens[2];  // monday
                    timeText = tokens[3]; // 05:30
                }
                else if (tokens.Length == 3)
                {
                    dayText = tokens[1];  // monday
                    timeText = tokens[2]; // 05:30
                }
                else return null;

                if (!Enum.TryParse<DayOfWeek>(dayText, true, out var day))
                    return null;

                if (!TimeSpan.TryParse(timeText, out var timeSpan))
                    return null;

                return GetNextWeekdayTime(now, day, timeSpan);
            }

            // DAILY -------------------------------------------
            if (frequency.StartsWith("daily"))
            {
                // daily at 03:00
                var parts = frequency.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var timeText = parts.Last(); // her zaman son parça saat olur

                if (!TimeSpan.TryParse(timeText, out var timeSpan))
                    return null;

                var today = now.Date.Add(timeSpan);
                return (today > now) ? today : today.AddDays(1);
            }

            // EVERY HOUR --------------------------------------
            if (frequency.Contains("hour"))
            {
                return now.AddHours(1);
            }

            // EVERY X MINUTES ---------------------------------
            if (frequency.StartsWith("every") && frequency.Contains("minute"))
            {
                var number = frequency.Split(' ')
                    .FirstOrDefault(x => int.TryParse(x, out _));

                return number != null ? now.AddMinutes(int.Parse(number)) : null;
            }

            return null;
        }

        private DateTime GetNextWeekdayTime(DateTime now, DayOfWeek day, TimeSpan time)
        {
            int daysToAdd = ((int)day - (int)now.DayOfWeek + 7) % 7;

            var next = now.Date
                .AddDays(daysToAdd)
                .Add(time);

            if (next <= now)
                next = next.AddDays(7);

            return next;
        }

    }
}
