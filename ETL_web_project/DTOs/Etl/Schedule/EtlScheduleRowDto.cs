using ETL_web_project.Enums;

namespace ETL_web_project.DTOs.Etl.Schedule
{
    public class EtlScheduleRowDto
    {
        public int JobId { get; set; }
        public int ScheduleId { get; set; }
        public string JobName { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;

        public string FrequencyText { get; set; } = string.Empty;

        public DateTime? LastRunTime { get; set; }
        public EtlStatus? LastRunStatus { get; set; }

        public DateTime? NextRunTime { get; set; }

        public bool IsActive { get; set; }
    }

}
