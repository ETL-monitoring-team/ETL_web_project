using ETL_web_project.Data.Context;
using ETL_web_project.DTOs;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class EtlLogService : IEtlLogService
    {
        private readonly ProjectContext _context;

        public EtlLogService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<EtlLogSummaryDto> GetLogsAsync(
            DateTime? fromDate,
            DateTime? toDate,
            Enums.LogLevel? level,
            string? searchText)
        {
            var now = DateTime.UtcNow;
            var last24h = now.AddHours(-24);

            // BASE QUERY
            var query = _context.EtlLogs
                .Include(l => l.Run)
                .ThenInclude(r => r.Job)
                .AsQueryable();

            // FILTERS
            if (fromDate.HasValue)
                query = query.Where(l => l.LogTime >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(l => l.LogTime <= toDate.Value);

            if (level.HasValue)
                query = query.Where(l => l.Level == level.Value);

            if (!string.IsNullOrWhiteSpace(searchText))
                query = query.Where(l => l.Message.Contains(searchText));

            // SUMMARY
            var summary = new EtlLogSummaryDto
            {
                ErrorCount = await query.CountAsync(l => l.Level == Enums.LogLevel.Error),
                WarningCount = await query.CountAsync(l => l.Level == Enums.LogLevel.Warn),
                InfoCount = await query.CountAsync(l => l.Level == Enums.LogLevel.Info),

                ErrorsLast24h = await query.CountAsync(l => l.Level == Enums.LogLevel.Error && l.LogTime >= last24h),
                WarningsLast24h = await query.CountAsync(l => l.Level == Enums.LogLevel.Warn && l.LogTime >= last24h),
                InfoLast24h = await query.CountAsync(l => l.Level == Enums.LogLevel.Info && l.LogTime >= last24h),

                ActiveRuns = await _context.EtlRuns.CountAsync(r => r.EndTime == null)
            };

            // LOG LIST
            summary.Logs = await query
                .OrderByDescending(l => l.LogTime)
                .Take(250)
                .Select(l => new EtlLogListItemDto
                {
                    LogId = l.LogId,
                    LogTime = l.LogTime,
                    Level = l.Level,          // senin enum
                    Message = l.Message,

                    RunId = l.RunId,
                    JobName = l.Run.Job.JobName,
                    JobCode = l.Run.Job.JobCode,
                    Status = l.Run.Status
                })
                .ToListAsync();

            return summary;
        }

        public Task<string?> GetLogsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
