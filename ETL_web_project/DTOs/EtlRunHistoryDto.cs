using ETL_web_project.Enums;

namespace ETL_web_project.DTOs
{
    public class EtlRunHistoryDto
    {
        public long RunId { get; set; }
        public EtlStatus Status { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        // Serviste set edeceğimiz için get; set; olmalı
        public string DurationText { get; set; } = string.Empty;
    }
}
