using ETL_web_project.Data.Context;
using ETL_web_project.Data.Entities;
using ETL_web_project.DTOs;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

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

            // 1) Tüm joblar
            var jobs = await _context.EtlJobs
                .OrderBy(j => j.JobName)
                .ToListAsync();

            dto.TotalJobs = jobs.Count;
            dto.ActiveJobs = jobs.Count(j => j.IsActive);

            if (!jobs.Any())
                return dto;

            var jobIds = jobs.Select(j => j.JobId).ToList();

            // 2) Her job için son run
            var lastRuns = await _context.EtlRuns
                .Where(r => jobIds.Contains(r.JobId))
                .GroupBy(r => r.JobId)
                .Select(g => g.OrderByDescending(r => r.StartTime).FirstOrDefault())
                .ToListAsync();

            DateTime? closestNext = null;

            foreach (var job in jobs)
            {
                var lastRun = lastRuns.FirstOrDefault(r => r.JobId == job.JobId);

                var row = new EtlScheduleRowDto
                {
                    JobId = job.JobId,
                    JobName = job.JobName,
                    JobCode = job.JobCode,
                    FrequencyText = string.IsNullOrWhiteSpace(job.Description)
                        ? "Not specified"
                        : job.Description,
                    IsActive = job.IsActive,
                    LastRunTime = lastRun?.StartTime,
                    LastRunStatus = lastRun?.Status
                };

                // Demo: NextRun = (LastRun.EndTime veya StartTime) + 1 saat
                if (job.IsActive && lastRun != null)
                {
                    var baseTime = lastRun.EndTime ?? lastRun.StartTime;
                    var next = baseTime.AddHours(1);

                    row.NextRunTime = next;

                    if (!closestNext.HasValue || next < closestNext.Value)
                        closestNext = next;
                }

                if (lastRun != null && lastRun.Status == EtlStatus.Failed)
                    dto.FailedLastRuns++;

                dto.Rows.Add(row);
            }

            dto.ClosestNextRun = closestNext;

            return dto;
        }
    }
}
