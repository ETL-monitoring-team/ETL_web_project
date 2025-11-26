using ETL_web_project.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("EtlRun", Schema = "etl")]
    public class EtlRun
    {
        [Key]
        public long RunId { get; set; }

        public int JobId { get; set; }

        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime? EndTime { get; set; }

        public EtlStatus Status { get; set; }

        public int? RowsRead { get; set; }
        public int? RowsInserted { get; set; }
        public int? RowsUpdated { get; set; }

        [MaxLength(2000)]
        public string? ErrorMessage { get; set; }

        [ForeignKey(nameof(JobId))]
        public virtual EtlJob Job { get; set; }
    }
}
