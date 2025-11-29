using ETL_web_project.Data.Entities;

namespace ETL_web_project.Interfaces
{
    public interface IEtlScheduleService
    {
        Task<List<EtlSchedule>> GetAllAsync();
        Task<EtlSchedule?> GetByIdAsync(int id);
        Task<EtlSchedule> CreateAsync(EtlSchedule schedule);
        Task<bool> UpdateAsync(EtlSchedule schedule);
        Task<bool> DeleteAsync(int id);
        Task<EtlRun> RunNowAsync(int scheduleId);
        Task<bool> ToggleActiveAsync(int scheduleId);
    }
}
