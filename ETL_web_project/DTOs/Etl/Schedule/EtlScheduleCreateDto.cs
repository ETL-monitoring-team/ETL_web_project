using System.ComponentModel.DataAnnotations;

namespace ETL_web_project.DTOs.Etl.Schedule
{
    public class EtlScheduleCreateDto
    {
        [Required]
        public int JobId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FrequencyText { get; set; }
    }
}
