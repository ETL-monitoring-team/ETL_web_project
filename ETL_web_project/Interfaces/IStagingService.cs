using ETL_web_project.DTOs;
using ETL_web_project.DTOs.Etl.Staging;

namespace ETL_web_project.Interfaces
{
    public interface IStagingService
    {
        Task<StagingPageDto> GetStagingOverviewAsync();
        Task ClearStagingAsync();
        Task<string> ExportCsvAsync();
    }
}
