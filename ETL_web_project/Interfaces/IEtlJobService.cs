using ETL_web_project.DTOs.Etl.Jobs;
using ETL_web_project.DTOs.Etl.RunHistory;

namespace ETL_web_project.Interfaces
{
    public interface IEtlJobService
    {
        Task<List<EtlJobListItemDto>> GetJobsAsync(string? searchText);
        Task<List<EtlRunHistoryDto>> GetRunsForJobAsync(int jobId);
        Task<long> TriggerRunAsync(int jobId);
    }
}
