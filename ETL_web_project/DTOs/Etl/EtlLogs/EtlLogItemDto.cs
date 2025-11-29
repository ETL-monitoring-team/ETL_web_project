using ETL_web_project.Enums;

namespace ETL_web_project.DTOs.Etl.EtlLogs
{
    public class EtlLogListItemDto
    {
        public long LogId { get; set; }
        public DateTime LogTime { get; set; }

        public Enums.LogLevel Level { get; set; }

        public string Message { get; set; } = string.Empty;
        public long RunId { get; set; }
        public string JobName { get; set; } = string.Empty;
        public string JobCode { get; set; } = string.Empty;
        public EtlStatus Status { get; set; }
    }
}
