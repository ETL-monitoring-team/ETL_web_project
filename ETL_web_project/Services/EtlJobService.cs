using ETL_web_project.Data.Context;
using ETL_web_project.DTOs;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class EtlJobService : IEtlJobService
    {
        private readonly ProjectContext _context;

        public EtlJobService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<List<EtlJobListItemDto>> GetJobsAsync(string? searchText)
        {
            // 1) Job query (arama filtresi)
            var jobsQuery = _context.EtlJobs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var term = searchText.Trim();

                jobsQuery = jobsQuery.Where(j =>
                    j.JobName.Contains(term) ||
                    j.JobCode.Contains(term) ||
                    (j.Description != null && j.Description.Contains(term)));
            }

            jobsQuery = jobsQuery.OrderBy(j => j.JobName);

            var jobs = await jobsQuery.ToListAsync();

            // 2) Her job için son run bilgisini çek
            var result = new List<EtlJobListItemDto>();

            foreach (var job in jobs)
            {
                var lastRun = await _context.EtlRuns
                    .Where(r => r.JobId == job.JobId)
                    .OrderByDescending(r => r.StartTime)
                    .FirstOrDefaultAsync();

                var dto = new EtlJobListItemDto
                {
                    JobId = job.JobId,
                    JobName = job.JobName,
                    JobCode = job.JobCode,
                    Description = job.Description,
                    IsActive = job.IsActive
                };

                if (lastRun != null)
                {
                    dto.LastRunId = lastRun.RunId;
                    dto.LastStatus = lastRun.Status;
                    dto.LastStartTime = lastRun.StartTime;
                    dto.LastEndTime = lastRun.EndTime;
                    dto.LastRowsRead = lastRun.RowsRead;
                    dto.LastRowsInserted = lastRun.RowsInserted;
                    dto.LastRowsUpdated = lastRun.RowsUpdated;
                }

                result.Add(dto);
            }

            return result;
        }
    }
}
