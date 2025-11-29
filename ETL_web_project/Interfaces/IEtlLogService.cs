using ETL_web_project.DTOs;
using ETL_web_project.DTOs.Etl.EtlLogs;
using LogLevel = ETL_web_project.Enums.LogLevel;

namespace ETL_web_project.Interfaces
{
    public interface IEtlLogService
    {
        Task<EtlLogSummaryDto> GetLogsAsync(DateTime? fromDate, DateTime? toDate, LogLevel? level, string? searchText);
    }
}

