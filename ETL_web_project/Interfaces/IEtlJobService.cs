using ETL_web_project.DTOs;

namespace ETL_web_project.Interfaces
{
    public interface IEtlJobService
    {
        /// <summary>
        /// ETL job listesini döner, isim / kod / açıklama bazlı arama yapar.
        /// </summary>
        Task<List<EtlJobListItemDto>> GetJobsAsync(string? searchText);
    }
}
