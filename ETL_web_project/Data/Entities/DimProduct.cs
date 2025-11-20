using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ETL_web_project.Data.Entities
{
    [Table("DimProduct", Schema = "dw")]
    public class DimProduct
    {
        [Key]
        public int ProductKey { get; set; }

        [Required, MaxLength(50)]
        public string ProductCode { get; set; }

        [Required, MaxLength(200)]
        public string ProductName { get; set; }

        [MaxLength(100)]
        public string Category { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool IsActive { get; set; } = true;
    }
}


