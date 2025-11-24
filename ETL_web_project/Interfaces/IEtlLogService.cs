using ETL_web_project.DTOs;
using LogLevel = ETL_web_project.Enums.LogLevel;

namespace ETL_web_project.Interfaces
{
    public interface IEtlLogService
    {
        /// <summary>
        /// ETL loglarını filtreleyerek getirir (date, level, search-text).
        /// Logs + Summary tek DTO içinde döner.
        /// </summary>
        Task<EtlLogSummaryDto> GetLogsAsync(
            DateTime? fromDate,
            DateTime? toDate,
            LogLevel? level,
            string? searchText);
        Task<string?> GetLogsAsync();
    }
}
