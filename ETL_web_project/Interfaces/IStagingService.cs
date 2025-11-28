using ETL_web_project.DTOs;

namespace ETL_web_project.Interfaces
{
    public interface IStagingService
    {
        Task<StagingPageDto> GetStagingOverviewAsync();
        // Tüm staging verisini temizler
        Task ClearStagingAsync();

        // Staging verisini CSV string olarak üretir
        Task<string> ExportCsvAsync();
    }
}
