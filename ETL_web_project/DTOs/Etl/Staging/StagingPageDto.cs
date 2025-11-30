namespace ETL_web_project.DTOs.Etl.Staging
{
    public class StagingPageDto
    {
        public long TotalRawRows { get; set; }
        public long NewRowsLastLoad { get; set; }
        public DateTime? LastLoadTime { get; set; }
        public int? DataFreshnessMinutes { get; set; }
        public int ErrorCountLast24h { get; set; }

        public string SelectedTableName { get; set; } = "stg.SalesRaw";
        public List<StagingRowDto> RecentRows { get; set; } = new();
        public StagingSummaryDto Summary { get; set; } = new();
        public StagingQualityDto Quality { get; set; } = new();
        public List<StagingLoadTrendPointDto> LoadTrend { get; set; } = new();
        public List<StagingErrorLogDto> ErrorLogs { get; set; } = new();
    }
}
