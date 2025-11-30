using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("DimDate", Schema = "dw")]
    public class DimDate
    {
        [Key]
        public int DateKey { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int Year { get; set; }
        public byte Month { get; set; }
        public byte Day { get; set; }

        [MaxLength(20)]
        public string MonthName { get; set; }

        public byte? DayOfWeek { get; set; }

        [MaxLength(20)]
        public string DayName { get; set; }
    }
}
