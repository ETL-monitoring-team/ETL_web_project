namespace ETL_web_project.DTOs.Etl.Jobs
{
    public class EtlJobRunDto
    {
        public long RunId { get; set; }
        public string JobName { get; set; } = string.Empty;

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Enums.EtlStatus Status { get; set; }

        public int? RowsRead { get; set; }
        public int? RowsInserted { get; set; }
        public int? RowsUpdated { get; set; }

        public string DurationText =>
            EndTime.HasValue
                ? (EndTime.Value - StartTime).ToString(@"hh\:mm\:ss")
                : "Running";
    }
}
