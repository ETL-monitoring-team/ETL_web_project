using ETL_web_project.DTOs.Etl.Jobs;

namespace ETL_web_project.DTOs.Etl.Schedule
{
    public class EtlScheduleOverviewDto
    {
        public int TotalJobs { get; set; }
        public int ActiveJobs { get; set; }
        public int FailedLastRuns { get; set; }
        public DateTime? ClosestNextRun { get; set; }
        public List<EtlScheduleRowDto> Rows { get; set; } = new();
        public List<EtlJobListItemDto> AvailableJobs { get; set; } = new();
    }
}
