using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("EtlLog", Schema = "etl")]
    public class EtlLog
    {
        [Key]
        public long LogId { get; set; }

        public long RunId { get; set; }

        public DateTime LogTime { get; set; } = DateTime.UtcNow;

        public LogLevel Level { get; set; }

        [Required, MaxLength(2000)]
        public string Message { get; set; }

        [ForeignKey(nameof(RunId))]
        public virtual EtlRun Run { get; set; }
    }
}
