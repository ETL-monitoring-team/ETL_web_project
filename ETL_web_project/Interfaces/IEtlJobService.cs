using ETL_web_project.DTOs;

namespace ETL_web_project.Interfaces
{
    public interface IEtlJobService
    {
        Task<List<EtlJobListItemDto>> GetJobsAsync(string? searchText);
    }
}
