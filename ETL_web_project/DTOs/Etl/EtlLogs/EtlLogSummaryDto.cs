namespace ETL_web_project.DTOs.Etl.EtlLogs
{
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
