using ETL_web_project.Enums;

namespace ETL_web_project.DTOs
{
    public class EtlLogListItemDto
    {
        public long LogId { get; set; }
        public DateTime LogTime { get; set; }

        // ÇAKIŞMA OLMAMASI İÇİN TAM AD ALANI KULLANDIK
        public ETL_web_project.Enums.LogLevel Level { get; set; }

        public string Message { get; set; } = string.Empty;

        // Run info
        public long RunId { get; set; }
        public string JobName { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;
        public EtlStatus Status { get; set; }
    }

    public class EtlLogSummaryDto
    {
        public int ErrorCount { get; set; }
        public int WarningCount { get; set; }
        public int InfoCount { get; set; }
        public int ActiveRuns { get; set; }

        public int ErrorsLast24h { get; set; }
        public int WarningsLast24h { get; set; }
        public int InfoLast24h { get; set; }

        public List<EtlLogListItemDto> Logs { get; set; } = new();
    }
}
