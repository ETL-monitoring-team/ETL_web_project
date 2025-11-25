using ETL_web_project.Enums;

namespace ETL_web_project.DTOs
{
    public class EtlJobListItemDto
    {
        public int JobId { get; set; }
        public string JobName { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;
        public string? Description { get; set; }

        public bool IsActive { get; set; }
        public long? LastRunId { get; set; }
        public EtlStatus? LastStatus { get; set; }
        public DateTime? LastStartTime { get; set; }
        public DateTime? LastEndTime { get; set; }

        public int? LastRowsRead { get; set; }
        public int? LastRowsInserted { get; set; }
        public int? LastRowsUpdated { get; set; }


        public string LastDurationText
        {
            get
            {
                if (LastStartTime.HasValue && LastEndTime.HasValue)
                {
                    var diff = LastEndTime.Value - LastStartTime.Value;
                    return diff.ToString(@"hh\:mm\:ss");
                }

                return "-";
            }
        }

        public string LastStartText =>
            LastStartTime?.ToString("dd.MM.yyyy HH:mm") ?? "Never";
    }
}
