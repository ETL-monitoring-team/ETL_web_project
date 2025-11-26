using System.ComponentModel.DataAnnotations;

namespace ETL_web_project.DTOs
{
    public class EtlScheduleEditDto
    {
        [Required]
        public int ScheduleId { get; set; }

        [Required]
        public int JobId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FrequencyText { get; set; }

        public bool IsActive { get; set; }
    }
}
