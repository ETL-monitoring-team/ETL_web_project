using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("EtlSchedule", Schema = "etl")]
    public class EtlSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        public int JobId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FrequencyText { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey(nameof(JobId))]
        public virtual EtlJob Job { get; set; }
    }
}
