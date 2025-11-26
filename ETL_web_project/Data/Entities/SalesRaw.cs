using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("SalesRaw", Schema = "stg")]
    public class SalesRaw
    {
        [Key]
        public int Id { get; set; }

        public DateTime? SalesTime { get; set; }

        [MaxLength(20)]
        public string StoreCode { get; set; }

        [MaxLength(50)]
        public string ProductCode { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }

        public DateTime LoadedAt { get; set; } = DateTime.Now;
    }
}
