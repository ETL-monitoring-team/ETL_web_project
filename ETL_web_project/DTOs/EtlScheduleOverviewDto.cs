using ETL_web_project.Enums;

namespace ETL_web_project.DTOs
{
    public class EtlScheduleRowDto
    {
        public int JobId { get; set; }

        public string JobName { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;

  
        public string FrequencyText { get; set; } = string.Empty;

        public DateTime? LastRunTime { get; set; }
        public EtlStatus? LastRunStatus { get; set; }

        /// <summary>
        /// Gerçek scheduler olmadığı için: LastRun + 1 saat gibi tahmini bir değer hesaplayacağız.
        /// Demo amaçlı.
        /// </summary>
        public DateTime? NextRunTime { get; set; }

        public bool IsActive { get; set; }
    }

    public class EtlScheduleOverviewDto
    {
        public int TotalJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int FailedLastRuns { get; set; }
        public DateTime? ClosestNextRun { get; set; }

        public List<EtlScheduleRowDto> Rows { get; set; } = new();
    }
}
