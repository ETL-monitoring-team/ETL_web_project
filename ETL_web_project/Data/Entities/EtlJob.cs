using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("EtlJob", Schema = "etl")]
    public class EtlJob
    {
        [Key]
        public int JobId { get; set; }

        [Required, MaxLength(100)]
        public string JobName { get; set; }

        [Required, MaxLength(50)]
        public string JobCode { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
