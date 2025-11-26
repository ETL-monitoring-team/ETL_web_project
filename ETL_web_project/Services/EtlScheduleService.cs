using ETL_web_project.Data.Context;
using ETL_web_project.Data.Entities;
using ETL_web_project.Enums;
using ETL_web_project.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ETL_web_project.Services
{
    public class EtlScheduleService : IEtlScheduleService
    {
        private readonly ProjectContext _context;

        public EtlScheduleService(ProjectContext context)
        {
            _context = context;
        }

        public async Task<List<EtlSchedule>> GetAllAsync()
        {
            return await _context.EtlSchedules
                .Include(s => s.Job)
                .ToListAsync();
        }

        public async Task<EtlSchedule> CreateAsync(EtlSchedule schedule)
        {
            _context.EtlSchedules.Add(schedule);
            await _context.SaveChangesAsync();
            return schedule;
        }

        public async Task<EtlSchedule?> GetByIdAsync(int id)
        {
            return await _context.EtlSchedules.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(EtlSchedule schedule)
        {
            _context.EtlSchedules.Update(schedule);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var schedule = await GetByIdAsync(id);
            if (schedule == null) return false;

            _context.EtlSchedules.Remove(schedule);
            return await _context.SaveChangesAsync() > 0;
        }

        // PAUSE / RESUME
        public async Task<bool> ToggleActiveAsync(int scheduleId)
        {
            var schedule = await GetByIdAsync(scheduleId);
            if (schedule == null) return false;

            schedule.IsActive = !schedule.IsActive;

            return await _context.SaveChangesAsync() > 0;
        }

        // RUN NOW
        public async Task<EtlRun> RunNowAsync(int scheduleId)
        {
            var schedule = await _context
                .EtlSchedules
                .Include(s => s.Job)
                .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);

            if (schedule == null)
                throw new Exception("Schedule not found.");

            var run = new EtlRun
            {
                JobId = schedule.JobId,
                StartTime = DateTime.Now,
                Status = EtlStatus.Running,
                ErrorMessage = null
            };

            _context.EtlRuns.Add(run);
            await _context.SaveChangesAsync();

            try
            {
                //
                // ----------------------------------------
                //  BURADA GERÇEK ETL KODUN ÇALIŞACAK
                //  Extract → Transform → Load
                // ----------------------------------------
                //

                // DEMO AMAÇLI - Sanki başarıyla ETL tamamlandı
                await Task.Delay(1500);  // Simülasyon

                run.Status = EtlStatus.Success;
                run.EndTime = DateTime.Now;
                run.ErrorMessage = null;   // BAŞARILI: NULL

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                run.Status = EtlStatus.Failed;
                run.EndTime = DateTime.Now;
                run.ErrorMessage = ex.Message; // HATA BURAYA YAZILIR

                await _context.SaveChangesAsync();
                throw;
            }

            return run;
        }
    }
}
