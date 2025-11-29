using ETL_web_project.Data.Context;
using ETL_web_project.DTOs;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using ETL_web_project.Data.Entities;
using Microsoft.EntityFrameworkCore;
using LogLevel = ETL_web_project.Enums.LogLevel;
using ETL_web_project.DTOs.Etl.Jobs;
using ETL_web_project.DTOs.Etl.RunHistory;

namespace ETL_web_project.Services
{
    public class EtlJobService : IEtlJobService
    {
        private readonly ProjectContext _context;

        public EtlJobService(ProjectContext context)
        {
            _context = context;
        }

        // ================================
        // 1) JOB LİSTESİ (arama ile)
        // ================================
        public async Task<List<EtlJobListItemDto>> GetJobsAsync(string? searchText)
        {
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

        // ================================
        // 2) BELİRLİ JOB İÇİN RUN HISTORY
        // ================================
        public async Task<List<EtlRunHistoryDto>> GetRunsForJobAsync(int jobId)
        {
            return await _context.EtlRuns
                .Where(r => r.JobId == jobId)
                .OrderByDescending(r => r.StartTime)
                .Select(r => new EtlRunHistoryDto
                {
                    RunId = r.RunId,
                    Status = r.Status,
                    StartTime = r.StartTime,
                    EndTime = r.EndTime,
                    DurationText = r.EndTime.HasValue
                        ? (r.EndTime.Value - r.StartTime).ToString("hh\\:mm\\:ss")
                        : "Running"
                })
                .ToListAsync();
        }

        // ================================
        // 3) RUN NOW (manuel ETL tetikleme)
        // ================================
        public async Task<long> TriggerRunAsync(int jobId)
        {
            var job = await _context.EtlJobs.FindAsync(jobId);
            if (job == null)
                throw new Exception("Job not found.");

            // --- ETL RUN KAYDI OLUŞTUR ---
            var run = new EtlRun
            {
                JobId = jobId,
                Status = EtlStatus.Running,
                StartTime = DateTime.UtcNow,
                ErrorMessage = string.Empty      // <-- NULL gitmesin
            };

            _context.EtlRuns.Add(run);
            await _context.SaveChangesAsync();

            // ilk log
            await AddLogAsync(run.RunId, LogLevel.Info, "Job manually triggered.");

            try
            {
                // Burada gerçekte ETL pipeline'ını çağırırsın.
                // Şimdilik simülasyon:
                await Task.Delay(1500);

                run.Status = EtlStatus.Success;
                run.EndTime = DateTime.Now;
                run.ErrorMessage = string.Empty;  // başarıyla bittiyse hata yok
                await _context.SaveChangesAsync();

                await AddLogAsync(run.RunId, LogLevel.Info, "Job completed successfully.");
            }
            catch (Exception ex)
            {
                run.Status = EtlStatus.Failed;
                run.EndTime = DateTime.Now;
                run.ErrorMessage = ex.Message;    // hata mesajını burada kaydediyoruz
                await _context.SaveChangesAsync();

                await AddLogAsync(run.RunId, LogLevel.Error, $"Job failed: {ex.Message}");
            }

            return run.RunId;
        }


        // ================================
        // 4) LOG HELPER
        // ================================
        private async Task AddLogAsync(long runId, LogLevel level, string message)
        {
            var log = new EtlLog
            {
                RunId = runId,
                Level = level,
                Message = message,
                LogTime = DateTime.Now
            };

            _context.EtlLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
